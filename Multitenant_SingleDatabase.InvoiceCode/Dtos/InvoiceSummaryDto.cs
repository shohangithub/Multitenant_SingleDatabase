using Multitenant_SingleDatabase.InvoiceCode.EfCoreClasses;

namespace Multitenant_SingleDatabase.InvoiceCode.Dtos
{
    public class InvoiceSummaryDto
    {
        public int InvoiceId { get; set; }

        public string InvoiceName { get; set; }

        public DateTime DateCreated { get; set; }

        public int NumItems { get; set; }

        public double? TotalCost { get; set; }

        public static IQueryable<InvoiceSummaryDto> SelectInvoices(IQueryable<Invoice> invoices)
        {
            return invoices.Select(x => new InvoiceSummaryDto
            {
                InvoiceId = x.InvoiceId,
                InvoiceName = x.InvoiceName,
                DateCreated = x.DateCreated,
                NumItems = x.LineItems.Count,
                TotalCost = x.LineItems.Select(y => (double?)y.TotalPrice).Sum()
            });
        }
    }
}
