using Common.CommonAdmin;
using Microsoft.AspNetCore.Mvc;
using AuthPermissions.AdminCode;
using Microsoft.EntityFrameworkCore;
using AuthPermissions.BaseCode.CommonCode;
using AuthPermissions.AspNetCore;
using Multitenant_Sharing.PermissionsCode;
using Multitenant_Sharing.Models;

namespace Multitenant_Sharing.Controllers
{
    public class AuthUsersController : Controller
    {
        private readonly IAuthUsersAdminService _authUsersAdmin;

        public AuthUsersController(IAuthUsersAdminService authUsersAdmin)
        {
            _authUsersAdmin = authUsersAdmin;
        }

        // List users filtered by authUser tenant
        //[HasPermission(SingleDbPermissions.UserRead)] !!! Turned off so that I can list all the users
        public async Task<ActionResult> Index(string message)
        {
            var authDataKey = User.GetAuthDataKeyFromUser();
            var userQuery = _authUsersAdmin.QueryAuthUsers(authDataKey);
            var usersToShow = await AuthUserDisplay.TurnIntoDisplayFormat(userQuery.OrderBy(x => x.Email)).ToListAsync();
            ViewBag.Message = message;

            return View(usersToShow);
        }

        [HasPermission(SingleDbPermissions.UserChange)]
        public async Task<ActionResult> Edit(string userId)
        {
            var status = await SetupManualUserChange.PrepareForUpdateAsync(userId, _authUsersAdmin);
            if (status.HasErrors)
                return RedirectToAction(nameof(ErrorDisplay),
                    new { errorMessage = status.GetAllErrors() });

            return View(status.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasPermission(SingleDbPermissions.UserChange)]
        public async Task<ActionResult> Edit(SetupManualUserChange change)
        {
            var status = await _authUsersAdmin.UpdateUserAsync(change.UserId,
                change.Email, change.UserName, change.RoleNames, change.TenantName);

            if (status.HasErrors)
                return RedirectToAction(nameof(ErrorDisplay),
                    new { errorMessage = status.GetAllErrors() });

            return RedirectToAction(nameof(Index), new { message = status.Message });
        }

        [HasPermission(SingleDbPermissions.UserSync)]
        public async Task<ActionResult> SyncUsers()
        {
            var syncChanges = await _authUsersAdmin.SyncAndShowChangesAsync();
            return View(syncChanges);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasPermission(SingleDbPermissions.UserSync)]
        //NOTE: the input be called "data" because we are using JavaScript to send that info back
        public async Task<ActionResult> SyncUsers(IEnumerable<SyncAuthUserWithChange> data)
        {
            var status = await _authUsersAdmin.ApplySyncChangesAsync(data);
            if (status.HasErrors)
                return RedirectToAction(nameof(ErrorDisplay),
                    new { errorMessage = status.GetAllErrors() });

            return RedirectToAction(nameof(Index), new { message = status.Message });
        }

        // GET: AuthUsersController/Delete/5
        [HasPermission(SingleDbPermissions.UserRemove)]
        public async Task<ActionResult> Delete(string userId)
        {
            var status = await _authUsersAdmin.FindAuthUserByUserIdAsync(userId);
            if (status.HasErrors)
                return RedirectToAction(nameof(ErrorDisplay),
                    new { errorMessage = status.GetAllErrors() });

            return View(AuthUserDisplay.DisplayUserInfo(status.Result));
        }

        // POST: AuthUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasPermission(SingleDbPermissions.UserRemove)]
        public async Task<ActionResult> Delete(AuthIdAndChange input)
        {
            var status = await _authUsersAdmin.DeleteUserAsync(input.UserId);
            if (status.HasErrors)
                return RedirectToAction(nameof(ErrorDisplay),
                    new { errorMessage = status.GetAllErrors() });

            return RedirectToAction(nameof(Index), new { message = status.Message });
        }

        public ActionResult ErrorDisplay(string errorMessage)
        {
            return View((object)errorMessage);
        }
    }
}
