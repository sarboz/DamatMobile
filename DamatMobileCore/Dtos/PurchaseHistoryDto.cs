using System;
using System.Collections.Generic;

namespace DamatMobile.Core.Dtos
{
    public record PurchaseHistoryDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal CashBack { get; set; }
        public decimal PurchaseAmount { get; set; }
        public Guid CustomerId { get; set; }
        public List<PurchaseDetailDto> PurchaseDetails { get; set; }
    }
}