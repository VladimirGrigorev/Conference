using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public int? LectureId { get; set; }
        public Lecture Lecture { get; set; }

        public int? ApplicationId { get; set; }
        public Application Application { get; set; }

        [NotMapped]
        public bool IsNew { get; set; }

        public ICollection<MessageNotification> MessageNotifications { get; set; } = new List<MessageNotification>();
    }
}
