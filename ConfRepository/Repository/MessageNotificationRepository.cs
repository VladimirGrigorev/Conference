using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    public class MessageNotificationRepository : BaseRepository<MessageNotification>, IMessageNotificationRepository
    {
        public MessageNotificationRepository(ConfContext confContext) : base(confContext)
        {
        }
    }
}