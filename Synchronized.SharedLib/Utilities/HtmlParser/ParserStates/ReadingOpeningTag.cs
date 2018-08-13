using System;

namespace UtilsLib.HtmlUtils.HtmlParser.ParserStates
{
    public class ReadingOpeningTag : ParserState
    {
        public ReadingOpeningTag() : base(StateType.ReadingOpeningTag)
        {
        }

        public override int ParseHtml(HtmlParser context, string htmlContent)
        {
            int indexOfWhitespace = htmlContent.IndexOf(" ");
            int indexOfGreaterThen = htmlContent.IndexOf(">");
            int i;
            if (indexOfWhitespace > -1)
            {
                i = Math.Min(indexOfWhitespace, indexOfGreaterThen);
            }
            else
            {
                i = indexOfGreaterThen;
            }
            if (-1 < i)
            {
                string tagName = htmlContent.Substring(1, i - 1);
                if (!_voidTags.Contains(tagName))
                {
                    PushToOpeningTagsStack(context, tagName);
                }
                if (i == htmlContent.IndexOf(" "))
                {
                    i = htmlContent.IndexOf(">");
                }
                context.ChangeState(context.GetState(StateType.ReadingHtml));
                return i + 1;
            }
            return i;
        }
    }
}