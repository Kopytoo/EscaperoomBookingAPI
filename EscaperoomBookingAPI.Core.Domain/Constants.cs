namespace EscaperoomBookingAPI.Core.Domain;

public static class Constants
{
    public static class BookingStatus
    {
        public const string Pending = "Pending";
        public const string Accepted = "Accepted";
        public const string Canceled = "Canceled";
        public const string Rejected = "Rejected";
    }

    public static class Room
    {
        public const string CracowBeast = "Cracow Beast";
        public const string PirateShip = "Pirate Ship";
        public const string AlchemistsMystery = "Alchemist's Mystery";
    }
    
    public static class Variant
    {
        public const string Default = "Default";
        public const string Weekday = "Weekday";
        public const string Weekend = "Weekend";
    }

    public static class Role
    {
        public const string Admin = "Admin";
        public const string Staff = "Staff";
        public const string User = "User";
    }
    
    public static class Credentials
    {
        public const string Username = "admin";
        public const string Password = "admin";
    }
}