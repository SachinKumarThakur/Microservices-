using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructre.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILoggerFactory loggerFactory,
            int? retry=0)
        {
            int retryForAvaibility = retry.Value;
            try
            {
                orderContext.Database.Migrate();
                if(!orderContext.Orders.Any())
                {
                    orderContext.Orders.AddRange(GetPreConfiguration());
                    await orderContext.SaveChangesAsync();
                }
            }
            catch(Exception exception)
            {
                if(retryForAvaibility<3)
                {
                    retryForAvaibility++;
                    var log = loggerFactory.CreateLogger<OrderContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(orderContext, loggerFactory, retryForAvaibility);
                }
            }
        }

        private static IEnumerable<Order> GetPreConfiguration()
        {
            return new List<Order>
            {
                new Order() { UserName = "sachin", FirstName = "Sachin", LastName = "Kumar", EmailAddress = "meh@ozk.com", AddressLine = "Bahcelievler", TotalPrice = 5239 },
                new Order() { UserName = "krity", FirstName = "Singh", LastName = "Arslan", EmailAddress ="sel@ars.com", AddressLine = "Ferah", TotalPrice = 3486 }
            };
        }
    }
}
