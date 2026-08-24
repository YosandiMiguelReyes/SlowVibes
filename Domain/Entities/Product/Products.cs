using Domain.Base;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Entities.Product
{
    public class Products : BaseEntity<int>, IIsActive, ICreatedAt
    {
        public int CategoryId { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string SKU { get; private set; } = string.Empty;

        public string? Description { get; private set; }

        public string? ImageUrl { get; private set; }

        public decimal PurchasePrice { get; private set; }

        public decimal SalePrice { get; private set; }

        public int Stock { get; private set; }

        public int LowStockThreshold { get; private set; }

        public bool IsActive { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        // EF Core
        private Products() { }

        private Products(int categoryId, string name, string? description, string? imageUrl, decimal purchasePrice, decimal salePrice, int stock, int lowStockThreshold)
        {
            CategoryId = categoryId;
            Name = name;
            SKU = GenerateSKU(name, categoryId);
            Description = description;
            ImageUrl = imageUrl;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            Stock = stock;
            LowStockThreshold = lowStockThreshold;

            IsActive = true;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public static Products Create(int categoryId, string name, string? description, string? imageUrl, decimal purchasePrice, decimal salePrice, int stock, int lowStockThreshold)
        {
            NormalizeFields(ref name, ref description, ref imageUrl);

            ValidateProductData(categoryId, name, purchasePrice, salePrice, lowStockThreshold);

            if (stock <= 0)
                throw new DomainException(
                    "El stock inicial debe ser mayor que cero.");

            return new Products(categoryId, name, description, imageUrl, purchasePrice, salePrice, stock, lowStockThreshold);
        }

        public void UpdateDetails(int categoryId, string name, string? description, string? imageUrl, decimal purchasePrice, decimal salePrice, int lowStockThreshold)
        {
            NormalizeFields(ref name, ref description, ref imageUrl);

            ValidateProductData(categoryId, name, purchasePrice, salePrice, lowStockThreshold);

            CategoryId = categoryId;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            LowStockThreshold = lowStockThreshold;
        }

        public void UpdateImage(string? imageUrl)
        {
            ImageUrl = string.IsNullOrWhiteSpace(imageUrl)
                ? null
                : imageUrl.Trim();
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException(
                    "La cantidad a agregar al stock debe ser mayor que cero.");

            Stock += quantity;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException(
                    "La cantidad a retirar del stock debe ser mayor que cero.");

            if (quantity > Stock)
                throw new DomainException(
                    "No se puede retirar una cantidad mayor al stock disponible.");

            Stock -= quantity;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        private static string GenerateSKU(string name, int categoryId)
        {
            var firstLetter = name
                .First(char.IsLetter)
                .ToString()
                .ToUpperInvariant();

            return $"{firstLetter}-{categoryId}";
        }

        private static void ValidateProductData(int categoryId, string name, decimal purchasePrice, decimal salePrice, int lowStockThreshold)
        {
            if (categoryId <= 0)
                throw new DomainException(
                    "El ID de la categoría debe ser válido.");

            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException(
                    "El nombre del producto no puede estar vacío.");

            if (purchasePrice < 0)
                throw new DomainException(
                    "El precio de compra no puede ser negativo.");

            if (salePrice <= 0)
                throw new DomainException(
                    "El precio de venta debe ser mayor que cero.");

            if (lowStockThreshold < 0)
                throw new DomainException(
                    "El umbral de stock bajo no puede ser negativo.");
        }

        private static void NormalizeFields(ref string name, ref string? description, ref string? imageUrl)
        {
            name = name.Trim();

            description = string.IsNullOrWhiteSpace(description)
                ? null
                : description.Trim();

            imageUrl = string.IsNullOrWhiteSpace(imageUrl)
                ? null
                : imageUrl.Trim();
        }


    }
}
