using System.Globalization;

namespace Web.UI.Infrastructure
{
    public static class StringExtensions
    {
        public static string Chop(this string str, int length) {
            if (string.IsNullOrWhiteSpace(str)) {
                return string.Empty;
            }

            var trimmedStr = str.Trim();

            if (trimmedStr.Length <= length)
                return trimmedStr;

            var result = trimmedStr.Substring(0, length - 4);
            return string.Format("{0} ...", result);
        }

        public static string ToUpperAndClearTurkishChars(this string word)
        {
            return word.ToUpper(new CultureInfo("tr-TR"))
                .Replace("Ğ", "G")
                .Replace("Ü", "U")
                .Replace("Ş", "S")
                .Replace("İ", "I")
                .Replace("Ö", "O")
                .Replace("Ç", "C");
        }

        public static string ConvertUrlFriendlyString(this string str) {
            str = str.ToLower(new CultureInfo("tr-Tr"))
                .Replace(" ", "-")
                .Replace("ı", "i")
                .Replace("ç", "c")
                .Replace("ü", "u")
                .Replace("ö", "o")
                .Replace("ğ", "g")
                .Replace("ş", "s");

            return str;
        }
    }
}