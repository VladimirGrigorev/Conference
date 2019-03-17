using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Conference.Model
{
    public class File
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public double Size { get; set; }

        public bool Private { get; set; }

        public int LectureID { get; set; }
        public Lecture Lecture { get; set; }
    }
}
