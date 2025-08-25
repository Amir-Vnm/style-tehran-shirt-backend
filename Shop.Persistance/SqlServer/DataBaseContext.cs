using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shop.Application.Interface;
using Shop.Domain.Entities;
using System.Data.Common;

namespace Shop.Persistance.SqlServer
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItems> CartItems { get; set; }

        public DbConnection GetDbConnection(DatabaseFacade databaseFacade)
        {
            var relationalConnection = databaseFacade.GetDbConnection();
            return relationalConnection;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ModelBuilderClass.QueryFilter(builder);

        }
    }
}
