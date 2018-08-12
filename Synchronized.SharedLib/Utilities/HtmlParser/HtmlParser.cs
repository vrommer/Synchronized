using System;
using System.Collections.Generic;
using UtilsLib.HtmlUtils.HtmlParser.ParserStates;

namespace UtilsLib.HtmlUtils.HtmlParser
{
    public enum StateType
    {
        ReadingHtml,
        EndOfFile,
        ReadingOpeningTag,
        ReadingClosingTag,
        ReadingEntity,
        ReadingHtmlEntity
        // Your Custom StateType
    }

    public class HtmlParser : IHtmlParser
    {

        /*********************************************
         * Fields
         *********************************************/
        public IParserState _state { get; set; }
        public Dictionary<int, IParserState> _states { get; set; }
        public Stack<string> _openingTags { get; set; }
        public Stack<string> _closingtags { get; set; }
        public string _currentContent { get; set; }

        /*********************************************
         * Constructors
         *********************************************/
        public HtmlParser()
        {
            _currentContent = "";
            _states = new Dictionary<int, IParserState>
            {
                { (int)StateType.ReadingHtml, new ReadingHtml() },
                { (int)StateType.ReadingOpeningTag, new ReadingOpeningTag() },
                { (int)StateType.ReadingClosingTag, new ReadingClosingTag() },
                { (int)StateType.ReadingHtmlEntity, new ReadingHtmlEntity() }                
                // Add your new ParseState to the states Dictionary
            };            
        }

        public void ChangeState(IParserState state)
        {
            _state = state;
        }

        public IParserState GetState(StateType state)
        {
            return _states[(int)state];
        }

        public string UtilizeHtml(string htmlContent)
        {
            try
            {
                _currentContent = String.Copy(htmlContent);
            }
            catch (ArgumentNullException e)
            {
                throw e;
            }
            _openingTags = new Stack<string>();
            _closingtags = new Stack<string>();
            _state = _states[(int)StateType.ReadingHtml];

            string content;
            int charsParsed = 0;

            while (charsParsed < htmlContent.Length)
            {
                content = htmlContent.Substring(charsParsed, htmlContent.Length - charsParsed);
                charsParsed += _state.ParseHtmlContent(this, content);
            }

            // Add custom step to return message.
            return RemoveHtmlEntityFromTale()
                .AddMissingClosingTags()
                .AddMissingOpeningTags()
                .ToString();
        }

        private HtmlParser AddMissingClosingTags()
        {          
            while (_openingTags.Count != 0)
            {
                _currentContent += "<" + _openingTags.Pop().Insert(0, "/") + ">";
            }
            return this;
        }

        private HtmlParser RemoveHtmlEntityFromTale()
        {            
            string tnetnoClmth = Utilities.ReverseString(_currentContent);
            int startIndex;
            if (_state.GetType().Equals(typeof(ReadingHtmlEntity)))
            {
                startIndex = tnetnoClmth.IndexOf("&") + 1;
                _currentContent = Utilities.ReverseString(tnetnoClmth.Substring(startIndex, tnetnoClmth.Length - startIndex));
                return this;
            }
            return this;
        }

        private HtmlParser AddMissingOpeningTags()
        {
            // TODO: Implement method for fixing inconsistent closing tags.            
            if (_closingtags.Count > 0)
            {
                _closingtags = new Stack<string>();
                throw new NotImplementedException();
            }
            return this;
        }

        public override string ToString()
        {
            return _currentContent;
        }
    }
}