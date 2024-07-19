using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Business;
using ReservationApi.Contracts;
using ReservationApi.Controllers;
using ReservationApi.Repository;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace TestProject1
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class ClientsUnitTests
    {

        private ClientsController _target;

        [SetUp]
        public void Setup()
        {
            var schedulerRepository = new SchedulerRepository();
            var scheduler = new Scheduler(schedulerRepository);

            _target = new ClientsController(null, scheduler);

            // add providers
            CalendarEntry calendarEntry = new CalendarEntry()
            {
                CustomerType = CustomerType.Provider,
                ProviderName = "jekyll",
                Created = DateTime.Parse("7/19/2024 10:46:47"),
                Start = DateTime.Parse("7/19/2024 10:46:47"),
                End = DateTime.Parse("7/19/2024 18:46:47"),
            };

            scheduler.Add(calendarEntry);
        }

        [Test]
        public async Task GetSchedules()
        {
            _target.Get("jekyll");

        }

        [Test]
        public async Task ReserveAppointment()
        {
            CalendarEntry calendarEntry = new CalendarEntry() 
            {
                CustomerType = CustomerType.Client,
                ProviderName = "jekyll",
                CustomerName = "NoShowCustomer",
                Start = DateTime.Parse("7/19/2024 12:00:00")
            };

            try
            {
                _target.Reserve(calendarEntry);
            }
            catch (Exception ex)
            {
            }

            var result = _target.Get("jekyll");

            // lets see if we can reserve it again!
            try
            {
                _target.Reserve(calendarEntry);
            }
            catch (Exception ex)
            {
            }

            var targetSlot = result.Where(r => r.Start == calendarEntry.Start).FirstOrDefault();

            targetSlot.ReservedDateTime = calendarEntry.Start.AddMinutes(-16);

            // lets see if we can reserve it again if not confirmed and time expired!
            try
            {
                _target.Reserve(calendarEntry);
            }
            catch (Exception ex)
            {
            }

            // test for confirmation block
            targetSlot.Confirmed = true;
            try
            {
                _target.Reserve(calendarEntry);
            }
            catch (Exception ex)
            {
            }
        }

        [Test]
        public async Task ConfirmAppointment()
        {
            CalendarEntry calendarEntry = new CalendarEntry()
            {
                CustomerType = CustomerType.Client,
                ProviderName = "jekyll",
                CustomerName = "NoShowCustomer",
                Start = DateTime.Parse("7/19/2024 12:30:00")
            };

            try
            {
                _target.Reserve(calendarEntry);

                _target.Confirm(calendarEntry);

                var result = _target.Get("jekyll");

            }
            catch (Exception ex)
            {

            }
        }
    }
}
