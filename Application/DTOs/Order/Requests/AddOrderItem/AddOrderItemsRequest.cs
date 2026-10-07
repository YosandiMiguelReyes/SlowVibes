using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Order.Requests.AddOrderItem
{
    public record AddOrderItemsRequest
    {
        [Required]
        [MinLength(1)]
        public List<AddOrderItemRequest> Items {get; init;} = [];
    }
}
