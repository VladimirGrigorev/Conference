using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conference.Model
{
    public class RoleInLecture
    {
        public int Id { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public int LectureID { get; set; }
        public Lecture Lecture { get; set; }

        public Role Role { get; set; }
    }
}
