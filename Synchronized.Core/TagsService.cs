//using Synchronized.Core.Interfaces;
//using Synchronized.Domain;
//using Synchronized.SharedLib.Utilities;
//using System.Threading.Tasks;
//using Synchronized.Repository.Interfaces;

//namespace Synchronized.Core
//{
//    public class TagsService : DataService<ServiceModel.Post, Tag>, ITagsService
//    {
//        private readonly ITagsRepository _tagsRepo;

//        public TagsService(ITagsRepository repo) : base(repo)
//        {
//            _tagsRepo = repo;
//        }

//        public async Task<Tag> FindTagByName(string name)
//        {
//            return await _tagsRepo.FindTagByName(name);
//        }

//        public async Task<PaginatedList<Tag>> GetTagsPageAsync(int pageIndex, int pageSize)
//        {
//            var tags = await _tagsRepo.GetTagsPageAsync(pageIndex, pageSize);
//            int count = await _repo.GetCount();
//            return new PaginatedList<Tag>(tags, count, pageIndex, pageSize);
//        }
//    }
//}
