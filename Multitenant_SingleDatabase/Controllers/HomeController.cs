﻿using AuthPermissions.SupportCode.AddUsersServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multitenant_SingleDatabase.InvoiceCode.Services;
using Multitenant_SingleDatabase.Models;
using Multitenant_SingleDatabase.PermissionsCode;
using System.Diagnostics;

namespace Multitenant_SingleDatabase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string message)
        {
            ViewBag.Message = message;

            if (AddTenantNameClaim.GetTenantNameFromUser(User) == null)
                return View(new AppSummary());

            return RedirectToAction("Index", "Invoice");
        }

        public IActionResult CreateTenant()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", new { message = "You can't create a new tenant because you are all ready logged in." });

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTenant([FromServices] ISignInAndCreateTenant userRegisterInvite,
            string tenantName, string email, string password, string version, bool isPersistent)
        {
            var newUserData = new AddNewUserDto { Email = email, Password = password, IsPersistent = isPersistent };
            var newTenantData = new AddNewTenantDto { TenantName = tenantName, Version = version };
            var status = await userRegisterInvite.SignUpNewTenantWithVersionAsync(newUserData, newTenantData,
                SingleDbCreateTenantVersions.TenantSetupData);
            if (status.HasErrors)
                return RedirectToAction(nameof(ErrorDisplay),
                    new { errorMessage = status.GetAllErrors() });

            return RedirectToAction(nameof(Index),
                new { message = status.Message });
        }

        [AllowAnonymous]
        public ActionResult AcceptInvite(string verify)
        {
            return View(new AcceptInviteDto { Verify = verify });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AcceptInvite([FromServices] IInviteNewUserService inviteUserServiceService,
            string verify, string email, string userName, string password, bool isPersistent)
        {
            var status = await inviteUserServiceService.AddUserViaInvite(verify, email, null, password, isPersistent);
            if (status.HasErrors)
                return RedirectToAction(nameof(ErrorDisplay),
                    new { errorMessage = status.GetAllErrors() });

            return RedirectToAction(nameof(Index),
                new { message = status.Message });
        }

        public ActionResult ErrorDisplay(string errorMessage)
        {
            return View((object)errorMessage);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}