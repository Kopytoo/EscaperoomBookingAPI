using EscaperoomBookingAPI.Core.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EscaperoomBookingAPI.Core.Domain.Dtos;

public class BookingDetailsDto
{
    [JsonConverter(typeof(StringEnumConverter))]
    public Room SelectedRoom { get; set; }
    public DateTime VisitDate { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public Variant BookingVariant { get; set; }
    public int NumberOfPeople { get; set; }
    public int Price { get; set; }
}