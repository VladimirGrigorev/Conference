using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conference.Model
{
    public class AdminOfConference
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User;

        public int ConferenceId { get; set; }
        public Conference Conference { get; set; }
    }
}
