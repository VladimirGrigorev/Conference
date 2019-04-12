using System;
using System.Collections.Generic;
using System.Text;
using ConfService.Dto;

namespace ConfService.Interface
{
    interface ISectionService
    {
        SectionDto Get(int id);
        IEnumerable<SectionDto> GetAll();
        int Add(SectionDto sectionDto);
    }
}
