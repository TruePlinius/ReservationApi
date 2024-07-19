using ReservationApi.Contracts;

namespace ReservationApi.Repository
{
    public interface ISchedulerRepository
    {
        void AddAvailability(List<CalendarEntry> calendarEntries);
        List<CalendarEntry> GetAvailability(string providerName);
        void MarkConfirmed(CalendarEntry calendarEntry);
        void MarkReserved(CalendarEntry calendarEntry);
    }
}
