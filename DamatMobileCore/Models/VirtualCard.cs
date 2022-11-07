using System;

namespace DamatMobile.Core.Models
{
    public class VirtualCard:BaseEntity
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Balance { get; set; }
        public string CardType { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}