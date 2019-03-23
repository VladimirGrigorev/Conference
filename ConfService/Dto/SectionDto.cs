using System.Collections.Generic;

namespace ConfService.Dto
{
    public class SectionDto
    {
        public int Id { get; set; }
        //[StringLength(200)]
        public string Name { get; set; }

        //[StringLength(8000)]
        public string Info { get; set; }

        public int ConferenceDtoId { get; set; }

        public ICollection<LectureDto> LecturesDto { get; set; } = new List<LectureDto>();
    }
}