using CafeWebApp.DomainModelEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeWebApp.InfrastructurePersistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=AppDbContext.cs")
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Food> Food { get; set; }

        //public System.Data.Entity.DbSet<CafeWebApp.Web.Models.Cart> Carts { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Table> Table { get; set; }
    }
}
