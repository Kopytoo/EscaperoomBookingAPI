using EscaperoomBookingAPI.Core.Domain.Entities.Common;
using EscaperoomBookingAPI.Core.Domain.Enums;

namespace EscaperoomBookingAPI.Core.Domain.Entities.Master;

public class BookingDetails : BaseEntity<Guid>
{
    public Room SelectedRoom { get; set; }
    public DateTime VisitDate { get; set; }
    public int NumberOfPeople { get; set; }
}