﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conference.Model
{
    public class Section
    {
        public int Id { get; set; }

        public string SectionName { get; set; }
        
        public int ConferenceId { get; set; }
        public Conference Conference{ get; set; }

        public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
    }
}
