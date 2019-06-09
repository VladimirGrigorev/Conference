using ConfModel.Model;

namespace ConfRepository.Interface
{
    public interface IApplicationNotificationRepository : IRepository<ApplicationNotification>
    {
        void DeleteRelatedNotifications(int userId, int appId);
    }
}