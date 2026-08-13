using Domain.Base;
using Domain.Entities.Category;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Entities.Product
{
    public class ProductDiscounts : BaseEntity<int>, IIsActive
    {
        public int? ProductId { get; private set; }
        public decimal? Percentage { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool IsActive { get; private set; }
        

        //Navegation properties
        public virtual Products Product { get; set; }

        private ProductDiscounts(){}

        private ProductDiscounts(int? productId, decimal? percentage, DateTime? startDate, DateTime endDate, bool isActive)
        {
            ProductId = productId;
            Percentage = percentage;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = isActive;
        }

        public static ProductDiscounts CreateProductDiscounts (int? productId, decimal? percentage, DateTime? startDate, DateTime endDate, bool isActive)
        {
            if(productId <= 0)
                throw new DomainException("El producto debe de ser valido");
            if(percentage < 0 || percentage >= 100)
                throw new DomainException("El porcentaje de descuento no puede ser 0 o 100");
            if(startDate > endDate)
                throw new DomainException("El dia de inicio del descuento no puede ser despues del dia de terminacion");

            return new ProductDiscounts(productId, percentage, startDate, endDate, isActive);
        }
    }
}
