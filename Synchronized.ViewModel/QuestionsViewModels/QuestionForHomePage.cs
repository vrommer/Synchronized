namespace Synchronized.ViewModel.QuestionsViewModels
{
    public class QuestionForHomePage
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Points { get; set; }
        public string Tags { get; set; }
        public bool IsAnswered { get; set; }
        public bool HasAnswers { get; set; }
    }
}
