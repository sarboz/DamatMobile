using System;

namespace DamatMobile.Core.Models
{
    public class PurchaseDetail : BaseEntity
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Count { get; set; }
        public Guid PurchaseHistoryId { get; set; }
        public PurchaseHistory PurchaseHistory { get; set; }
    }
}