using Domain.Entities.Audit;
using Domain.Entities.Category;
using Domain.Entities.InventoryMovement;
using Domain.Entities.Notification;
using Domain.Entities.Order;
using Domain.Entities.Payment;
using Domain.Entities.Product;
using Domain.Entities.Roles;
using Domain.Entities.Settings;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class SlowVibesDbContext : DbContext
    {
        public SlowVibesDbContext(DbContextOptions<SlowVibesDbContext> context) : base(context)
        {
            
        }

        #region User
        public DbSet<Users> User { get; set; }
        #endregion

        #region Settings
        public DbSet<Settings> Setting { get; set; }
        #endregion

        #region Roles
        public DbSet<Roles> Role { get; set; }
        public DbSet<UserRoles> UserRole { get; set; }
        #endregion

        #region Product
        public DbSet<Products> Product { get; set; }
        public DbSet<ProductDiscounts> ProductDiscount { get; set; }
        #endregion

        #region Payment
        public DbSet<Payments> Payment { get; set; }
        public DbSet<PaymentMethods> PaymentMethod { get; set; }
        #endregion

        #region Order
        public DbSet<Orders> Order { get; set; }
        public DbSet<OrderItems> OrderItem { get; set; }
        public DbSet<OrderStatuses> OrderStatus { get; set; }
        #endregion

        #region Notification
        public DbSet<Notifications> Notification { get; set; }
        #endregion

        #region InventoryMovement
        public DbSet<InventoryMovements> InventoryMovement { get; set; }
        public DbSet<InventoryMovementTypes> InventoryMovementType { get; set; }
        public DbSet<InventoryMovementReasons> InventoryMovementReason { get; set; }
        #endregion

        #region Category
        public DbSet<Categories> Category { get; set; }
        #endregion

        #region Audit
        public DbSet<AuditLogs> AuditLog { get; set; }
        #endregion

    }
}
