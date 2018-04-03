using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilsLib.HtmlUtils.HtmlParser.ParserStates
{
    public class ReadingClosingTag : ParserState
    {
        public ReadingClosingTag() : base(StateType.ReadingClosingTag)
        {
        }

        public override int ParseHtml(HtmlParser inputContext, string htmlContent)
        {
            HtmlParser context = (HtmlParser)inputContext;
            string tagName = "";
            int i = htmlContent.IndexOf(">");
            if (-1 < i)
            {
                tagName = htmlContent.Substring(2, i - 2);
                // if reading incorrect closing tag.
                if (!context._openingTags.Peek().Equals(tagName))
                {
                    context._closingtags.Push(tagName);
                }
                else
                {
                    context._openingTags.Pop();
                }
                context.ChangeState(context.GetState(StateType.ReadingHtml));                
            }            
            return i + 1;
        }
    }
}