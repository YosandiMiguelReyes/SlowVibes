using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Order.Requests.RemoveOrderItem
{
    public record RemoveOrderItemsRequest
    {
        [Required]
        [MinLength(1)]
        public List<int> ProductIds {get; init;} = [];
    }
}