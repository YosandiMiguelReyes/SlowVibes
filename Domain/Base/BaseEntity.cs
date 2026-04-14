

namespace Domain.Base
{
    public abstract class BaseEntity<Tkey>
    {
        public Tkey? Id { get; set; }
    }
}
