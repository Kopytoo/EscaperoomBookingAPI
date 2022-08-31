using EscaperoomBookingAPI.Core.Domain.Entities.Common;
using EscaperoomBookingAPI.Core.Domain.Enums;

namespace EscaperoomBookingAPI.Core.Domain.Entities.Master;

public class Summary : BaseEntity<Guid>
{
    public BookingStatus Status { get; set; }
    public DateTime CreationDate { get; set; }
    public Variant BookingVariant { get; set; }
    public int Price { get; set; }
    public CustomerDetails? CustomerDetails { get; set; }
    public BookingDetails? BookingDetails { get; set; }
}