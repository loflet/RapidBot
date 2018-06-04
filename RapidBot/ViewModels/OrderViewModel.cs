using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidBot.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }

        public ICollection<OrderItemViewModel> OrderItems { get; set; }
    }
}
