using System.Collections.Generic;
using System.Linq;
using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    public class SectionExpertRepository : BaseRepository<SectionExpert>, ISectionExpertRepository
    {
        public SectionExpertRepository(ConfContext confContext) : base(confContext){}

        public IEnumerable<int> GetExpertIds(int sectionId)
        {
            return Set.Where(e => e.SectionId == sectionId).Select(e => e.UserId);
        }
    }
}