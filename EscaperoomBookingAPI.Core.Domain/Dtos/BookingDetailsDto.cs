using EscaperoomBookingAPI.Core.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EscaperoomBookingAPI.Core.Domain.Dtos;

public class BookingDetailsDto
{
    [JsonConverter(typeof(StringEnumConverter))]
    public Room SelectedRoom { get; set; }
    public DateTime VisitDate { get; set; }
    public int NumberOfPeople { get; set; }
}