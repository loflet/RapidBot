using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidBot.Data.Configuration
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("Product");
            Property(p => p.UnitPrice).IsOptional().HasPrecision(12, 2);
            Property(p => p.ProductName).IsRequired().HasMaxLength(50);
            Property(p => p.SupplierId).IsRequired();
            Property(p => p.IsDiscontinued).IsRequired();
        }
    }
}
