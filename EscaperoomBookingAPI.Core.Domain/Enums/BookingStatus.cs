using System.Runtime.Serialization;

namespace EscaperoomBookingAPI.Core.Domain.Enums;

public enum BookingStatus
{
    [EnumMember(Value = Constants.BookingStatus.Pending)]
    Pending = 0,

    [EnumMember(Value = Constants.BookingStatus.Accepted)]
    Accepted = 1,

    [EnumMember(Value = Constants.BookingStatus.Canceled)]
    Canceled = 2,

    [EnumMember(Value = Constants.BookingStatus.Rejected)]
    Rejected = 3
}