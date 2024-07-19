using Microsoft.AspNetCore.Mvc;
using ReservationApi.Business;
using ReservationApi.Contracts;

namespace ReservationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProvidersController : ControllerBase
    {
        private readonly ILogger<ProvidersController> _logger;
        private readonly IScheduler _scheduler;

        public ProvidersController(ILogger<ProvidersController> logger, IScheduler scheduler)
        {
            _scheduler = scheduler;
            _logger = logger;
        }

        [HttpPost]
        public void Post([FromBody] CalendarEntry calendarEntry)
        {
            _scheduler.Add(calendarEntry);
        }

        [HttpGet]
        [Route("{ProviderName}")]
        public IEnumerable<CalendarEntry> Get([FromRoute(Name = "ProviderName")] string providerName)
        {
            var schedules = _scheduler.Get(providerName);

            return schedules;
        }
    }
}
