using Microsoft.IdentityModel.Tokens;

namespace Back_End.Utils
{
    public class StringHelper
    {
        public string GetfirstLetterUpper(string text)
        {
            if (text.IsNullOrEmpty())
            {
                return string.Empty;
            }
           return char.ToUpper(text[0]) + text.Substring(1).ToLower();
        }
    }
}
