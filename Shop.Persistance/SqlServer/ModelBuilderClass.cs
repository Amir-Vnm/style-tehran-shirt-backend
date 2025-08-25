using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.Persistance.SqlServer
{
    public static class ModelBuilderClass
    {
        public static void QueryFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<Cart>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<CartItems>().HasQueryFilter(x => !x.IsDelete);
        }
    }
}
