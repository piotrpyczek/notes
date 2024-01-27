using System.Text.RegularExpressions;

namespace Notes.Infrastructure.TagResolver
{
    public class PhoneTagResolver : ITagResolver
    {
        private Regex phoneRegex;

        public PhoneTagResolver()
        {
            phoneRegex = new Regex("(?:[(]?[\\+]?[0-9]{2}[)]?)?[-\\s]?[0-9]{3}[-\\s]?[0-9]{3}[-\\s]?[0-9]{3}", RegexOptions.Compiled);
        }

        public string Name
        {
            get { return "PHONE"; }
        }

        public bool AppliesTo(string text)
        {
            return phoneRegex.IsMatch(text);
        }
    }
}
