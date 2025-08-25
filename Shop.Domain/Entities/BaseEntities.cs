namespace Shop.Domain.Entities
{
    public class BaseEntities<T>
    {
        public T Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public bool IsDelete { get; set; }
    }
    public abstract class BaseEntities : BaseEntities<long>
    {

    }
}
