using AuthPermissions;
using Multitenant_SingleDatabase.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Multitenant_SingleDatabase.PermissionsCode;
using AuthPermissions.BaseCode.SetupCode;
using AuthPermissions.BaseCode;
using Common.IdentityCookieCode;
using AuthPermissions.AspNetCore;
using Multitenant_SingleDatabase.InvoiceCode.Services;
using Multitenant_SingleDatabase.InvoiceCode.EfCoreCode;
using AuthPermissions.AspNetCore.Services;
using RunMethodsSequentially;
using AuthPermissions.AspNetCore.StartupServices;
using Multitenant_SingleDatabase.InvoiceCode.AppStart;
using AuthPermissions.SupportCode.AddUsersServices.Authentication;
using AuthPermissions.SupportCode.AddUsersServices;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

/// Adds a set of common identity services to the application, including a default UI, token providers,
/// and configures authentication to use identity cookie
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();



// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();


builder.Services.ConfigureApplicationCookie(options =>
{
    //this will cause all the logged-in users to have their claims periodically updated
    options.Events.OnValidatePrincipal = PeriodicCookieEvent.PeriodicRefreshUsersClaims;
});

builder.Services.RegisterAuthPermissions<SingleDbPermissions>(options =>
{
    options.TenantType = TenantTypes.SingleLevel;
    options.LinkToTenantType = LinkToTenantTypes.OnlyAppUsers;
    options.EncryptionKey = builder.Configuration[nameof(AuthPermissionsOptions.EncryptionKey)];
    options.PathToFolderToLock = builder.Environment.WebRootPath;
})
    //NOTE: This uses the same database as the individual accounts DB
    .UsingEfCoreSqlServer(connectionString)
    .IndividualAccountsAuthentication()
    .RegisterAddClaimToUser<AddTenantNameClaim>()
    .RegisterAddClaimToUser<AddRefreshEveryMinuteClaim>()
    .RegisterTenantChangeService<InvoiceTenantChangeService>()
    .AddRolesPermissionsIfEmpty(SingleAppAuthSetupData.RolesDefinition)
    .AddTenantsIfEmpty(SingleAppAuthSetupData.TenantDefinition)
    .AddAuthUsersIfEmpty(SingleAppAuthSetupData.UsersRolesDefinition)
    .RegisterFindUserInfoService<IndividualAccountUserLookup>()
    .RegisterAuthenticationProviderReader<SyncIndividualAccountUsers>()
    .AddSuperUserToIndividualAccounts()
    .SetupAspNetCoreAndDatabase(options =>
    {
        //Migrate individual account database
        options.RegisterServiceToRunInJob<StartupServiceMigrateAnyDbContext<ApplicationDbContext>>();
        //Add demo users to the database (if no individual account exist)
        options.RegisterServiceToRunInJob<StartupServicesIndividualAccountsAddDemoUsers>();

        //Migrate the application part of the database
        options.RegisterServiceToRunInJob<StartupServiceMigrateAnyDbContext<InvoicesDbContext>>();
        //This seeds the invoice database (if empty)
        options.RegisterServiceToRunInJob<StartupServiceSeedInvoiceDbContext>();
    });


//manually add services from the AuthPermissions.SupportCode project
//Add the SupportCode services
builder.Services.AddTransient<IAddNewUserManager, IndividualUserAddUserManager<IdentityUser>>();
builder.Services.AddTransient<ISignInAndCreateTenant, SignInAndCreateTenant>();
builder.Services.AddTransient<IInviteNewUserService, InviteNewUserService>();

builder.Services.RegisterSingleDbInvoices(builder.Configuration);

//builder.Services.AddRazorPages();

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Password settings.
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequiredUniqueChars = 1;

//    // Lockout settings.
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;

//    // User settings.
//    options.User.AllowedUserNameCharacters =
//    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//    options.User.RequireUniqueEmail = false;
//});

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    // Cookie settings
//    options.Cookie.HttpOnly = true;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

//    options.LoginPath = "/Identity/Account/Login";
//    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
//    options.SlidingExpiration = true;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
