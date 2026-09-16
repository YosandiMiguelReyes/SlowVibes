using System.Linq.Expressions;
using System.Reflection;
using Application.Contracts.Persistence;
using Application.Contracts.Repositories.Order;
using Application.Contracts.Repositories.Product;
using Application.Contracts.Repositories.Users;
using Application.DTOs.Order.Requests;
using Application.UseCases.Order.CreateOrder;
using Domain.Base;
using Domain.Entities.Order;
using Domain.Entities.Order.Enums;
using Domain.Entities.Product;
using Domain.Entities.User;
using Domain.Exceptions;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Order.CreateOrder;

public sealed class CreateOrderUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_WithValidRequest_CreatesOrderReducesStockAndReturnsResponse()
    {
        var user = CreateUser(id: 1);
        var product = CreateProduct(id: 10, stock: 5);
        var discount = ProductDiscounts.CreateProductDiscounts(
            product.Id,
            percentage: 10m,
            DateTimeOffset.UtcNow.AddDays(-1),
            DateTimeOffset.UtcNow.AddDays(1));

        var userRepository = new Mock<IUserRepository>();
        var productsRepository = new Mock<IProductsRepository>();
        var discountsRepository = new Mock<IProductDiscountsRepository>();
        var ordersRepository = new Mock<IOrderRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        userRepository.Setup(repository => repository.GetAsync(user.Id)).ReturnsAsync(user);
        productsRepository.Setup(repository => repository.GetAsync(product.Id)).ReturnsAsync(product);
        discountsRepository
            .Setup(repository => repository.FindAsync(It.IsAny<Expression<Func<ProductDiscounts, bool>>>() ))
            .ReturnsAsync([discount]);
        ordersRepository
            .Setup(repository => repository.AddAsync(It.IsAny<Orders>()))
            .Callback<Orders>(order => SetId(order, 50))
            .Returns(Task.CompletedTask);
        unitOfWork.Setup(work => work.SaveChangesAsync()).ReturnsAsync(1);

        var useCase = CreateUseCase(
            userRepository,
            productsRepository,
            discountsRepository,
            ordersRepository,
            unitOfWork);

        var response = await useCase.ExecuteAsync(CreateValidRequest(product.Id, quantity: 3));

        Assert.Equal(50, response.OrderId);
        Assert.Equal("yosandi", response.UserName);
        Assert.Equal(27m, response.TotalAmount);
        Assert.Equal(2, product.Stock);
        Assert.Single(response.Items);
        Assert.Equal(27m, response.Items[0].SubTotal);
        Assert.Equal(10m, response.Items[0].DiscountApplied);
        productsRepository.Verify(repository => repository.Update(product), Times.Once);
        ordersRepository.Verify(repository => repository.AddAsync(It.IsAny<Orders>()), Times.Once);
        unitOfWork.Verify(work => work.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WithNoItems_ThrowsDomainException()
    {
        var useCase = CreateUseCase(
            new Mock<IUserRepository>(),
            new Mock<IProductsRepository>(),
            new Mock<IProductDiscountsRepository>(),
            new Mock<IOrderRepository>(),
            new Mock<IUnitOfWork>());

        var request = CreateValidRequest(productId: 10, quantity: 1) with { Items = [] };

        await Assert.ThrowsAsync<DomainException>(() => useCase.ExecuteAsync(request));
    }

    [Fact]
    public async Task ExecuteAsync_WithInactiveUser_ThrowsDomainExceptionWithoutChangingStock()
    {
        var user = CreateUser(id: 1);
        user.Deactivate();
        var userRepository = new Mock<IUserRepository>();
        var productsRepository = new Mock<IProductsRepository>();
        userRepository.Setup(repository => repository.GetAsync(user.Id)).ReturnsAsync(user);

        var useCase = CreateUseCase(
            userRepository,
            productsRepository,
            new Mock<IProductDiscountsRepository>(),
            new Mock<IOrderRepository>(),
            new Mock<IUnitOfWork>());

        await Assert.ThrowsAsync<DomainException>(() =>
            useCase.ExecuteAsync(CreateValidRequest(productId: 10, quantity: 1)));

        productsRepository.Verify(repository => repository.GetAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_WhenQuantityExceedsStock_DoesNotPersistOrder()
    {
        var user = CreateUser(id: 1);
        var product = CreateProduct(id: 10, stock: 2);
        var userRepository = new Mock<IUserRepository>();
        var productsRepository = new Mock<IProductsRepository>();
        var ordersRepository = new Mock<IOrderRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        userRepository.Setup(repository => repository.GetAsync(user.Id)).ReturnsAsync(user);
        productsRepository.Setup(repository => repository.GetAsync(product.Id)).ReturnsAsync(product);

        var useCase = CreateUseCase(
            userRepository,
            productsRepository,
            new Mock<IProductDiscountsRepository>(),
            ordersRepository,
            unitOfWork);

        await Assert.ThrowsAsync<DomainException>(() =>
            useCase.ExecuteAsync(CreateValidRequest(product.Id, quantity: 3)));

        ordersRepository.Verify(repository => repository.AddAsync(It.IsAny<Orders>()), Times.Never);
        unitOfWork.Verify(work => work.SaveChangesAsync(), Times.Never);
    }

    private static CreateOrderUseCase CreateUseCase(
        Mock<IUserRepository> users,
        Mock<IProductsRepository> products,
        Mock<IProductDiscountsRepository> discounts,
        Mock<IOrderRepository> orders,
        Mock<IUnitOfWork> unitOfWork) =>
        new(users.Object, products.Object, discounts.Object, orders.Object, unitOfWork.Object);

    private static CreateOrderRequest CreateValidRequest(int productId, int quantity) => new()
    {
        UserId = 1,
        OrderSource = OrderSources.Online,
        DeliveryType = DeliveryTypes.PickUp,
        Items = [new CreateOrderItemRequest { ProductId = productId, Quantity = quantity }]
    };

    private static Users CreateUser(int id)
    {
        var user = Users.Create("Yosandi", "yosandi", "yosandi@example.com", null, "password-hash");
        SetId(user, id);
        return user;
    }

    private static Products CreateProduct(int id, int stock)
    {
        var product = Products.Create(1, "Café", null, null, 5m, 10m, stock, 1);
        SetId(product, id);
        return product;
    }

    private static void SetId<T>(BaseEntity<T> entity, T id)
    {
        var property = typeof(BaseEntity<T>).GetProperty(nameof(BaseEntity<T>.Id), BindingFlags.Instance | BindingFlags.Public)
            ?? throw new InvalidOperationException("No se encontró la propiedad Id.");

        property.SetValue(entity, id);
    }
}
