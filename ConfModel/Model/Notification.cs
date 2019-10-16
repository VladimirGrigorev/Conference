using ConfModel.Interface;

namespace ConfModel.Model
{
    public class Notification : IId
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
    }

    public class ApplicationNotification : Notification
    {
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
    }

    public class FileNotification : Notification
    {
        public int FileId { get; set; }
        public File File { get; set; }
    }

    public class MessageNotification : Notification
    {
        public int MessageId { get; set; }
        public Message Message { get; set; }
    }
}