using System;
using System.Collections.Generic;

namespace DamatMobile.Core.Dtos
{
    public record CustomerDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
    
        public List<VirtualCardDto> VirtualCards { get; set; }
    }
}