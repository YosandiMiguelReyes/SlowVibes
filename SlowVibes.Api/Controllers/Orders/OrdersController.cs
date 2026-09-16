using Application.DTOs.Order.Requests;
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
        var response = await createOrderUseCase.ExecuteAsync(request);

        return Created($"/api/orders/{response.OrderId}", response);
    }
}
