using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfService.Dto
{
    public class ApplicationDto
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

        public int UserId { get; set; }

        public ICollection<FileDto> Files { get; set; } = new List<FileDto>();

        //public ICollection<MessageDto> Messages { get; set; } = new List<MessageDto>();
    }
}