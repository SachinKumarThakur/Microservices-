using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Infrastructre.Data;
using Ordering.Infrastructre.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructre.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                      .Where(o => o.UserName == userName)
                      .ToListAsync();

            return orderList;
        }
    }
}