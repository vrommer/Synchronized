using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilsLib.HtmlUtils.HtmlParser.ParserStates
{
    public class ReadingHtml : ParserState
    {
        public ReadingHtml() : base(StateType.ReadingHtml)
        {
        }

        public override int ParseHtml(HtmlParser context, string htmlContent)
        {
            int numOfCharsParsed = 0;
            while (numOfCharsParsed < htmlContent.Length)
            {                
                // If reading </
                if ((htmlContent[numOfCharsParsed].Equals('<')) && 
                    (numOfCharsParsed + 1 < htmlContent.Length) && 
                    (htmlContent[numOfCharsParsed + 1].Equals('/')))
                {
                    context.ChangeState(context.GetState(StateType.ReadingClosingTag));
                    return numOfCharsParsed;
                }
                // If reading <
                else if (htmlContent[numOfCharsParsed].Equals('<'))
                {
                    context.ChangeState(context.GetState(StateType.ReadingOpeningTag));
                    return numOfCharsParsed;
                }
                // If reading entity
                else if (htmlContent[numOfCharsParsed].Equals('&'))
                {
                    context.ChangeState(context.GetState(StateType.ReadingHtmlEntity));
                    return numOfCharsParsed;
                }
                // Add your condition here

                numOfCharsParsed++;
            }
            // no tags
            return numOfCharsParsed;
        }
    }
}