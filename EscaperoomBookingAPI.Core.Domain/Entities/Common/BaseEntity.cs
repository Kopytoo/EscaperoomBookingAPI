namespace EscaperoomBookingAPI.Core.Domain.Entities.Common;

public class BaseEntity<T> 
{
    public virtual T Id { get; set; }
}