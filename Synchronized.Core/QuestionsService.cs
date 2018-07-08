using Synchronized.Core.Interfaces;
using Synchronized.Repository.Interfaces;
using Synchronized.Core.Utilites;
using Synchronized.Core.Utilities.Interfaces;
using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;
using UtilsLib.HtmlUtils.HtmlParser;
using Synchronized.Core.Factories.Interfaces;
using System.Collections.Generic;
using SharedLib.Infrastructure.Constants;
using Synchronized.Domain;
using System;
using Synchronized.ViewModel;
using Microsoft.Extensions.Logging;
using Synchronized.SharedLib;
using Synchronized.ServiceModel.Interfaces;

namespace Synchronized.Core
{
    public class QuestionsService : PostsService<Question, ServiceModel.Question>, IQuestionsService
    {
        private HtmlParser _parser;

        public QuestionsService(IQuestionsRepository repo, IServiceModelFactory factory, IDataConverter converter, HtmlParser parser, 
            ILogger<QuestionsService> logger) : base(repo, factory, converter, logger)
        {
            _parser = parser;
        }

        public async Task<PaginatedList<ServiceModel.Question>> GetPage(int pageIndex, int pageSize)
        {
            return await GetQuestionsPage(pageIndex, pageSize);
        }

        public async override Task<PaginatedList<ServiceModel.Question>> GetPage(int pageIndex, int pageSize, string sortOrder, string searchString)
        {
            return await GetQuestionsPage(pageIndex, pageSize, sortOrder, searchString);
        }

        public async override Task<ServiceModel.Question> GetById(string id)
        {
            var domainQuestion = await ((IQuestionsRepository)_repo).GetById(id);
            var question = _converter.Convert(domainQuestion);
            return question;
        }

        private async Task<PaginatedList<ServiceModel.Question>> GetQuestionsPage(int pageIndex, int pageSize, string sortOrder = null, string searchString = null)
        {
            List<Question> domainQuestions; ;
            if (sortOrder == null)
            {
                domainQuestions = await ((IQuestionsRepository)_repo).GetPageAsync(pageIndex, pageSize);
            } else
            {
                domainQuestions = ((IQuestionsRepository)_repo).GetPage(pageIndex, pageSize, sortOrder, searchString);
            }
            var questions = _converter.Convert(domainQuestions);
            if (sortOrder != null)
            {
                Utils.MinimizeContent(_parser, questions);
            }
            var questionsPage = _factory.GetQuestionsList(questions, _repo.GetCount(), pageSize, pageIndex);
            return questionsPage;
        }

        public async Task<ServiceModel.Question> VoteForQuestion(string postId, VoteType voteType, ApplicationUser user)
        {
            var question = await ((IQuestionsRepository)_repo).GetQuestionById(postId);
            var serviceQuestion = _converter.Convert(question);
            var canVote = CanVote(user.Id, user.Points, voteType, serviceQuestion);
            if (canVote)
            {
                switch (voteType)
                {
                    case VoteType.UpVote:
                        serviceQuestion.UpVotes++;
                        question.Publisher.Points += Constants.QUESTION_UPVOTE_ASKER_BONUS;
                        break;
                    default:
                        serviceQuestion.DownVotes++;
                        question.Publisher.Points += Constants.QUESTION_DOWNVOTE_AKSER_PENALTY;
                        if (user.Id.Equals(question.Publisher.Id))
                        {
                            user.Points += question.Publisher.Points + Constants.QUESTION_DOWNVOTE_VOTER_PENALTY;
                        }
                        else
                        {
                            user.Points += Constants.QUESTION_DOWNVOTE_VOTER_PENALTY;
                        }
                        break;
                }
                serviceQuestion.VoterIds.Add(user.Id);
                question.Votes.Add(new Vote
                {
                    VoterId = user.Id,
                    VoteType = (int)voteType
                });

                await _repo.UpdateAsync(question);
                await _repo.UpdateAsync(question.Publisher);
                await _repo.UpdateAsync(user);
                await serviceQuestion.Notify();
                return serviceQuestion;
            }
            return null;
        }

        private bool CanVote(string userId, int userPoints, VoteType voteType, ServiceModel.VotedPost post)
        {
            if (string.IsNullOrEmpty(userId) || post == null)
            {
                return false;
            }
            // If user upvotes and can upvote or user downvotes and can downvote
            if (((voteType == VoteType.UpVote) && (Constants.VOTE_UP_POINTS <= userPoints)) ||
                ((voteType == VoteType.DownVote) && (Constants.VOTE_DOWN_POINTS <= userPoints)))
            {
                return !post.VoterIds.Contains(userId);
            }
            return false;
        }

        private bool CanView(string userId, ServiceModel.Question question)
        {
            if (string.IsNullOrEmpty(userId) || question == null)
            {
                return false;
            }
            return !question.ViewerIds.Contains(userId);
        }

        /// <summary>
        /// Vote for answer. UpVote or DownVote an Answer. Each user may Vote only once per Post.
        /// </summary>
        /// <param name="postId">The Id of the Answer</param>
        /// <param name="voteType">One of { UpVote, DownVote }</param>
        /// <param name="userId">THe Id of the voter</param>
        /// <returns></returns>
        public async Task<ServiceModel.Answer> VoteForAnswer(string postId, VoteType voteType, ApplicationUser user)
        {
            var answer = await ((IQuestionsRepository)_repo).GetAnswerById(postId);
            var serviceAnswer = _converter.Convert(answer);

            var canVote = CanVote(user.Id, user.Points, voteType, serviceAnswer);
            if (canVote)
            {
                var domainQuestion = await ((IQuestionsRepository)_repo).GetQuestionById(answer.QuestionId);
                var serviceQuestion = _converter.Convert(domainQuestion);
                answer.Votes.Add(new Vote
                {
                    VoterId = user.Id,
                    VoteType = (int)voteType
                });
                await ((IQuestionsRepository)_repo).UpdateAnswerAsync(answer);
                serviceAnswer.VoterIds.Add(user.Id);
                switch (voteType)
                {
                    case VoteType.UpVote:
                        serviceAnswer.UpVotes++;
                        answer.Publisher.Points += Constants.ANSWER_UPVOTE_ANSWERER_BONUS;
                        break;
                    default:
                        serviceAnswer.DownVotes++;
                        answer.Publisher.Points += Constants.ANSWER_DOWNVOTE_ANSWERER_PNEALTY;
                        if (user.Id.Equals(answer.Publisher.Id))
                        {
                            user.Points += answer.Publisher.Points + Constants.ANSWER_DOWNVOTE_VOTER_PENALTY;
                        }
                        else
                        {
                            user.Points += Constants.ANSWER_DOWNVOTE_VOTER_PENALTY;
                        }
                        break;
                }
                await _repo.UpdateAsync(answer);
                await _repo.UpdateAsync(answer.Publisher);
                await _repo.UpdateAsync(user);
                await serviceQuestion.Notify();
                return serviceAnswer;
            }
            return null;
        }

        public async Task<ServiceModel.Question> ViewQuestion(string questionId, string userId)
        {
            var question = await ((IQuestionsRepository)_repo).GetQuestionById(questionId);
            var serviceQuestion = _converter.Convert(question);
            var canView = CanView(userId, serviceQuestion);
            if (canView)
            {
                question.QuestionViews.Add(new Domain.QuestionView
                {
                    UserId = userId
                });
                await _repo.UpdateAsync(question);
                serviceQuestion.VoterIds.Add(userId);
                serviceQuestion.Views++;
            }
            return serviceQuestion;
        }

        public async Task<string> AskQuestion(ServiceModel.Question question)
        {
            if (question.PublisherId == null)
            {
                return null;
            }
            if (!await TagsAreValid(question.Tags))
            {
                return null;
            }
            var subscriber = _factory.GetUser();
            subscriber.Id = question.PublisherId;
            question.Subscribers = _factory.GetOfType<List<IQuestionSubscriber>>();
            question.Subscribers.Add(subscriber);
            var domainQuestion = _converter.Convert(question);
            try
            {
                var questionId = await ((IQuestionsRepository)_repo).AddAsync(domainQuestion);
                return questionId;
            } catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> TagsAreValid(string tags)
        {
            var tagNamesArray = tags.Split(",");
            for (int i = 0; i < tagNamesArray.Length; i++)
            {
                var tagIsValid = await TagIsValidAsync(tagNamesArray[i]);
                if (!tagIsValid)
                {
                    return false;
                }
            }
            return true;
        }

        private async Task<bool> TagIsValidAsync(string tagId)
        {
            return await ((IQuestionsRepository)_repo).GetQuestionTagById(tagId) != null;
        }

        public async Task AnswerQuestion(ServiceModel.Answer answer, string questionId)
        {
            var question = await ((IQuestionsRepository)_repo).GetQuestionById(questionId);
            var domainAnswer = _converter.Convert(answer);
            question.Answers.Add(domainAnswer);
            await ((IQuestionsRepository)_repo).UpdateAsync(question);
        }

        public async Task AcceptAnswer(AnswerViewModel answer, ApplicationUser user)
        {
            var canAccept = ((!String.IsNullOrEmpty(user.Id)) && (answer.QuestionPublisherId.Equals(user.Id)));
            if (canAccept)
            {
                var domainAnswer = await ((IQuestionsRepository)_repo).GetAnswerById(answer.Id);
                var domainQuestion = await ((IQuestionsRepository)_repo).GetQuestionById(answer.QuestionId);
                var questionPublisher = _converter.Convert(domainQuestion);
                domainAnswer.IsAccepted = true;
                domainAnswer.Publisher.Points += Constants.ANSWER_ACCEPT_ANSWERER_BONUS;
                user.Points += Constants.ANSWER_ACCEPT_ACCEPTER_BONUS;
                await _repo.UpdateAsync(domainAnswer);
                await _repo.UpdateAsync(user);
                await questionPublisher.Notify();

            }               
        }
    } 
}
