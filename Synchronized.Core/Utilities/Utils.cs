using Synchronized.ServiceModel;
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
                int contentLength = q.Body.Length;
                int substringLength = Math.Min(512, contentLength);
                q.Body = parser.UtilizeHtml(q.Body.Substring(0, substringLength));
            }
        }
    }
}
