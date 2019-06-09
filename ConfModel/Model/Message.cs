using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ConfModel.Interface;

namespace ConfModel.Model
{
    public class Message : IId
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Text { get; set; }
         
        public DateTime DateTimeSent { get; set; }  = DateTime.Now;

        public int UserId { get; set; }
        public User User { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public ICollection<MessageNotification> MessageNotifications { get; set; } = new List<MessageNotification>();
    }
}
