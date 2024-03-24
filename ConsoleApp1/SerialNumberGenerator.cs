public static class SerialNumberGenerator 
{
    private static int lastNumber = 0;

    public static string GenerateSerialNumber(string type)
    {
        lastNumber++;
        return $"KON-{type}-{lastNumber}";
    }
}