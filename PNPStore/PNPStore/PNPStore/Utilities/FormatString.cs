using System.Text.RegularExpressions;
using System.Text;

namespace PNPStore.Utilities
{
    public class FormatString
    {
        static public string RemoveAccents(string input)
        {
            string normalizedString = input.Normalize(NormalizationForm.FormD);
            Regex nonAphabetic = new Regex("[^a-zA-Z0-9 ]");
            return nonAphabetic.Replace(normalizedString, "");
        }
    }
}
