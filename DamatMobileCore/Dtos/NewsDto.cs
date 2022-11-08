using System;

namespace DamatMobile.Core.Dtos
{
    public class NewsDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateTime { get; set; }
    }
}