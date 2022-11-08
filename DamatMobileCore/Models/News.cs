using System;

namespace DamatMobile.Core.Models
{
    public class News:BaseEntity
    {
        public string ImageUrl { get; set; }
        public DateTime DateTime { get; set; }
    }
}