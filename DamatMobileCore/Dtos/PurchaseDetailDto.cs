using System;

namespace DamatMobile.Core.Dtos
{
    public record PurchaseDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Discount  { get; set; }
        public string Count  { get; set; }
    }
}