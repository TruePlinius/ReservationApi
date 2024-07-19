using System.Text.Json.Serialization;

namespace ReservationApi.Contracts
{
    public class CalendarEntry
    {
        public string? ProviderName { get; set; }
        public CustomerType CustomerType { get; set; }
        public DateTime Created { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? CustomerName { get; set; }
        public DateTime ReservedDateTime { get; set; }
        public bool Reserved { get; set; }
        public bool Confirmed { get; set; }
    }
}
