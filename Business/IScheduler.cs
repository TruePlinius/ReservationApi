using ReservationApi.Contracts;

namespace ReservationApi.Business
{
    public interface IScheduler
    {
        List<CalendarEntry> Get(string providerName);
        void Add(CalendarEntry calendarEntry);
        void Reserve(CalendarEntry calendarEntry);
        void Confirm(CalendarEntry calendarEntry);
    }
}
