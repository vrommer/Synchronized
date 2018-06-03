using Synchronized.UI.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using Synchronized.ServiceModel;
using Synchronized.SharedLib.Interfaces;
using Synchronized.ViewModel;
using Synchronized.ViewModel.QuestionsViewModels;
using Synchronized.ViewModelFactories.Interfaces;
using Synchronized.SharedLib.Utilities;

namespace Synchronized.UI.Utilities
{
    public class DataConverter : IDataConverter
    {
        private IViewModelFactory _factory;

        public DataConverter(IViewModelFactory  factory)
        {
            _factory = factory;
        }
        public QuestionForHomePage Convert(Question from)
        {
            var to = _factory.GetQuestionForHomePage();

            to.Id = String.Copy(from.Id);
            to.HasAnswers = (from.Answers.Count > 0);
            to.IsAnswered = from.IsAnswered;
            to.Points = from.SumVotes;
            to.Tags = String.Copy(from.Tags);
            to.Title = String.Copy(from.Title);

            return to;
        }

        public List<QuestionForHomePage> Convert(ICollection<Question> from)
        {
            var questions = _factory.GetPaginatedList<QuestionForHomePage>(((PaginatedList<Question>)from).TotalPages, ((PaginatedList<Question>)from).PageIndex, ((PaginatedList<Question>)from).TotalSize);

            foreach (var q in from)
            {
                var question = Convert(q);
                questions.Add(question);
            }

            //from.ForEach(q => {
            //    var question = Convert(q);
            //    questions.Add(question);
            //});

            return questions;
        }

        public AnswerViewModel Convert(Answer from)
        {
            var answer = _factory.GetAnswer();

            answer.Accapted = from.IsAccepted;
            answer.Body = String.Copy(from.Body);
            answer.DatePosted = from.DatePosted;
            answer.Id = String.Copy(from.Id);
            answer.Points = from.SumVotes;
            answer.PublisherId = String.Copy(from.PublisherId);
            answer.PublisherName = String.Copy(from.PublisherName);

            answer.Comments = Convert((List<Comment>)from.Comments);

            return answer;
        }

        public List<AnswerViewModel> Convert(ICollection<Answer> from)
        {
            var answers = _factory.GetAnswers();

            if (from != null)
            {
                foreach (var a in from)
                {
                    answers.Add(((IAnswerConverter)this).Convert(a));
                }
                //from.ForEach(a =>
                //{
                //    answers.Add(((IAnswerConverter)this).Convert(a));
                //});
            }

            return answers;
        }

        public CommentViewModel Convert(Comment from)
        {
            var comment = _factory.GetComment();
            if (from != null) { 
                comment.Body = String.Copy(from.Body);
                comment.DatePosted = from.DatePosted;
                comment.Id = String.Copy(from.Id);
                comment.PublisherId = String.Copy(from.PublisherId);
                comment.PublisherName = String.Copy(from.PublisherName);
                comment.VotedPostId = String.Copy(from.VotedPostId);
            }

            return comment;            
        }

        public List<CommentViewModel> Convert(ICollection<Comment> from)
        {
            var comments = _factory.GetComments();

            if (from != null)
            {
                foreach (var c in from)
                {
                    comments.Add(((ICommentConverter)this).Convert(c));
                }
                //from.ForEach(c =>
                //{
                //    comments.Add(((ICommentConverter)this).Convert(c));
                //});
            }

            return comments;
        }

        QuestionForQuestionsPage IDataConverter<Question, QuestionForQuestionsPage>.Convert(Question from)
        {
            var question = _factory.GetQuestionForQuestionsPage();

            question.AnswersCount = from.Answers.Count;
            question.Body = String.Copy(from.Body);
            question.DatePosted = from.DatePosted;
            question.HasAnswers = (from.Answers.Count > 0);
            question.Id = String.Copy(from.Id);
            question.IsAnswered = from.IsAnswered;
            question.Points = from.SumVotes;
            question.PublisherId = String.Copy(from.PublisherId);
            question.PublisherName = String.Copy(from.PublisherName);
            question.Tags = String.Copy(from.Tags);
            question.Title = String.Copy(from.Title);
            question.ViewsCount = from.Views;

            return question;
        }

        List<QuestionForQuestionsPage> IDataConverter<Question, QuestionForQuestionsPage>.Convert(ICollection<Question> from)
        {
            var questions = _factory.GetPaginatedList<QuestionForQuestionsPage>(((PaginatedList<Question>)from).TotalPages, ((PaginatedList<Question>)from).PageIndex, ((PaginatedList<Question>)from).TotalSize);

            foreach (var q in from)
            {
                QuestionForQuestionsPage question = ((IQuestionsConverter)this).Convert(q);
                questions.Add(question);
            }
            //from.ForEach(q => {
            //    QuestionForQuestionsPage question = ((IQuestionsConverter)this).Convert(q);
            //    questions.Add(question);
            //});

            return questions;
        }

        QuestionForDetailsPage IDataConverter<Question, QuestionForDetailsPage>.Convert(Question from)
        {
            var question = _factory.GetQuestionForDetailsPage();

            question.AnswersCount = from.Answers.Count;
            question.Body = String.Copy(from.Body);
            question.DatePosted = from.DatePosted;
            question.HasAnswers = from.Answers.Count > 0;
            question.Id = String.Copy(from.Id);
            question.IsAnswered = from.IsAnswered;
            question.Points = from.SumVotes;
            question.PublisherId = String.Copy(from.PublisherId);
            question.PublisherName = String.Copy(from.PublisherName);
            question.Tags = String.Copy(from.Tags);
            question.Title = String.Concat(from.Title);
            question.ViewsCount = from.Views;

            question.Answers = Convert((List<Answer>)from.Answers);
            question.Comments = Convert((List<Comment>)from.Comments);

            return question;
        }

        List<QuestionForDetailsPage> IDataConverter<Question, QuestionForDetailsPage>.Convert(ICollection<Question> from)
        {
            throw new NotImplementedException();
        }
    }
}
