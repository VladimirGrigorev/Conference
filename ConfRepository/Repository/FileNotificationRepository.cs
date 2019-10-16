using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    public class FileNotificationRepository : BaseRepository<FileNotification>, IFileNotificationRepository
    {
        public FileNotificationRepository(ConfContext confContext) : base(confContext){}
    }
}