using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ConfModel.Interface;

namespace ConfModel.Model
{
    public class Conference : IId
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(8000)]
        public string Info { get; set; }
        [StringLength(500)]
        public string Location { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeStartConference { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeFinishConference { get; set; }

        public ICollection<AdminOfConference> AdminOfConferences { get; set; } = new List<AdminOfConference>();

        public ICollection<Section> Sections { get; set; } = new List<Section>();
    }
}
