using AuthPermissions.AdminCode;
using AuthPermissions.AspNetCore.AccessTenantData;
using AuthPermissions.AspNetCore;
using AuthPermissions.BaseCode.CommonCode;
using Microsoft.AspNetCore.Mvc;
using Multitenant_SingleDatabase.PermissionsCode;
using Multitenant_SingleDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace Multitenant_SingleDatabase.Controllers
{
    public class TenantController : Controller
    {
        private readonly IAuthTenantAdminService _authTenantAdmin;

        public TenantController(IAuthTenantAdminService authTenantAdmin)
        {
            _authTenantAdmin = authTenantAdmin;
        }

        [HasPermission(SingleDbPermissions.TenantList)]
        public async Task<IActionResult> Index(string message)
        {
            var tenantNames = await SingleLevelTenantDto.TurnIntoDisplayFormat(_authTenantAdmin.QueryTenants())
                .OrderBy(x => x.TenantName)
                .ToListAsync();

            ViewBag.Message = message;

            return View(tenantNames);
        }

        [HasPermission(SingleDbPermissions.TenantCreate)]
        public async Task<IActionResult> Create()
        {
            return View(new SingleLevelTenantDto { AllPossibleRoleNames = await _authTenantAdmin.GetRoleNamesForTenantsAsync() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasPermission(SingleDbPermissions.TenantCreate)]
        public async Task<IActionResult> Create(SingleLevelTenantDto input)
        {
            var status = await _authTenantAdmin.AddSingleTenantAsync(input.TenantName, input.TenantRolesName);

            return status.HasErrors
                ? RedirectToAction(nameof(ErrorDisplay),
                    new { errorMessage = status.GetAllErrors() })
                : RedirectToAction(nameof(Index), new { message = status.Message });
        }

        [HasPermission(SingleDbPermissions.TenantUpdate)]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await SingleLevelTenantDto.SetupForUpdateAsync(_authTenantAdmin, id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasPermission(SingleDbPermissions.TenantUpdate)]
        public async Task<IActionResult> Edit(SingleLevelTenantDto input)
        {
            var status = await _authTenantAdmin
                .UpdateTenantNameAsync(input.TenantId, input.TenantName);

            return status.HasErrors
                ? RedirectToAction(nameof(ErrorDisplay),
                    new { errorMessage = status.GetAllErrors() })
                : RedirectToAction(nameof(Index), new { message = status.Message });
        }


        [HasPermission(SingleDbPermissions.TenantDelete)]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _authTenantAdmin.GetTenantViaIdAsync(id);
            if (status.HasErrors)
                return RedirectToAction(nameof(ErrorDisplay),
                    new { errorMessage = status.GetAllErrors() });

            return View(new SingleLevelTenantDto
            {
                TenantId = id,
                TenantName = status.Result.TenantFullName
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasPermission(SingleDbPermissions.TenantDelete)]
        public async Task<IActionResult> Delete(SingleLevelTenantDto input)
        {
            var status = await _authTenantAdmin.DeleteTenantAsync(input.TenantId);

            return status.HasErrors
                ? RedirectToAction(nameof(ErrorDisplay),
                    new { errorMessage = status.GetAllErrors() })
                : RedirectToAction(nameof(Index), new { message = status.Message });
        }

        [HasPermission(SingleDbPermissions.TenantAccessData)]
        public async Task<IActionResult> StartAccess([FromServices] ILinkToTenantDataService service, int id)
        {
            var currentUser = User.GetUserIdFromUser();
            var status = await service.StartLinkingToTenantDataAsync(currentUser, id);

            return status.HasErrors
                ? RedirectToAction(nameof(ErrorDisplay),
                    new { errorMessage = status.GetAllErrors() })
                : RedirectToAction(nameof(Index), new { message = status.Message });
        }

        public IActionResult StopAccess([FromServices] ILinkToTenantDataService service, bool gotoHome)
        {
            var currentUser = User.GetUserIdFromUser();
            service.StopLinkingToTenant();

            return gotoHome
                ? RedirectToAction(nameof(Index), "Home")
                : RedirectToAction(nameof(Index), new { message = "Finished linking to tenant's data" });
        }

        public ActionResult ErrorDisplay(string errorMessage)
        {
            return View((object)errorMessage);
        }
    }
}
