using Microsoft.AspNetCore.Mvc;
using ReservationApi.Business;
using ReservationApi.Contracts;

namespace ReservationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ProvidersController> _logger;
        private readonly IScheduler _scheduler;

        public ClientsController(ILogger<ProvidersController> logger, IScheduler scheduler)
        {
            _scheduler = scheduler;
            _logger = logger;
        }

        [HttpGet]
        [Route("{ProviderName}")]
        public IEnumerable<CalendarEntry> Get([FromRoute(Name = "ProviderName")] string providerName)
        {
            var schedules = _scheduler.Get(providerName);


            return schedules;
        }

        [HttpPost]
        [Route("Reserve")]
        public void Reserve([FromBody] CalendarEntry calendarEntry)
        {
            // sanitation and validation here
            if (calendarEntry.Start < DateTime.Now.AddDays(1))
                throw new Exception("Reservations need to be 24 hours in advance");

            _scheduler.Reserve(calendarEntry);
        }

        [HttpPost]
        [Route("Confirm")]
        public void Confirm([FromBody] CalendarEntry calendarEntry)
        {
            _scheduler.Confirm(calendarEntry);
        }
    }
}
