using AuthPermissions.AspNetCore;
using AuthPermissions.BaseCode.PermissionsCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Multitenant_SingleDatabase.InvoiceCode.AppStart;
using Multitenant_SingleDatabase.InvoiceCode.Dtos;
using Multitenant_SingleDatabase.InvoiceCode.EfCoreClasses;
using Multitenant_SingleDatabase.InvoiceCode.EfCoreCode;
using Multitenant_SingleDatabase.InvoiceCode.Services;
using Multitenant_SingleDatabase.PermissionsCode;

namespace Multitenant_SingleDatabase.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly InvoicesDbContext _context;

        public InvoiceController(InvoicesDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string message)
        {
            ViewBag.Message = message;

            var listInvoices = User.HasPermission(SingleDbPermissions.InvoiceRead)
                ? await InvoiceSummaryDto.SelectInvoices(_context.Invoices)
                    .OrderByDescending(x => x.DateCreated)
                    .ToListAsync()
                : null;
            return View(listInvoices);
        }

        [HasPermission(SingleDbPermissions.InvoiceCreate)]
        public IActionResult CreateInvoice()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasPermission(SingleDbPermissions.InvoiceCreate)]
        public async Task<IActionResult> CreateInvoice(Invoice invoice)
        {
            var builder = new ExampleInvoiceBuilder(null);
            var newInvoice = builder.CreateRandomInvoice(AddTenantNameClaim.GetTenantNameFromUser(User), invoice.InvoiceName);
            _context.Add(newInvoice);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { message = $"Added the invoice '{newInvoice.InvoiceName}'." });
        }

    }
}
