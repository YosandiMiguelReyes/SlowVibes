using Application.DTOs.Order.Requests.AddOrderItem;
using Application.DTOs.Order.Responses.AddOrderItem;
using Application.Contracts.Persistence;
using Application.Contracts.Repositories.Order;
using Application.Contracts.Repositories.Product;
using Application.Result;
using Domain.Entities.Order;
using Domain.Exceptions;
using Domain.Entities.Product;
using Domain.Entities.Order.Enums;

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
#region Argument Validation
            ArgumentNullException.ThrowIfNull(request);

            if (orderId <= 0)
            {
                return OperationResult<AddOrderItemsResponse>.Failure(
                "El ID de la orden no es válido.");
            }

            if(request.Items is null || request.Items.Count == 0)
            {
                return OperationResult<AddOrderItemsResponse>.Failure(
                "La orden debe contener al menos un producto.");
            }

            if (request.Items.Any(item => item is null))
            {
                return OperationResult<AddOrderItemsResponse>.Failure(
                "La lista contiene un artículo inválido.");
            }

            if (request.Items.Any(item =>
                item.ProductId <= 0 || item.Quantity <= 0))
            {
                return OperationResult<AddOrderItemsResponse>.Failure(
                "Cada producto debe tener un ID y una cantidad mayores que cero.");
            }

            var groupedItems = request.Items
                .GroupBy(item => item.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    Quantity = group.Sum(item => (long)item.Quantity)
                })
                .ToList();
            
            if (groupedItems.Any(item => item.Quantity > int.MaxValue))
            {
                return OperationResult<AddOrderItemsResponse>.Failure(
                "La cantidad acumulada de un producto supera el máximo permitido.");
            }

#endregion
            
#region Load and Validate Request
            try
            {
                var order = await orderRepository
                    .GetOrderWithItemsAsync(orderId);

                if (order is null)
                {
                    return OperationResult<AddOrderItemsResponse>.Failure(
                        "No se pudo encontrar la order");
                }

                if (order.OrderStatus != OrderStatuses.Pending)
                {
                    return OperationResult<AddOrderItemsResponse>.Failure(
                    "Solo se pueden agregar productos a una orden pendiente.");
                }

                var products = new Dictionary<int, Products>();
                
                //products and stock validation.
                foreach(var requestedItem in groupedItems)
                {
                    var product = await productsRepository
                        .GetAsync(requestedItem.ProductId);

                    if(product is null)
                    {
                        return OperationResult<AddOrderItemsResponse>.Failure(
                            "Uno de los productos de la orden no existe.");
                    }

                    if(!product.IsActive)
                    {
                        return OperationResult<AddOrderItemsResponse>.Failure(
                            $"El producto '{product.Name}' no está disponible.");
                    }

                    if (product.Stock < requestedItem.Quantity)
                    {
                        return OperationResult<AddOrderItemsResponse>.Failure(
                            $"Stock insuficiente para '{product.Name}'. " +
                            $"Disponible: {product.Stock}; " +
                            $"solicitado: {requestedItem.Quantity}.");
                    }

                    var line = order.Items.FirstOrDefault(
                    item => item.ProductId == requestedItem.ProductId);

                    if (line is not null &&
                        requestedItem.Quantity >
                        (long)int.MaxValue - line.Quantity)
                    {
                        return OperationResult<AddOrderItemsResponse>.Failure(
                            $"La cantidad total de '{product.Name}' en la orden supera el máximo permitido.");
                    }

                    products [product.Id] = product;

                }

                foreach(var item in order.Items)
                {
                    if(products.ContainsKey(item.ProductId))
                        continue;
                    
                    var product = await productsRepository.GetAsync(item.ProductId);

                    if(product is null)
                    {
                        return OperationResult<AddOrderItemsResponse>.Failure(
                            $"No se encontró el producto {item.ProductId}.");
                    }

                    products[item.ProductId] = product;
                    
                }
#endregion
            // Phase 2: Prepare every order line and validate discounts
            // before mutating the order or stock.

#region Prepare Order Lines before Mutation

                var itemsToAdd = new List<(Products Product, int Quantity, OrderItems OrderItem)>();

                foreach(var item in groupedItems)
                {
                    var product = products[item.ProductId];
                    var quantity = checked((int)item.Quantity);

                    var discounts = await productDiscountsRepository.FindAsync
                        (discount => discount.ProductId == product.Id);

                    var discountApplied = discounts
                        .Where(discount => discount.IsCurrentlyActive)
                        .Select(discount => discount.Percentage)
                        .DefaultIfEmpty(0m)
                        .Max();

                    var orderItem = OrderItems.CreateOrderItems(
                        productId: product.Id,
                        quantity: quantity,
                        purchasePrice: product.PurchasePrice,
                        salePrice: product.SalePrice,
                        discountApplied: discountApplied
                    );

                    itemsToAdd.Add((product, quantity, orderItem));

                }
#endregion
                
                
#region All validation passed. Apply changes.

        foreach (var plannedItem in itemsToAdd)
        {
            order.AddItem(plannedItem.OrderItem);
            plannedItem.Product.DecreaseStock(plannedItem.Quantity);
        }
        await unitOfWork.SaveChangesAsync();
#endregion

#region Prepare Response
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
#endregion
         
        catch (DomainException exception)
        {
                return OperationResult<AddOrderItemsResponse>.Failure(
                    exception.Message);
        }


      } 

    }
}