namespace Shop.Domain.Entities
{
    public class Category: BaseEntities
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Product> products { get; set; }
    }
}
