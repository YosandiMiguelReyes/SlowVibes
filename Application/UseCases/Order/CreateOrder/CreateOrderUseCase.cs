using Application.Contracts.Persistence;
using Application.Contracts.Repositories.Order;
using Application.Contracts.Repositories.Product;
using Application.Contracts.Repositories.Users;
using Application.DTOs.Order.Requests.CreateOrder;
using Application.DTOs.Order.Responses.CreateOrder;
using Application.Result;
using Domain.Entities.Order;
using Domain.Exceptions;

namespace Application.UseCases.Order.CreateOrder;

public sealed class CreateOrderUseCase(
    IUserRepository userRepository,
    IProductsRepository productsRepository,
    IProductDiscountsRepository productDiscountsRepository,
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork)
{
    public async Task<OperationResult<CreateOrderResponse>> ExecuteAsync(CreateOrderRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        try 
        { 

        if (request.Items is null || request.Items.Count == 0)
            return OperationResult<CreateOrderResponse>.Failure("La orden debe contener al menos un producto.");


        var user = await userRepository.GetAsync(request.UserId);

        if (user is null)
        {
            return OperationResult<CreateOrderResponse>.Failure(
                "El usuario de la orden no existe.");
        }
        if (!user.IsActive)
{
            return OperationResult<CreateOrderResponse>.Failure(
                "El usuario está inactivo y no puede crear una orden.");
        }

        if (request.OrderSource is null)
        {
            return OperationResult<CreateOrderResponse>.Failure(
                "El origen de la orden es obligatorio.");
        }

        if (request.DeliveryType is null)
        {
            return OperationResult<CreateOrderResponse>.Failure(
                "El tipo de entrega es obligatorio.");
        }

        var order = Orders.Create(
            request.UserId,
            request.OrderSource.Value,
            request.DeliveryType.Value,
            request.ShippingAddress);

        var productNames = new Dictionary<int, string>();

        foreach (var requestedItem in request.Items)
        {
            var product = await productsRepository.GetAsync(
                requestedItem.ProductId);

            if (product is null)
            {
                return OperationResult<CreateOrderResponse>.Failure(
                    "Uno de los productos de la orden no existe.");
            }

            if (!product.IsActive)
            {
                return OperationResult<CreateOrderResponse>.Failure(
                    $"El producto '{product.Name}' no está disponible.");
            }

            product.DecreaseStock(requestedItem.Quantity);

            var discounts = await productDiscountsRepository.FindAsync(
                discount => discount.ProductId == product.Id);

            var discountApplied = discounts
                .Where(discount => discount.IsCurrentlyActive)
                .Select(discount => discount.Percentage)
                .DefaultIfEmpty(0m)
                .Max();

            order.AddItem(
                OrderItems.CreateOrderItems(
                    product.Id,
                    requestedItem.Quantity,
                    product.PurchasePrice,
                    product.SalePrice,
                    discountApplied));

            productsRepository.Update(product);

            productNames[product.Id] = product.Name;
        }

        await orderRepository.AddAsync(order);

        await unitOfWork.SaveChangesAsync();

        var response = new CreateOrderResponse
        {
            OrderId = order.Id,
            UserName = user.UserName ?? user.FullName,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderStatus = order.OrderStatus,
            OrderSource = order.OrderSource,
            DeliveryType = order.DeliveryType,
            ShippingAddress = order.ShippingAddress,

            Items = order.Items
                .Select(item => new CreateOrderItemResponse
                {
                    ProductId = item.ProductId,
                    ProductName = productNames[item.ProductId],
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

        return OperationResult<CreateOrderResponse>.Success(response);
    }
        catch (DomainException exception)
        {
            return OperationResult<CreateOrderResponse>.Failure(
                exception.Message);
        }
    }
}
