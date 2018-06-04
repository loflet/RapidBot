using RapidBot.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidBot.Data.Repositories
{
    public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

    public interface IOrderItemRepository : IRepository<OrderItem>
    {

    }
}
