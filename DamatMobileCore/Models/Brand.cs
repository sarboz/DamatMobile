using System;
using System.Collections.Generic;
using DamatMobile.Core.Dtos;

namespace DamatMobile.Core.Models
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<BrandImage> BrandImages { get; set; }
    }
}