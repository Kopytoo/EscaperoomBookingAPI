using System.Runtime.Serialization;

namespace EscaperoomBookingAPI.Core.Domain.Enums;

public enum Room
{
    [EnumMember(Value = Constants.Room.CracowBeast)]
    CracowBeast = 0,

    [EnumMember(Value = Constants.Room.PirateShip)]
    PirateShip = 1,

    [EnumMember(Value = Constants.Room.AlchemistsMystery)]
    AlchemistsMystery = 2,
}