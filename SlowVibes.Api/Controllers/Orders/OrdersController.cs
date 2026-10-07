using Application.DTOs.Order.Requests.AddOrderItem;
using Application.DTOs.Order.Requests.RemoveOrderItem;
using Application.DTOs.Order.Requests.CreateOrder;
using Application.UseCases.Order.AddOrderItem;
using Application.UseCases.Order.CreateOrder;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Order.RemoveOrderItem;
using Application.UseCases.Order.UpdateQuantity;
using Application.DTOs.Order.Requests.UpdateQuantity;

namespace SlowVibes.Api.Controllers.Orders;

[ApiController]
[Route("api/orders")]
public sealed class OrdersController(CreateOrderUseCase createOrderUseCase,
                                     AddOrderItemUseCase addOrderItemUseCase,
                                     RemoveOrderItemUseCase removeOrderItemUseCase,
                                     UpdateOrderItemQuantityUseCase updateOrderItemQuantityUseCase) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateOrderRequest request)
    {
        var result = await createOrderUseCase.ExecuteAsync(request);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error });

        return Created($"/api/orders/{result.Value!.OrderId}", result.Value);
    }

    [HttpPatch("{orderId:int}/items")]
    public async Task<IActionResult> AddItemAsync(int orderId,[FromBody] AddOrderItemsRequest request)
    {
        var result = await addOrderItemUseCase.ExecuteAsync(orderId, request);

        if (result.IsFailure)
            return BadRequest(new {error = result.Error});

        return Ok(result.Value);
    }

    [HttpPatch("{orderId:int}/items/remove")]
    public async Task<IActionResult> RemoveItemAsync(int orderId, [FromBody] RemoveOrderItemsRequest request)
    {
        var result = await removeOrderItemUseCase.ExecuteAsync(orderId, request);

        if(result.IsFailure)
        {
            return BadRequest(new {error = result.Error});
        }

        return Ok(result.Value);
    }

    [HttpPatch("{orderId:int}/items/quantity")]
    public async Task<IActionResult> UpdateItemQuantityAsync(int orderId, [FromBody] UpdateOrderItemsQuantityRequest request)
    {
        var result = await updateOrderItemQuantityUseCase.ExecuteAsync(orderId, request);

        if(result.IsFailure)
        {
            return BadRequest(new {error = result.Error});
        }

        return Ok(result.Value);
    }
}
