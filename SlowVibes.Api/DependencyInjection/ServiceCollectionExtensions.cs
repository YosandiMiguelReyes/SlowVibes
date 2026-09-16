using ApplicationUnitOfWork = Application.Contracts.Persistence.IUnitOfWork;
using Application.Contracts.Repositories.Order;
using Application.Contracts.Repositories.Product;
using Application.Contracts.Repositories.Users;
using Application.UseCases.Order.CreateOrder;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Order;
using Persistence.Repositories.Product;
using Persistence.Repositories.Users;
using Persistence.UoW;

namespace SlowVibes.Api.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSlowVibesServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("LaptopConnection")
            ?? throw new InvalidOperationException("La cadena de conexión 'LaptopConnection' no está configurada.");

        services.AddDbContext<SlowVibesDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<ApplicationUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddScoped<IProductDiscountsRepository, ProductDiscountRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<CreateOrderUseCase>();

        return services;
    }
}
