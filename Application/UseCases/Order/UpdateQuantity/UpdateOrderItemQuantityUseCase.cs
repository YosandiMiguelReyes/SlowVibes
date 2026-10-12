using Application.Contracts.Persistence;
using Application.Contracts.Repositories.Order;
using Application.Contracts.Repositories.Product;
using Application.DTOs.Order.Requests.UpdateQuantity;
using Application.DTOs.Order.Responses.UpdateQuantity;
using Application.Result;
using Domain.Entities.Product;
using Domain.Exceptions;

namespace Application.UseCases.Order.UpdateQuantity;

public sealed class UpdateOrderItemQuantityUseCase(IOrderRepository orderRepository,
                                            IProductsRepository productsRepository,
                                            IUnitOfWork unitOfWork
                                            )
{
    public async Task<OperationResult<UpdateOrderItemsQuantityResponse>> ExecuteAsync(int orderId, UpdateOrderItemsQuantityRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if(request.Items == null || request.Items.Count == 0)
        {
            return OperationResult<UpdateOrderItemsQuantityResponse>.Failure("La orden debe contener al menos un producto.");
        }

        var unique = request.Items.DistinctBy(item => item.ProductId).ToList();
        var products = new Dictionary<int, Products>();
        var quantityDifference = new Dictionary<int, int>();

        try
        {
            var order = await orderRepository.GetOrderWithItemsAsync(orderId);
            if(order is null)
            {
                return OperationResult<UpdateOrderItemsQuantityResponse>.Failure("No se pudo encontrado la orden");
            }

            foreach(var item in unique)
            {
                var line = order.Items.FirstOrDefault(Items => Items.ProductId == item.ProductId);

                if(line is null)
                {
                    return OperationResult<UpdateOrderItemsQuantityResponse>.Failure("No se pudo encontrado el producto en esta order"); 
                }

                var product = await productsRepository.GetAsync(item.ProductId);

                if(product is null)
                {
                    return OperationResult<UpdateOrderItemsQuantityResponse>.Failure("No se pudo encontrado el producto");
                }


                var difference = item.Quantity - line.Quantity;

                if(difference > 0 && difference > product.Stock)
                {
                    return OperationResult<UpdateOrderItemsQuantityResponse>.Failure($"No hay suficiente producto en stock para el producto{product.Name}");
                }

                products[item.ProductId] = product;
                quantityDifference[item.ProductId] = difference;
                
            }

            foreach(var item in order.Items)
            {
                if(!products.ContainsKey(item.ProductId))
                {
                    var product = await productsRepository.GetAsync(item.ProductId);

                    if(product is null)
                    {
                        return OperationResult<UpdateOrderItemsQuantityResponse>.Failure(
                        $"No se encontró el producto {item.ProductId}.");
                    }

                    products[item.ProductId] = product;
                }

            }

            foreach(var item in unique)
            {
                order.UpdateQuantity(item.ProductId, item.Quantity);

                if(quantityDifference[item.ProductId] < 0)
                {
                    products[item.ProductId].IncreaseStock(Math.Abs(quantityDifference[item.ProductId]));
                }
                if(quantityDifference[item.ProductId] > 0)
                {
                    products[item.ProductId].DecreaseStock(quantityDifference[item.ProductId]);
                }
                
            }
            await unitOfWork.SaveChangesAsync();

            var response = new UpdateOrderItemsQuantityResponse
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                OrderSource = order.OrderSource,
                DeliveryType = order.DeliveryType,
                ShippingAddress = order.ShippingAddress,
                Items = order.Items
                    .Select(item => new UpdateOrderItemQuantityResponse
                    {
                        ProductName = products[item.ProductId].Name,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        SalePrice = item.SalePrice,
                        DiscountApplied = item.DiscountApplied,

                        SubTotal =
                            item.SalePrice *
                            item.Quantity *
                            (1m - item.DiscountApplied / 100m)

                    }).ToList()
            };

            return OperationResult<UpdateOrderItemsQuantityResponse>.Success(response);
        }

        catch(DomainException error)
        {
            return OperationResult<UpdateOrderItemsQuantityResponse>.Failure(error.Message);
        }
        
    }
}