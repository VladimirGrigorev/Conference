using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfModel.Interface;

namespace ConfModel.Model
{
    public class AdminOfConference : IId
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ConferenceId { get; set; }
        public Conference Conference { get; set; }
    }
}
