using Synchronized.UI.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using Synchronized.ServiceModel;
using Synchronized.SharedLib.Interfaces;
using Synchronized.ViewModel;
using Synchronized.ViewModel.QuestionsViewModels;
using Synchronized.ViewModelFactories.Interfaces;
using Synchronized.SharedLib.Utilities;
using Synchronized.Core.Factories.Interfaces;

namespace Synchronized.UI.Utilities
{
    public class PostsConverter : IPostsConverter
    {
        private IViewModelFactory _viewModelFacotry;
        private IServiceModelFactory _serviceModelFactory;

        public PostsConverter(IViewModelFactory  viewModelFactory, IServiceModelFactory serviceModelFactory)
        {
            _viewModelFacotry = viewModelFactory;
            _serviceModelFactory = serviceModelFactory;
        }
        public QuestionForHomePage Convert(Question from)
        {
            var to = _viewModelFacotry.GetQuestionForHomePage();

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
            var questions = _viewModelFacotry.GetPaginatedList<QuestionForHomePage>(((PaginatedList<Question>)from).TotalPages, ((PaginatedList<Question>)from).PageIndex, ((PaginatedList<Question>)from).TotalSize);

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
            var answer = _viewModelFacotry.GetAnswer();

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
            var answers = _viewModelFacotry.GetAnswers();

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
            var comment = _viewModelFacotry.GetComment();
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
            var comments = _viewModelFacotry.GetComments();

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

        public Question Convert(QuestionForHomePage from)
        {
            throw new NotImplementedException();
        }

        public List<Question> Convert(ICollection<QuestionForHomePage> from)
        {
            throw new NotImplementedException();
        }

        public Question Convert(QuestionForQuestionsPage from)
        {
            throw new NotImplementedException();
        }

        public List<Question> Convert(ICollection<QuestionForQuestionsPage> from)
        {
            throw new NotImplementedException();
        }

        public Question Convert(QuestionForDetailsPage from)
        {
            throw new NotImplementedException();
        }

        public List<Question> Convert(ICollection<QuestionForDetailsPage> from)
        {
            throw new NotImplementedException();
        }

        public Answer Convert(AnswerViewModel from)
        {
            var answer = _serviceModelFactory.GetAnswer();
            answer.Body = String.Copy(from.Body);
            return answer;
        }

        public List<Answer> Convert(ICollection<AnswerViewModel> from)
        {
            throw new NotImplementedException();
        }

        public Comment Convert(CommentViewModel from)
        {
            throw new NotImplementedException();
        }

        public List<Comment> Convert(ICollection<CommentViewModel> from)
        {
            throw new NotImplementedException();
        }

        public Question Convert(AskViewModel from)
        {
            var question = _serviceModelFactory.GetQuestion();
            question.Body = String.Copy(from.Body);
            question.Title = String.Copy(from.Title);
            question.Tags = String.Copy(from.Tags);
            return question;
        }

        public List<Question> Convert(ICollection<AskViewModel> from)
        {
            throw new NotImplementedException();
        }

        public EditViewModel Convert(VotedPost from)
        {
            var editPostViewModel = _viewModelFacotry.GetOfType<EditViewModel>();
            editPostViewModel.Id = String.Copy(from.Id);
            editPostViewModel.Body = String.Copy(from.Body);
            // If from is of type ServiceModel.Question
            if (from.GetType().Equals(typeof(Question)))
            {
                editPostViewModel.Title = String.Copy(((Question)from).Title);
                editPostViewModel.Tags = String.Copy(((Question)from).Tags);
                editPostViewModel.QuestionId = String.Copy(from.Id);
            }
            else
            {
                editPostViewModel.QuestionId = String.Copy(((Answer)from).QuestionId);
            }
            return editPostViewModel;
        }

        public VotedPost Convert(EditViewModel from)
        {
            throw new NotImplementedException();
        }

        public List<EditViewModel> Convert(ICollection<VotedPost> from)
        {
            throw new NotImplementedException();
        }

        public List<VotedPost> Convert(ICollection<EditViewModel> from)
        {
            throw new NotImplementedException();
        }

        QuestionForQuestionsPage IDataConverter<Question, QuestionForQuestionsPage>.Convert(Question from)
        {
            var question = _viewModelFacotry.GetQuestionForQuestionsPage();

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
            var questions = _viewModelFacotry.GetPaginatedList<QuestionForQuestionsPage>(((PaginatedList<Question>)from).TotalPages, ((PaginatedList<Question>)from).PageIndex, ((PaginatedList<Question>)from).TotalSize);

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
            var question = _viewModelFacotry.GetQuestionForDetailsPage();

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

        AskViewModel IDataConverter<Question, AskViewModel>.Convert(Question from)
        {
            throw new NotImplementedException();
        }

        List<AskViewModel> IDataConverter<Question, AskViewModel>.Convert(ICollection<Question> from)
        {
            throw new NotImplementedException();
        }
    }
}
