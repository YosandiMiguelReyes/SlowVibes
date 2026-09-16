using Application.Contracts.Persistence;
using Application.Contracts.Repositories.Order;
using Application.Contracts.Repositories.Product;
using Application.Contracts.Repositories.Users;
using Application.DTOs.Order.Requests;
using Application.DTOs.Order.Responses;
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
    public async Task<CreateOrderResponse> ExecuteAsync(CreateOrderRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.Items is null || request.Items.Count == 0)
            throw new DomainException("La orden debe incluir al menos un producto.");

        var user = await userRepository.GetAsync(request.UserId)
            ?? throw new DomainException("El usuario de la orden no existe.");

        if (!user.IsActive)
            throw new DomainException("No se puede crear una orden para un usuario inactivo.");

        var order = Orders.Create(
            request.UserId,
            request.OrderSource ?? throw new DomainException("El origen de la orden es obligatorio."),
            request.DeliveryType ?? throw new DomainException("El tipo de entrega es obligatorio."),
            request.ShippingAddress);

        var productNames = new Dictionary<int, string>();

        foreach (var requestedItem in request.Items)
        {
            var product = await productsRepository.GetAsync(requestedItem.ProductId)
                ?? throw new DomainException("Uno de los productos de la orden no existe.");

            if (!product.IsActive)
                throw new DomainException($"El producto '{product.Name}' no está disponible.");

            product.DecreaseStock(requestedItem.Quantity);

            var discounts = await productDiscountsRepository.FindAsync(
                discount => discount.ProductId == product.Id);

            var discountApplied = discounts
                .Where(discount => discount.IsCurrentlyActive)
                .Select(discount => discount.Percentage)
                .DefaultIfEmpty(0m)
                .Max();

            order.AddItem(OrderItems.CreateOrderItems(
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

        return new CreateOrderResponse
        {
            OrderId = order.Id,
            UserName = user.UserName ?? user.FullName,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderStatus = order.OrderStatus,
            OrderSource = order.OrderSource,
            DeliveryType = order.DeliveryType,
            ShippingAddress = order.ShippingAddress,
            Items = order.Items.Select(item => new CreateOrderItemResponse
            {
                ProductId = item.ProductId,
                ProductName = productNames[item.ProductId],
                Quantity = item.Quantity,
                SalePrice = item.SalePrice,
                DiscountApplied = item.DiscountApplied,
                SubTotal = item.SalePrice * item.Quantity * (1m - item.DiscountApplied / 100m)
            }).ToList()
        };
    }
}
