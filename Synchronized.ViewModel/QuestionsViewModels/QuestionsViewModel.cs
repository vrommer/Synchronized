using System;

namespace Synchronized.ViewModel.QuestionsViewModels
{
    public class QuestionsViewModel: QuestionViewModel
    {
        public string Body { get; set; }
        public DateTime DatePosted { get; set; }
        public int Answers { get; set; }
        public int Views { get; set; }
        public string PublisherName { get; set; }
    }
}
