namespace Shop.Domain.Entities
{
    public class CartItems: BaseEntities
    {
        public long ProductId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public long CartId { get; set; }
        public virtual Cart? Cart { get; set; }
    }
}
