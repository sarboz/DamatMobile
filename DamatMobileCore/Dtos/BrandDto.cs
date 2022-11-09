using System;
using System.Collections.Generic;

namespace DamatMobile.Core.Dtos
{
    public record BrandDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<BrandImageDto> BrandImages { get; set; }
    }

    public record BrandImageDto
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
    }
}