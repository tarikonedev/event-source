using EventSource.Ordering.Application.Common.Commands;
using EventSource.Ordering.Application.Orders;
using EventSource.Ordering.Application.Orders.Commands;
using EventSource.Ordering.Domain.Orders.Write;
using Microsoft.AspNetCore.Mvc;

namespace EventSource.Ordering.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        private CommandDispatcher _commandDispatcher = new CommandDispatcher();

        private OrderService _orderService = new OrderService();

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<Order>> Get()
        {
            var items = await _orderService.GetAll();
            return items;
        }

        //[HttpGet, Route("{id}")]
        //public IEnumerable<WeatherForecast> Get(string id)
        //{
        //}

        [HttpPost]
        public Task CreateOrder([FromBody] CreateOrderCommand command)
        {
            return _commandDispatcher.Dispatch(command);
        }

        [HttpPost, Route("items")]
        public Task AddProduct([FromBody] AddOrderLineItemCommand command)
        {
            return _commandDispatcher.Dispatch(command);
        }
    }

}