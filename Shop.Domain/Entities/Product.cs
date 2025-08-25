namespace Shop.Domain.Entities
{
    public class Product: BaseEntities
    {
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public int inventory { get; set; }
        public Category Category { get; set; }
        public long CategoryId { get; set; }
    }
}
