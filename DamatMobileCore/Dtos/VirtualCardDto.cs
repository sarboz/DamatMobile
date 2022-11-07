using System;

namespace DamatMobile.Core.Dtos
{
    public record VirtualCardDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Balance { get; set; }
        public string CardType { get; set; }
    }
}