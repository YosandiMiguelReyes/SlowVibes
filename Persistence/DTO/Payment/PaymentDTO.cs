using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.DTO.Payment
{
    public class PaymentDTO
    {
        public int? OrderId { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal? Amount { get; set; }
        public string? Status { get; set; } //max length 20
        public string? ReferenceNumber { get; set; } //max length 100
        public DateTime? PaymentDate { get; set; }
    }
}
