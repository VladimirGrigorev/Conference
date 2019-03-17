using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Conference.Model
{
    public class Message
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Text { get; set; }
         
        public DateTime DateTimeSent { get; set; }  = DateTime.Now;

        public int UserId { get; set; }
        public User User { get; set; }

        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }
    }
}
