using System;
using System.Collections.Generic;

namespace ConfService.Dto
{
    public class ConferenceDto
    {
        public int Id { get; set; }

        //[StringLength(500)]
        public string Name { get; set; }

        //[StringLength(8000)]
        public string Info { get; set; }
        //[StringLength(500)]
        public string Location { get; set; }
        
        public DateTime DateTimeStartConference { get; set; }
        
        public DateTime DateTimeFinishConference { get; set; }
        
        public ICollection<SectionDto> SectionsDto { get; set; } = new List<SectionDto>();

    }
}