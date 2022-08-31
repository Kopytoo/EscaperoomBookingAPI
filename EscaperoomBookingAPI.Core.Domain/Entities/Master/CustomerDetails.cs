using EscaperoomBookingAPI.Core.Domain.Entities.Common;

namespace EscaperoomBookingAPI.Core.Domain.Entities.Master;

public class CustomerDetails : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int PhoneNumber { get; set; }
    public string? OtherInfo { get; set; }
    public Guid SummaryReference { get; set; }
    public Summary Summary { get; set; }
}