using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ConfModel.Interface;

namespace ConfModel.Model
{
    public class Section : IId
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(8000)]
        public string Info { get; set; }

        public int ConferenceId { get; set; }
        public Conference Conference{ get; set; }

        public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
    }
}
