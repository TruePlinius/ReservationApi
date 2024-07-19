using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Business;
using ReservationApi.Contracts;
using ReservationApi.Controllers;
using ReservationApi.Repository;
using System.Globalization;

namespace TestProject1
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class ProvidersUnitTests
    {

        private ProvidersController _target;

        [SetUp]
        public void Setup()
        {
            _target = new ProvidersController(null, new Scheduler(new SchedulerRepository()));
        }


        [Test]
        public async Task GetSchedules()
        {
            _target.Get("jekyll");

        }

        [Test]
        public async Task PostSchedulesRegularEightHours()
        {
            CalendarEntry calendarEntry = new CalendarEntry() 
            {
                CustomerType = CustomerType.Provider,
                ProviderName = "jekyll",
                Created = DateTime.Parse("7/19/2024 10:46:47"),
                Start = DateTime.Parse("7/19/2024 10:46:47"),
                End = DateTime.Parse("7/19/2024 18:46:47"),
            };

            _target.Post(calendarEntry);

            var result = _target.Get("jekyll");

        }
    }
}
