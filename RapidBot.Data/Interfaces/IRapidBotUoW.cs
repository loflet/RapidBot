using RapidBot.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidBot.Data.Interfaces
{
    public interface IRapidBotUoW
    {
        BaseRepository<Customer> CustomerRepository { get; }
        BaseRepository<Order> OrderRepository { get; }
        BaseRepository<OrderItem> OrderItemRepository { get; }
        BaseRepository<Product> ProductRepository { get; }
        BaseRepository<Supplier> SupplierRepository { get; }
        void Save();
    }
}
