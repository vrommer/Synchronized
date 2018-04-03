using Synchronized.Model;
using System;
using System.Collections.Generic;
using UtilsLib.HtmlUtils;

namespace Synchronized.Core.Utilites
{
    public class Utils
    {
        public static void MinimizeContent(IHtmlUtils parser, List<Question> questions)
        {
            foreach (Question q in questions)
            {
                int contentLength = q.Content.Length;
                int substringLength = Math.Min(512, contentLength);
                q.Content = parser.UtilizeHtml(q.Content.Substring(0, substringLength));
            }
        }
    }
}
