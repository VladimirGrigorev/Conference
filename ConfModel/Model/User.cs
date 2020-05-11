﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ConfModel.Interface;

namespace ConfModel.Model
{
    public class User : IId
    {   
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(500)]
        public string Password { get; set; }

        public bool IsGlobalAdmin { get; set; } = false;


        public ICollection<Message> Messages { get; set; } = new List<Message>();

        public ICollection<Application> Applications { get; set; } = new List<Application>();

        public ICollection<RoleInLecture> RoleInLectures { get; set; } = new List<RoleInLecture>();

        public ICollection<AdminOfConference> AdminOfConferences { get; set; } = new List<AdminOfConference>();

        public ICollection<SectionExpert> SectionExperts { get; set; } = new List<SectionExpert>();
    }
}
