namespace UtilsLib.HtmlUtils
{
    public interface IHtmlUtils
    {
        // Returns a valid html string based on the input html string. Fixes errors if possible and adds missing closing tags
        string UtilizeHtml(string htmlContent);
    }
}
