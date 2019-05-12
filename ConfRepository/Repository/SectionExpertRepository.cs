using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    public class SectionExpertRepository : BaseRepository<SectionExpert>, ISectionExpertRepository
    {
        public SectionExpertRepository(ConfContext confContext) : base(confContext)
        {
        }
    }
}