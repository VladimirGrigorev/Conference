using System.ComponentModel.DataAnnotations;

namespace ConfService.Dto
{
    public class FileDto
    {
        public int Id { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        public double Size { get; set; }

        public bool Private { get; set; }

        public int LectureId { get; set; }
    }
}