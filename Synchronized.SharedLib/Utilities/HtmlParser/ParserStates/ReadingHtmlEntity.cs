using System;
using System.Collections.Generic;
using System.Text;

namespace UtilsLib.HtmlUtils.HtmlParser.ParserStates
{
    class ReadingHtmlEntity : ParserState
    {
        public ReadingHtmlEntity() : base(StateType.ReadingHtmlEntity)
        {
        }

        public override int ParseHtml(HtmlParser context, string htmlContent)
        {
            int charsParsed = htmlContent.IndexOf(";");
            bool charFound = (-1 < charsParsed);
            if (!charFound)
            {
                return htmlContent.Length;                               
            }
            context.ChangeState(context.GetState(StateType.ReadingHtml));
            return charsParsed;
        }
    }
}
