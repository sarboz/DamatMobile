using System;

namespace DamatMobile.Core.Dtos
{
    public record ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DiscountValue { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }
        public string DetailText { get; set; }
    }
}