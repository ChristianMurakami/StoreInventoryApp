using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace StoreInventoryApplication.DataModels

{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options)
            : base(options)
        {}
        public DbSet<Product> Products { set; get; }
        public DbSet<shelf> Shelves { set; get; }
        public DbSet<Sale> Sales { set; get; }
        public DbSet<ProdId_N_Count> ProdId_N_Counts { set; get; }
        public DbSet<StoreOrder> StoreOrders { set; get; }
    }
}
