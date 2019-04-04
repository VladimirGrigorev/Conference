using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfService.Dto
{
    public class LectureDto
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string Topic { get; set; }

        [StringLength(8000)]
        public string Info { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeStart { get; set; }

        public int SectionId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeOpenChat { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeCloseChat { get; set; }
        
        public ICollection<FileDto> FilesDto { get; set; } = new List<FileDto>();
    }
}