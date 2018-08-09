namespace UtilsLib.HtmlUtils
{
    public interface IHtmlParser
    {
        // Returns a valid html string based on the input html string. Fixes errors if possible and adds missing closing tags
        string UtilizeHtml(string htmlContent);
    }
}
