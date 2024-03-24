using System.Text.RegularExpressions;

namespace Petstagram.Services
{
    public class HtmlSanitizerService
    {
        public bool IsValid(string input)
        {
            //regular expression
            string pattern = @"[<>&'""\x00-\x1F\x7F]";

            return !Regex.IsMatch(input, pattern);
        }
        public string Sanitize(string input)
        {
            var replacements = new Dictionary<string, string>
            {
                {"<", "&lt;"},
                {">", "&gt;"},
                {"&", "&amp;"},
                {"'", "&apos;"},
                {"\"", "&quot;"}
            };

            foreach(var (key, value) in replacements)
            {
                input = Regex.Replace(input, Regex.Escape(key), value);
            }

            return input;
        }

        public bool AlllowedPicture(IFormFile file)
        {
            if (file == null)
            {
                return false;
            }

            string[] extensions = { ".jpg", ".png", ".jpeg" };
            string extension = Path.GetExtension(file.FileName);
            
            return extensions.Contains(extension);
        }
    }
}
