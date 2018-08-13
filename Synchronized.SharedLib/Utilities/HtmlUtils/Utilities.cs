using System.Linq;

namespace UtilsLib.HtmlUtils
{
    class Utilities
    {
        public static string ReverseString(string str)
        {
            return new string( (from c in str select c).Reverse().ToArray() );
        }
    }
}
