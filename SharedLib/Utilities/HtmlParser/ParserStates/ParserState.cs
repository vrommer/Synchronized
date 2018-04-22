using System.Collections.Generic;

namespace UtilsLib.HtmlUtils.HtmlParser.ParserStates
{
    public abstract class ParserState : IParserState
    {
        /*********************************************
         * Fields
         *********************************************/
        public StateType _stateType { get; set; }
        public static List<string> _voidTags = new List<string>()
        {
            "area",
            "base",
            "br",
            "col",
            "command",
            "embed",
            "hr",
            "img",
            "input",
            "keygen",
            "link",
            "meta",
            "param",
            "source",
            "track",
            "wbr"
        };

        /*********************************************
         * Constructors
         *********************************************/
        public ParserState(StateType stateType)
        {
            this._stateType = stateType;
        }
        /*********************************************
         * Methods
         *********************************************/
        public override int GetHashCode()
        {
            return _stateType.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return ((int)_stateType == int.Parse(obj.GetType().GetMember("_stateType")[0].ToString()));
        }
        /**
         * Parse HTML file. 
         * Returns true if end of file is reached.
         */
        public int ParseHtmlContent(HtmlParser context, string htmlContent)
        {
            int index = ParseHtml(context, htmlContent);
            if (-1 < index)
            {
                return index;
            }
            else
            {
                return htmlContent.Length;
            }
        }

        protected void PushToOpeningTagsStack(HtmlParser context, string element)
        {
            context._openingTags.Push(element);
        }

        protected void PushToClosingTagsStack(HtmlParser context, string element)
        {
            context._closingtags.Push(element);
        }

        /**
         * <summary>Implement this method in your custom ConcreteParseState subclass.</summary>
         * <returns>The number of chars parsed.</returns>
         * <param name="htmlContent">The HTML to be parsed represented as a string.</param>
         * <param name="context">An instance of IHtmlParser context under which the different ParseStates will be executed.
         * Maintains an instace of a ConcreteIParseState subclass that defines the current state.</param>
         */
        public abstract int ParseHtml(HtmlParser context, string htmlContent);
    }
}