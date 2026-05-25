using System.Text.RegularExpressions;

namespace ParkingApp.Domain.Helpers;

public static class PlateHelper
{
    public static bool IsValid(string plate)
    {
        return Regex.IsMatch(
            plate,
            @"^[A-Z0-9]{5,10}$"
        );
    }
}