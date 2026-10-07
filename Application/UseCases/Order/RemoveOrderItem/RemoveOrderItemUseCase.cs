using Application.Contracts.Persistence;
using Application.Contracts.Repositories.Order;
using Application.Contracts.Repositories.Product;
using Application.DTOs.Order.Requests.RemoveOrderItem;
using Application.DTOs.Order.Responses.RemoveOrderItem;
using Application.Result;
using Domain.Exceptions;
using Domain.Entities.Product;

namespace Application.UseCases.Order.RemoveOrderItem;

public sealed class RemoveOrderItemUseCase(IOrderRepository orderRepository,
                                           IProductsRepository productsRepository,
                                           IUnitOfWork unitOfWork)
{
    public async Task<OperationResult<RemoveOrderItemsReponse>> ExecuteAsync(int orderId, RemoveOrderItemsRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.ProductIds is null || request.ProductIds.Count == 0)
        {
            return OperationResult<RemoveOrderItemsReponse>.Failure("Se debe remover al menos un producto.");
        }

        var uniqueIds =  request.ProductIds.Distinct().ToList();

        try
        {
            var order = await orderRepository.GetAsync(orderId);

            if(order is null)
            {
                return OperationResult<RemoveOrderItemsReponse>.Failure("No se pudo encontrado la orden");
            }


            var productsToRemove = new Dictionary<int,Products>();

            foreach(var productId in uniqueIds)
            {
               var line = order.Items.FirstOrDefault(item => item.ProductId == productId);

               if(line is null)
                {
                    return OperationResult<RemoveOrderItemsReponse>.Failure("No se pudo encontrado el producto en esta order");
                }

                var product = await productsRepository.GetAsync(productId);

                if(product is null)
                {
                    return OperationResult<RemoveOrderItemsReponse>.Failure("No se pudo encontrado el producto");
                }

                productsToRemove[productId] = product;
                
            }
            var remainingItems = order.Items
                .Where(item => !uniqueIds.Contains(item.ProductId))
                .ToList();

            var remainingProductNames = new Dictionary<int, string>();


            foreach (var item in remainingItems)
            {
                var product = await productsRepository.GetAsync(item.ProductId);

                if (product is null)
                {
                    return OperationResult<RemoveOrderItemsReponse>.Failure(
                        $"No se encontró el producto {item.ProductId}.");
                }

                remainingProductNames[item.ProductId] = product.Name;
            }


            foreach (var productId in uniqueIds)
            {
                var line = order.Items.First(
                    item => item.ProductId == productId);

                var product = productsToRemove[productId];

                product.IncreaseStock(line.Quantity);
                order.RemoveItem(productId);

                productsRepository.Update(product);
            }

            orderRepository.Update(order);
            await unitOfWork.SaveChangesAsync();


            var response = new RemoveOrderItemsReponse
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                OrderSource = order.OrderSource,
                DeliveryType = order.DeliveryType,
                ShippingAddress = order.ShippingAddress,

                Items = order.Items.Select(item =>
                    new RemoveOrderItemReponse
                    {
                        ProductName = remainingProductNames[item.ProductId],
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        SalePrice = item.SalePrice,
                        DiscountApplied = item.DiscountApplied,
                        SubTotal = item.SalePrice
                            * item.Quantity
                            * (1m - item.DiscountApplied / 100m)
                    }).ToList()
            };

            return OperationResult<RemoveOrderItemsReponse>.Success(response);

        }
        catch(DomainException exception)
        {
            return OperationResult<RemoveOrderItemsReponse>.Failure(
                exception.Message);
        }

    }
}