using System.ComponentModel.DataAnnotations;
using Domain.Entities.Order.Enums;

namespace Application.DTOs.Order.Requests;

public record CreateOrderRequest
{
    [Range(1, int.MaxValue)]
    public int UserId { get; init; }

    [Required]
    [EnumDataType(typeof(OrderSources))]
    public OrderSources? OrderSource { get; init; }

    [Required]
    [EnumDataType(typeof(DeliveryTypes))]
    public DeliveryTypes? DeliveryType { get; init; }

    [StringLength(255)]
    public string? ShippingAddress { get; init; }

    [Required]
    [MinLength(1)]
    public List<CreateOrderItemRequest> Items { get; init; } = [];
}
