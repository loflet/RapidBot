using RapidBot.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidBot.Data
{
    public class RapidBotEntities : DbContext
    {
        public RapidBotEntities() : base("RapidBotData") { }
        public RapidBotEntities(string connectionString) : base(connectionString)
        {
            //Note - Sanket
            //If you remove the base(connectionString) ensure that the below line is uncommented otherwise connection string from DbFactory will be wrongly set.
            // Database.Connection.ConnectionString = connectionString;

            //Note: Database Initialisation moved to web.config
            //Database.SetInitializer<RapidCircleAlmEntities>(new DropCreateDatabaseIfModelChanges<RapidCircleAlmEntities>());
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public virtual int Commit()
        {
            int stateChanges = base.SaveChanges();
            return stateChanges;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Reference your model configurations here from RapidBot.Data.Configuration.
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderItemConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new SupplierConfiguration());

        }
    }
}
