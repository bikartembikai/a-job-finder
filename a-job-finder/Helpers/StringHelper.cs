using System.Text.RegularExpressions;

namespace a_job_finder.Helpers
{
    public static class StringHelper
    {
        public static string StripHtml(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        public static string BreakLongText(string text, int length = 100)
        {
            string clean = StripHtml(text);
            if (string.IsNullOrEmpty(clean)) return string.Empty;
            if (clean.Length <= length) return clean;
            
            return clean.Substring(0, length) + "...";
        }
    }
}
