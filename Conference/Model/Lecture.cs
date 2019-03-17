using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Conference.Model
{
    public class Lecture
    {
        public int Id { get; set; }

        public string TopicLecture { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeLecture { get; set; }

        public int SectionID { get; set; }
        public Section Section { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeOpenChat { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeCloseChat { get; set; }

        public ICollection<RoleInLecture> RoleInLectures { get; set; } = new List<RoleInLecture>();

        public ICollection<File> Lectures { get; set; } = new List<File>();

        public ICollection<Message> Messages { get; set; } = new List<Message>();

    }
}
