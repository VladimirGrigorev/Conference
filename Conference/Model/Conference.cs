﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Conference.Model
{
    public class Conference
    {
        public int ConferenceId { get; set; }
        public string ConferenceName { get; set; }
        public string Location { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeStartConference { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeFinishConference { get; set; }

        public int ThemeConferenced { get; set; }
        public ThemeSection ThemeConference { get; set; }

        public ICollection<AdminOfConference> AdminOfConferences { get; set; } = new List<AdminOfConference>();
    }
}
