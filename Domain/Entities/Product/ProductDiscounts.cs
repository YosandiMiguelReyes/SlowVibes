using Domain.Base;
using Domain.Exceptions;

namespace Domain.Entities.Product
{
    public class ProductDiscounts : BaseEntity<int>
    {
        public int ProductId { get; private set; }
        public decimal Percentage { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        

        private ProductDiscounts(){}

        private ProductDiscounts(int productId, decimal percentage, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            ProductId = productId;
            Percentage = percentage;
            StartDate = startDate;
            EndDate = endDate;
        }

        private static void ValidateDiscount(decimal percentage, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            if(percentage <= 0 || percentage > 90)
                throw new DomainException("El porcentaje de descuento no puede ser menor a 0 o mayor a 90");
            if(startDate >= endDate)
                throw new DomainException("La fecha de inicio del descuento debe ser anterior a la fecha de finalización.");
        }

        public static ProductDiscounts CreateProductDiscounts (int productId, decimal percentage, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            ValidateDiscount(percentage, startDate, endDate);
            if(productId <= 0)
                throw new DomainException("El producto debe ser válido");

            return new ProductDiscounts(productId, percentage, startDate, endDate);
        }


        public void UpdateDiscount(decimal percentage, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            ValidateDiscount(percentage, startDate, endDate);

            Percentage = percentage;
            StartDate = startDate;
            EndDate = endDate;
        }

        public bool IsCurrentlyActive =>
            DateTimeOffset.UtcNow >= StartDate &&
            DateTimeOffset.UtcNow <= EndDate;
    }
}
