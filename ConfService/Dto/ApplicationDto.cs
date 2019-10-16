using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ConfModel.Model;

namespace ConfService.Dto
{
    public class ApplicationDto
    {
        public int Id { get; set; }
        [StringLength(300)]
        [Required]
        public string Topic { get; set; }

        [StringLength(300)]
        [Required]
        public string Authors { get; set; }

        [StringLength(300)]
        public string Keywords { get; set; }

        [StringLength(8000)]
        [Required]
        public string Info { get; set; }

        public int SectionId { get; set; }

        public int UserId { get; set; }

        public string SectionName { get; set; }
        public string ConferenceName { get; set; }

        public bool IsNew { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }

        //public ICollection<FileDto> Files { get; set; } = new List<FileDto>();
        
        //public ICollection<MessageDto> Messages { get; set; } = new List<MessageDto>();
    }

    public class ApplicationStatDto
    {
        public int Id { get; set; }
        
        public ApplicationStatus ApplicationStatus { get; set; }
    }
}