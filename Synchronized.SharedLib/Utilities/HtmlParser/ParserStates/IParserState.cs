using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsLib.HtmlUtils.HtmlParser.ParserStates
{ 
    public interface IParserState
    {
        // Returns the zero-based number of chars parsed by the implementing IParseState Class
        int ParseHtmlContent(HtmlParser context, string htmlContent);
    }
}
