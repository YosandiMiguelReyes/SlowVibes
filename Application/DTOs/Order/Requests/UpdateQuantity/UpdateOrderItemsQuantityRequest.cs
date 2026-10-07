using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Order.Requests.UpdateQuantity;

    public record UpdateOrderItemsQuantityRequest
    {
        [MinLength(1)]
        public List<UpdateOrderItemQuantityRequest> Items {get; init;} = [];
    }