namespace LSWebApp.Infrastructure
{
    public class Utilities
    {
        public static string FormatNumber(decimal? number)
        {
            if (number.HasValue)
            {
                return string.Format("{0:C}", number).Replace("$", string.Empty);
            }
            return string.Empty;
        }
    }
}