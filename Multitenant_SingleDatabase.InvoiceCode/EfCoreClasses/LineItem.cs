using AuthPermissions.BaseCode.CommonCode;

namespace Multitenant_SingleDatabase.InvoiceCode.EfCoreClasses
{
    public class LineItem : IDataKeyFilterReadWrite
    {
        public int LineItemId { get; set; }

        public string ItemName { get; set; }

        public int NumberItems { get; set; }

        public decimal TotalPrice { get; set; }

        public string DataKey { get; set; }

        //----------------------------------------------
        // relationships 

        public int InvoiceId { get; set; }
    }
}
