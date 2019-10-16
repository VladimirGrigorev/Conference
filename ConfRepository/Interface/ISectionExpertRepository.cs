using System.Collections.Generic;
using ConfModel.Model;

namespace ConfRepository.Interface
{
    public interface ISectionExpertRepository: IRepository<SectionExpert>
    {
        IEnumerable<int> GetExpertIds(int sectionId);
    }
}