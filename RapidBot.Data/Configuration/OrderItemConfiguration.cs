using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidBot.Data.Configuration
{
    class OrderItemConfiguration : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemConfiguration()
        {
            ToTable("OrderItem");
            Property(o => o.UnitPrice).IsRequired().HasPrecision(12, 2);
            Property(o => o.Quantity).IsRequired();
        }
    }
}
