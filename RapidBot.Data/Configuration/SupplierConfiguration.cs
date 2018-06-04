using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidBot.Data.Configuration
{
    public class SupplierConfiguration : EntityTypeConfiguration<Supplier>
    {
        public SupplierConfiguration()
        {
            ToTable("Supplier");
            Property(s => s.CompanyName).IsRequired().HasMaxLength(40);
        }
    }
}
