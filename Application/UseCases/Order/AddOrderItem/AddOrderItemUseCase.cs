using Application.DTOs.Order.Requests.AddOrderItem;
using Application.DTOs.Order.Responses.AddOrderItem;
using Application.Contracts.Persistence;
using Application.Contracts.Repositories.Order;
using Application.Contracts.Repositories.Product;
using Application.Result;
using Domain.Entities.Order;
using Domain.Exceptions;
using Domain.Entities.Product;
using Microsoft.AspNetCore.Http.Features;

namespace Application.UseCases.Order.AddOrderItem
{
    public sealed class AddOrderItemUseCase(
        IOrderRepository orderRepository, 
        IProductsRepository productsRepository, 
        IProductDiscountsRepository productDiscountsRepository,
        IUnitOfWork unitOfWork)
    {
        
        public async Task<OperationResult<AddOrderItemsResponse>> ExecuteAsync (int orderId, AddOrderItemsRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if(request.Items is null || request.Items.Count == 0)
            {
                return OperationResult<AddOrderItemsResponse>.Failure("La orden debe contener al menos un producto.");
            }

            var unique = request.Items.DistinctBy(i => i.ProductId).ToList();
            var products = new Dictionary<int, Products>();

            try
            {
                var order = await orderRepository.GetOrderWithItemsAsync(orderId);

                if (order is null)
                {
                    return OperationResult<AddOrderItemsResponse>.Failure("No se pudo encontrar la order");
                }

//-------------------------------------------------------------------------------------------------------------------------------------------
                
                foreach(var requestedItems in unique)
                {
                    var product = await productsRepository.GetAsync(requestedItems.ProductId);

                    if(product is null)
                    {
                        return OperationResult<AddOrderItemsResponse>.Failure("Uno de los productos de la orden no existe.");
                    }
                    if(!product.IsActive)
                    {
                        return OperationResult<AddOrderItemsResponse>.Failure($"El producto '{product.Name}' no está disponible.");
                    }

                    products [product.Id] = product;

                }
//------------------------------------------------------------------------------------------------------------------------------------

                foreach(var item in order.Items)
                {
                    if(!products.ContainsKey(item.ProductId))
                    {
                        var product = await productsRepository.GetAsync(item.ProductId);

                        if(product is null)
                        {
                            return OperationResult<AddOrderItemsResponse>.Failure(
                            $"No se encontró el producto {item.ProductId}.");
                        }

                    products[item.ProductId] = product;
                    }
                }

//-------------------------------------------------------------------------------------------------------------------------------------------
                foreach(var item in unique)
                {

                    var discounts = await productDiscountsRepository.FindAsync
                        (discount => discount.ProductId == products[item.ProductId].Id);

                    var discountApplied = discounts
                        .Where(discount => discount.IsCurrentlyActive)
                        .Select(discount => discount.Percentage)
                        .DefaultIfEmpty(0m)
                        .Max();

                    order.AddItem(
                        OrderItems.CreateOrderItems
                        (
                            products[item.ProductId].Id,
                            item.Quantity,
                            products[item.ProductId].PurchasePrice,
                            products[item.ProductId].SalePrice,
                            discountApplied
                        ));

                        products[item.ProductId].DecreaseStock(item.Quantity);

                }
                await unitOfWork.SaveChangesAsync();

        var response = new AddOrderItemsResponse 
        {
            OrderId = order.Id,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderStatus = order.OrderStatus,
            OrderSource = order.OrderSource,
            DeliveryType = order.DeliveryType,
            ShippingAddress = order.ShippingAddress,

            Items = order.Items
                .Select(item => new AddOrderItemResponse
                {
                    ProductId = item.ProductId,
                    ProductName = products[item.ProductId].Name,
                    Quantity = item.Quantity,
                    SalePrice = item.SalePrice,
                    DiscountApplied = item.DiscountApplied,

                    SubTotal =
                        item.SalePrice *
                        item.Quantity *
                        (1m - item.DiscountApplied / 100m)
                })
                .ToList()

                };

            return OperationResult<AddOrderItemsResponse>.Success(response);

        }
         
        catch (DomainException exception)
        {
                return OperationResult<AddOrderItemsResponse>.Failure(
                    exception.Message);
        }


      } 

    }
}
