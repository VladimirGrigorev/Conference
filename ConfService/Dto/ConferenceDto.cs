using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfService.Dto
{
    public class ConferenceDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        [StringLength(8000)]
        public string Info { get; set; }
        [StringLength(500)]
        public string Location { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateTimeStart { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateTimeFinish { get; set; }
        
        public ICollection<SectionDto> SectionsDto { get; set; } = new List<SectionDto>();

    }
}