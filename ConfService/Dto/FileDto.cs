using System.ComponentModel.DataAnnotations;

namespace ConfService.Dto
{
    public class FileDto
    {
        public int Id { get; set; }

        [StringLength(300)]
        public string Name { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        [StringLength(200)]
        public string TempName { get; set; }

        public double Size { get; set; }

        public bool IsNew { get; set; }

        public int ApplicationId { get; set; }
    }
}