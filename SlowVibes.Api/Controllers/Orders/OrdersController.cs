using Application.DTOs.Order.Requests.AddOrderItem;
using Application.DTOs.Order.Requests.CreateOrder;
using Application.UseCases.Order.CreateOrder;
using Microsoft.AspNetCore.Mvc;

namespace SlowVibes.Api.Controllers.Orders;

[ApiController]
[Route("api/orders")]
public sealed class OrdersController(CreateOrderUseCase createOrderUseCase) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateOrderRequest request)
    {
        var result = await createOrderUseCase.ExecuteAsync(request);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error });

        return Created($"/api/orders/{result.Value!.OrderId}", result.Value);
    }

    public async Task<IActionResult> AddItemAsync(AddOrderItemRequest request)
    {
        // Implement the logic to add an item to the order
        // This is a placeholder for the actual implementation
        return Ok();
    }
}
