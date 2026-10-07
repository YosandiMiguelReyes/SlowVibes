using Application.DTOs.Order.Requests.AddOrderItem;
using Application.DTOs.Order.Responses.AddOrderItem;
using Application.Contracts.Persistence;
using Application.Contracts.Repositories.Order;
using Application.Contracts.Repositories.Product;
using Application.Result;
using Domain.Entities.Order;
using Domain.Exceptions;

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

            try
            {

                if(request.Items is null || request.Items.Count == 0)
                {
                    return OperationResult<AddOrderItemsResponse>.Failure("La orden debe contener al menos un producto.");
                }
                var order = await orderRepository.GetAsync(orderId);

                if (order is null)
                {
                    return OperationResult<AddOrderItemsResponse>.Failure("No se pudo encontrar la order");
                }
                //I won't validate the status of the order since the entity does (Domain)

                var produtsName = new Dictionary<int, string>();

                foreach(var requestedItems in request.Items)
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

                    product.DecreaseStock(requestedItems.Quantity);

                    var discounts = await productDiscountsRepository.FindAsync
                        (discount => discount.ProductId == product.Id);

                    var discountApplied = discounts
                        .Where(discount => discount.IsCurrentlyActive)
                        .Select(discount => discount.Percentage)
                        .DefaultIfEmpty(0m)
                        .Max();

                    order.AddItem(
                        OrderItems.CreateOrderItems
                        (
                            product.Id,
                            requestedItems.Quantity,
                            product.PurchasePrice,
                            product.SalePrice,
                            discountApplied
                        ));

                    productsRepository.Update(product);

                    produtsName [product.Id] = product.Name;

                }
                orderRepository.Update(order);
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
                    ProductName = produtsName[item.ProductId],
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
