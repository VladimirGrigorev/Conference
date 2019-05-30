using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    public class InfoPageRepository: BaseRepository<InfoPage>, IInfoPageRepository
    {
        public InfoPageRepository(ConfContext confContext) : base(confContext){}
    }
}