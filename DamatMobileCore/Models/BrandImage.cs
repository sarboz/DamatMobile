using System;

namespace DamatMobile.Core.Models
{
    public class BrandImage:BaseEntity
    {
        public string Image { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}