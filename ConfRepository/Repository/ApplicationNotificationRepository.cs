using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    public class ApplicationNotificationRepository : BaseRepository<ApplicationNotification>, IApplicationNotificationRepository
    {
        public ApplicationNotificationRepository(ConfContext confContext) : base(confContext)
        {
        }
    }
}