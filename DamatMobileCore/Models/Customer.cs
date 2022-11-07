using System;
using System.Collections.Generic;

namespace DamatMobile.Core.Models
{
    public class Customer:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public List<VirtualCard> VirtualCards { get; set; } = new();
        public List<PurchaseHistory> PurchaseHistories { get; set; } = new();
    }
}