using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Conference.Model
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(500)]
        public string PassHash { get; set; }

        public SexType Sex { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
