using EventSource.Ordering.Application.Common.Persistance;
using EventSource.Ordering.Application.Infrastructure.Persistance;
using EventSource.Ordering.Domain.Orders.Read;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventSource.Ordering.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IReadRepository readRepository = new InMemoryReadRepository();

        // GET: api/<OrderReadController>
        [HttpGet]
        public IEnumerable<OrderRM> Get()
        {
            return readRepository.GetOrders();
        }
    }
}
