using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ConfModel.Interface;

namespace ConfModel.Model
{
    public class File : IId
    {
        public int Id { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        public double Size { get; set; }

        public bool Private { get; set; }

        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }
    }
}
