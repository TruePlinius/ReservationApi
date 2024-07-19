using ReservationApi.Business;
using ReservationApi.Contracts;
using ReservationApi.Repository;

namespace ReservationApi.Business
{
    public class Scheduler : IScheduler
    {
        private readonly ISchedulerRepository _schedulerRepository;

        public Scheduler(ISchedulerRepository schedulerRepository)
        {
            _schedulerRepository = schedulerRepository;
        }

        public List<CalendarEntry> Get(string providerName)
        {
            return _schedulerRepository.GetAvailability(providerName);
        }

        public void Add(CalendarEntry calendarEntry)
        {
            List<CalendarEntry> calendarEntries = new List<CalendarEntry>(); 

            // we need to chunk the time in 15 min slots
            calendarEntry.Start = new DateTime(
                calendarEntry.Start.Year,
                calendarEntry.Start.Month,
                calendarEntry.Start.Day,
                calendarEntry.Start.Hour,
                0, 0);

            calendarEntry.End = new DateTime(
                calendarEntry.End.Year,
                calendarEntry.End.Month,
                calendarEntry.End.Day,
                calendarEntry.End.Hour,
                0, 0);

            while (calendarEntry.Start != calendarEntry.End)
            {

                CalendarEntry newCalendarEntry = new CalendarEntry
                {
                    CustomerType = CustomerType.Provider,
                    ProviderName = calendarEntry.ProviderName,
                    Start = calendarEntry.Start
                };

                calendarEntry.Start = calendarEntry.Start.AddMinutes(15);

                newCalendarEntry.End = calendarEntry.Start;

                calendarEntries.Add(newCalendarEntry);
            }

            _schedulerRepository.AddAvailability(calendarEntries);
        }

        public void Reserve(CalendarEntry calendarEntry)
        {
            _schedulerRepository.MarkReserved(calendarEntry);
        }

        public void Confirm(CalendarEntry calendarEntry)
        {
            _schedulerRepository.MarkConfirmed(calendarEntry);
        }
    }
}
