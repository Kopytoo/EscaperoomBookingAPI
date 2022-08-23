using EscaperoomBookingAPI.Core.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EscaperoomBookingAPI.Core.Domain.Dtos;

public class SummaryDto
{
    [JsonConverter(typeof(StringEnumConverter))]
    public BookingStatus Status { get; set; }
    public DateTime CreationDate { get; set; }
    public int Price { get; set; }
    public CustomerDetailsDto CustomerDetailsDto { get; set; }
    public BookingDetailsDto BookingDetailsDto { get; set; }
}