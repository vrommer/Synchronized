using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace UtilsLib.HtmlUtils
{
    [EventSource(Name = "HtmlParser")]
    class HtmlParserEventSource : EventSource
    {
        public class Keywords
        {
            public const EventKeywords Parser = (EventKeywords)1;
        }

        public class Tasks
        {
            public const EventTask Sanity = (EventTask)1;
        }

        [Event(1, Message = "Setting up logger.", Level = EventLevel.Informational, Keywords = Keywords.Parser)]
        public void Sanity() { WriteEvent(1); }

        [Event(2, Message = "Opening file {0} for logging.", Level = EventLevel.Informational, Keywords = Keywords.Parser)]
        public void OpenFileStart(string fileName) { WriteEvent(2, fileName); }

        [Event(3, Message = "File opened.", Level = EventLevel.Informational, Keywords = Keywords.Parser)]
        public void OpenFileStop() { WriteEvent(3); }

        public static HtmlParserEventSource logger = new HtmlParserEventSource();
    }
}
