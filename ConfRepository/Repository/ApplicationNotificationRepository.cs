using System.Linq;
using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    public class ApplicationNotificationRepository : BaseRepository<ApplicationNotification>, IApplicationNotificationRepository
    {
        public ApplicationNotificationRepository(ConfContext confContext) : base(confContext){}

        public void DeleteRelatedNotifications(int userId, int appId)
        {
            Set.RemoveRange(Set.Where(n=>n.ApplicationId == appId && n.UserId == userId));
            _context.SaveChanges();
        }
    }
}