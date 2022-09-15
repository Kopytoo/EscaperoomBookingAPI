using System.Runtime.Serialization;

namespace EscaperoomBookingAPI.Core.Domain.Enums;

public enum Variant
{
    [EnumMember(Value = Constants.Variant.Default)]
    Default = 0,
    
    [EnumMember(Value = Constants.Variant.Weekday)]
    Weekday = 1,

    [EnumMember(Value = Constants.Variant.Weekend)]
    Weekend = 2,
}