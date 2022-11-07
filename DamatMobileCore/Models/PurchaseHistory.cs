using System;
using System.Collections.Generic;

namespace DamatMobile.Core.Models
{
    public class PurchaseHistory:BaseEntity
    {
        public DateTime Date { get; set; }
        public Guid CustomerId { get; set; }
        public decimal CashBack { get; set; }
        public decimal PurchaseAmount { get; set; }
        public List<PurchaseDetail> PurchaseDetails { get; set; }
        public Customer Customer { get; set; }
    }
}