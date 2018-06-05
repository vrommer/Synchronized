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
using Synchronized.ServiceModel;
using System;

namespace Synchronized.Core
{
    public class QuestionsService : PostsService<Domain.Question, ServiceModel.Question>, IQuestionsService
    {
        private HtmlParser _parser;

        public QuestionsService(IQuestionsRepository repo, IServiceModelFactory factory, IDataConverter converter, HtmlParser parser) : base(repo, factory, converter)
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
            List<Domain.Question> domainQuestions; ;
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

        public async Task<ServiceModel.Question> VoteForQuestion(string postId, VoteType voteType, string userId)
        {
            var question = await ((IQuestionsRepository)_repo).GetQuestionById(postId);
            var serviceQuestion = _converter.Convert(question);
            var canVote = CanVote(userId, serviceQuestion);
            if (canVote)
            {
                question.Votes.Add(new Vote
                {
                    VoterId = userId,
                    VoteType = (int)voteType
                });
                await _repo.UpdateAsync(question);
                serviceQuestion.VoterIds.Add(userId);
                switch(voteType)
                {
                    case VoteType.UpVote:
                        serviceQuestion.UpVotes++;
                        break;
                    default:
                        serviceQuestion.DownVotes++;
                        break;
                }                
            }
            return serviceQuestion;
        }

        private bool CanVote(string userId, ServiceModel.VotedPost post)
        {
            if (string.IsNullOrEmpty(userId) || post == null)
            {
                return false;
            }
            return !post.VoterIds.Contains(userId);
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
        public async Task<ServiceModel.Answer> VoteForAnswer(string postId, VoteType voteType, string userId)
        {
            var answer = await ((IQuestionsRepository)_repo).GetAnswerById(postId);
            var serviceAnswer = _converter.Convert(answer);
            var canVote = CanVote(userId, serviceAnswer);
            if (canVote)
            {
                answer.Votes.Add(new Vote
                {
                    VoterId = userId,
                    VoteType = (int)voteType
                });
                await ((IQuestionsRepository)_repo).UpdateAnswerAsync(answer);
                serviceAnswer.VoterIds.Add(userId);
                switch (voteType)
                {
                    case VoteType.UpVote:
                        serviceAnswer.UpVotes++;
                        break;
                    default:
                        serviceAnswer.DownVotes++;
                        break;
                }
            }
            return serviceAnswer; 
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
    }
}
