using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    public class ApplicationRepository: BaseRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(ConfContext confContext) : base(confContext){}
    }
}