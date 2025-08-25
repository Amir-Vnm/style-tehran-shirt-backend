namespace Shop.Domain.Entities
{
    public class Cart: BaseEntities
    {
        public string BrowserId { get; set; }
        public bool Finished { get; set; }
        public ICollection<CartItems> CartItems { get; set; }

    }
}
