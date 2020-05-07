using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ConfModel.Interface;

namespace ConfModel.Model
{
    public class Lecture : IId
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string Topic { get; set; }

        [StringLength(8000)]
        public string Info { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeStart { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeOpenChat { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeCloseChat { get; set; }

        public ICollection<RoleInLecture> RoleInLectures { get; set; } = new List<RoleInLecture>();

        public ICollection<Message> Messages { get; set; } = new List<Message>();

        //public ICollection<File> Files { get; set; } = new List<File>();

        //public ICollection<LecMessage> Messages { get; set; } = new List<LecMessage>();

    }
}
