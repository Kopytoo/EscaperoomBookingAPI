using System.Runtime.Serialization;

namespace EscaperoomBookingAPI.Core.Domain.Enums;

public enum Variant
{
    [EnumMember(Value = Constants.Variant.Weekday)]
    Weekday = 0,

    [EnumMember(Value = Constants.Variant.Weekend)]
    Weekend = 1,
}