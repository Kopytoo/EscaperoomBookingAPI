namespace EscaperoomBookingAPI.Core.Domain.Dtos;

public class CustomerDetailsDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int PhoneNumber { get; set; }
    public string? OtherInfo { get; set; }
}