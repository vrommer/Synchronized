namespace Synchronized.ViewModel
{
    /// <summary>
    /// This class contains data to be presented in the edit view.
    /// </summary>
    public class EditViewModel
    {
        public string Id { get; set; }
        public string QuestionId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Tags { get; set; }
    }
}
