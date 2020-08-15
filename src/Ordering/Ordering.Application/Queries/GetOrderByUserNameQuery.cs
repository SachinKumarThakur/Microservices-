using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Ordering.Application.Responses;

namespace Ordering.Application.Queries
{
    public class GetOrderByUserNameQuery:IRequest<IEnumerable<OrderResponse>>
    {
        public GetOrderByUserNameQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentException(nameof(userName));
        }

        public string UserName { get; set; }
    }
}
