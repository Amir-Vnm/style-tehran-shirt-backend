using System.Text.Json.Serialization;

namespace Shop.Application.Services.ProductService.Query.Dto
{
    public class ProductDto
    {
        public long Id { get; set; }

        public string Description { get; set; }

        // این فیلد فقط اسم فایل یا مسیر نسبی عکس رو نگه می‌داره
        public string ImageFile { get; set; }

        // این فیلد مسیر کامل قابل استفاده در فرانت رو می‌سازه
        [JsonPropertyName("imageUrl")]
        public string ImageUrl => $"/Product/{System.IO.Path.GetFileName(ImageFile)}";

        public double Price { get; set; }

        public int inventory { get; set; }

        public long CategoryId { get; set; }
    }
}
