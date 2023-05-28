using AuthPermissions.BaseCode.CommonCode;
using System.ComponentModel.DataAnnotations;


namespace Multitenant.SingleLevelSharding.EfCoreClasses
{
    public class Invoice : IDataKeyFilterReadWrite
    {
        public int InvoiceId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string InvoiceName { get; set; }

        public DateTime DateCreated { get; set; }

        public string DataKey { get; set; }

        //----------------------------------------
        // relationships

        public List<LineItem> LineItems { get; set; }


    }
}
