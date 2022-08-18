using System.Runtime.Serialization;

namespace EscaperoomBookingAPI.Core.Domain.Enums;

public enum Role
{
    [EnumMember(Value = Constants.Role.Admin)]
    Admin = 0,

    [EnumMember(Value = Constants.Role.Staff)]
    Staff = 1,
}