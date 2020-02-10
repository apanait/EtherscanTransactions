namespace UportTransactions.BLL
{
    public static class CustomExtensions
    {
        public static string ClearHexPrefix(this string hex)
        {
            return hex.Replace("0x", "");
        }
    }
}
