using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfModel.Interface;

namespace ConfModel.Model
{
    public class RoleInLecture : IId
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }

        public Role Role { get; set; }
    }
}
