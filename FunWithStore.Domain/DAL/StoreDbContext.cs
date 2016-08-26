using System.Data.Entity;
using FunWithStore.Domain.Entities;

namespace FunWithStore.Domain.DAL
{
    public class StoreContext : System.Data.Entity.DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public StoreContext() : base("MyStoreContext")
        {
            Database.SetInitializer(new StoreDBInitializer());
        }
    }
}
