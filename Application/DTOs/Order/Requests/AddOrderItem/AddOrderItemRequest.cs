using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Order.Requests.AddOrderItem
{
    public record AddOrderItemRequest
    {
        [Range(1, int.MaxValue)]
       public int ProductId {get; init;}

       [Range (1, int.MaxValue)]
       public int Quantity {get; init;}
    }
}
