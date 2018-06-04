using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidBot.Model
{
    public partial class Product
    {        
        public Product()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string Package { get; set; }
        public bool IsDiscontinued { get; set; }
        
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
