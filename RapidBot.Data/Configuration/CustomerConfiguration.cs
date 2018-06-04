using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidBot.Data.Configuration
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customer");
            Property(c => c.FirstName).IsRequired().HasMaxLength(40);
            Property(c => c.LastName).IsRequired().HasMaxLength(40);            
        }
    }
}
