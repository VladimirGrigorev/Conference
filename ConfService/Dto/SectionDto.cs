using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfService.Dto
{
    public class SectionDto
    {
        public int Id { get; set; }
        [StringLength(200)]
        [Required]
        public string Name { get; set; }

        [StringLength(8000)]
        public string Info { get; set; }
        [Required]
        public int ConferenceId { get; set; }

        public ICollection<LectureDto> Lectures { get; set; } = new List<LectureDto>();

        public ICollection<UserInfoDto> Experts { get; set; } = new List<UserInfoDto>();
    }
}