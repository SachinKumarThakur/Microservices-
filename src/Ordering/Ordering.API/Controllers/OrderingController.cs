using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderingController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrderByUserNameQuery(userName);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        //this is for testing Purpose
        //Basically Order will be placed by Basket API and Message Consume by RABBITMQ in Order API Consumer Section
        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderResponse>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
