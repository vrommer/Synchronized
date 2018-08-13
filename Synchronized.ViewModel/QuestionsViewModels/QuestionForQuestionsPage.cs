using System;

namespace Synchronized.ViewModel.QuestionsViewModels
{
    /// <summary>
    /// This Class represents a Question in the Questions Index View Context.
    /// </summary>
    public class QuestionForQuestionsPage: QuestionForHomePage
    {
        public string Body { get; set; }
        public DateTime DatePosted { get; set; }
        public int AnswersCount { get; set; }
        public int ViewsCount { get; set; }
        public string PublisherName { get; set; }
        public string PublisherId { get; set; }
    }
}
