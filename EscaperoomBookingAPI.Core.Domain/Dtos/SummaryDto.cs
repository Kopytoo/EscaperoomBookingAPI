using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EscaperoomBookingAPI.Core.Domain.Dtos;

public class SummaryDto
{
    public Guid Id { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public BookingStatus Status { get; set; }
    public DateTime CreationDate { get; set; }
    public int Price { get; set; }
    public CustomerDetailsDto? CustomerDetails { get; set; }
    public BookingDetailsDto? BookingDetails { get; set; }
}