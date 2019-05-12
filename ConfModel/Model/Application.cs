using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ConfModel.Interface;

namespace ConfModel.Model
{
    /// <summary>
    /// Заявка
    /// </summary>
    public class Application: IId
    {
        public int Id { get; set; }
        [StringLength(300)]
        public string Topic { get; set; }

        [StringLength(300)]
        public string Authors { get; set; }

        [StringLength(300)]
        public string Keywords { get; set; }

        [StringLength(8000)]
        public string Info { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }

        public ICollection<File> Files { get; set; } = new List<File>();
        
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}