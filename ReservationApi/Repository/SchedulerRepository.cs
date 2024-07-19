using ReservationApi.Contracts;

namespace ReservationApi.Repository
{
    public class SchedulerRepository : ISchedulerRepository
    {
        private readonly Dictionary<string, List<CalendarEntry>> _providers = new Dictionary<string, List<CalendarEntry>>();

        public void AddAvailability(List<CalendarEntry> calendarEntries)
        {
            if (!_providers.ContainsKey(calendarEntries[0].ProviderName))
                _providers.Add(calendarEntries[0].ProviderName, new List<CalendarEntry>());

            _providers[calendarEntries[0].ProviderName].AddRange(calendarEntries);
        }

        public List<CalendarEntry> GetAvailability(string providerName)
        {
            List<CalendarEntry> availability;

            _providers.TryGetValue(providerName, out availability);

            return availability;            
        }

        public void MarkConfirmed(CalendarEntry calendarEntry)
        {
            // to do: need to all the checks
            List<CalendarEntry> availability;
            _providers.TryGetValue(calendarEntry.ProviderName, out availability);

            if (availability == null)
                throw new Exception("Provider doesnt exist");

            // find the slot
            var slot = availability.Where(a => a.Start == calendarEntry.Start).FirstOrDefault();

            // check available
            if (slot == null)
                throw new Exception("Time with provider doesnt exist");

            // check if its already confirmed
            slot.Confirmed = true;
        }

        public void MarkReserved(CalendarEntry calendarEntry)
        {
            List<CalendarEntry> availability;
            _providers.TryGetValue(calendarEntry.ProviderName, out availability);

            if (availability == null)
                throw new Exception("Provider doesnt exist");

            // find the slot
            var slot = availability.Where(a => a.Start == calendarEntry.Start).FirstOrDefault();
            
            // check available
            if (slot == null)
                throw new Exception("Time with provider doesnt exist");

            // check if its already confirmed
            if (slot.Confirmed)
                throw new Exception("Time slot already confirmed");

            if (slot.Reserved && slot.ReservedDateTime > DateTime.Now.AddMinutes(-30))
                throw new Exception("Time slot already reserved");

            // mark it and set time
            slot.Reserved = true;
            slot.ReservedDateTime = DateTime.Now;
            slot.CustomerName = calendarEntry.CustomerName;
        }
    }
}
