namespace Application.DTOs.Payment;

public class PaymentDTO
{
    public int? OrderId { get; set; }
    public string? PaymentMethod { get; set; }
    public decimal? Amount { get; set; }
    public string? Status { get; set; }
    public string? ReferenceNumber { get; set; }
    public DateTime? PaymentDate { get; set; }
}
