using Application.Contracts.Persistence;
using Application.Contracts.Repositories.Order;
using Application.Contracts.Repositories.Product;
using Application.DTOs.Order.Requests.UpdateQuantity;
using Application.DTOs.Order.Responses.UpdateQuantity;
using Application.Result;
using Domain.Entities.Order.Enums;
using Domain.Entities.Product;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Order.UpdateQuantity;

public sealed class UpdateOrderItemQuantityUseCase(IOrderRepository orderRepository,
                                            IProductsRepository productsRepository,
                                            IUnitOfWork unitOfWork
                                            )
{
    public async Task<OperationResult<UpdateOrderItemsQuantityResponse>> ExecuteAsync(int orderId, UpdateOrderItemsQuantityRequest request)
    {


        ArgumentNullException.ThrowIfNull(request);

        if(orderId <= 0)
        {
            return OperationResult<UpdateOrderItemsQuantityResponse>.Failure(
                "El ID de la orden no es válido.");
        }

        if(request.Items == null || request.Items.Count == 0)
        {
            return OperationResult<UpdateOrderItemsQuantityResponse>.Failure(
                "La orden debe contener al menos un producto.");
        }
        if(request.Items.Any(item => item is null))
        {
            return OperationResult<UpdateOrderItemsQuantityResponse>.Failure(
                "La lista contiene un artículo inválido.");
        }
        if(request.Items.Any(item => 
            item.ProductId <= 0 || item.Quantity <= 0))
        {
            return OperationResult<UpdateOrderItemsQuantityResponse>.Failure(
                "Cada producto debe tener un ID y una cantidad mayores que cero.");
        }

        var groupedItems = request.Items.GroupBy(item => item.ProductId)
            .Select(group => new
            {
                ProductId = group.Key,
                Quantity = group.Sum(item => (long)item.Quantity)
            })
            .ToList();

        if(groupedItems.Any(item => item.Quantity > int.MaxValue))
        {
            return OperationResult<UpdateOrderItemsQuantityResponse>.Failure(
                "La cantidad total del producto supera el máximo permitido.");
        }

        var products = new Dictionary<int, Products>();
        var quantityDifference = new Dictionary<int, int>();

        try
        {
            var order = await orderRepository.GetOrderWithItemsAsync(orderId);

            if(order is null)
            {
                return OperationResult<UpdateOrderItemsQuantityResponse>.Failure(
                    "No se pudo encontrado la orden");
            }

            if(order.OrderStatus != OrderStatuses.Pending)
            {
                return OperationResult<UpdateOrderItemsQuantityResponse>.Failure(
                    "Solo se pueden actualizar productos de una orden pendiente");
            }

            foreach(var item in groupedItems)
            {

                var product = await productsRepository.GetAsync(item.ProductId);

                if(product is null)
                {
                    return OperationResult<UpdateOrderItemsQuantityResponse>.Failure(
                        "No se pudo encontrado el producto");
                }

                var line = order.Items.FirstOrDefault(Items => Items.ProductId == item.ProductId);

                if(line is null)
                {
                    return OperationResult<UpdateOrderItemsQuantityResponse>.Failure(
                        "No se pudo encontrado el producto en esta order"); 
                }

                var difference = item.Quantity - line.Quantity;

                if(difference > 0 && difference > product.Stock)
                {
                    return OperationResult<UpdateOrderItemsQuantityResponse>.Failure(
                        $"No hay suficiente producto en stock para el producto{product.Name}");
                }

                products[item.ProductId] = product;
                quantityDifference[item.ProductId] = checked((int)difference);
                
            }

            foreach(var item in order.Items)
            {
                if(products.ContainsKey(item.ProductId))
                    continue;
                
                    var product = await productsRepository.GetAsync(item.ProductId);

                    if(product is null)
                    {
                        return OperationResult<UpdateOrderItemsQuantityResponse>.Failure(
                        $"No se encontró el producto {item.ProductId}.");
                    }

                    products[item.ProductId] = product;
                

            }

            foreach(var item in groupedItems)
            {
                order.UpdateQuantity(item.ProductId, checked((int)item.Quantity));

                if(quantityDifference[item.ProductId] < 0)
                {
                    products[item.ProductId].IncreaseStock(Math.Abs(quantityDifference[item.ProductId]));
                }
                if(quantityDifference[item.ProductId] > 0)
                {
                    products[item.ProductId].DecreaseStock(quantityDifference[item.ProductId]);
                }
                
            }
            try
            {
                await unitOfWork.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return OperationResult<UpdateOrderItemsQuantityResponse>.Failure(
                    "Ocurrió un error de concurrencia al actualizar la orden o el stock de los productos. " +
                    "Por favor, intente nuevamente.");
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