using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ConfModel.Model;
using ConfRepository.Interface;
using ConfService.Dto;
using ConfService.Interface;


namespace ConfService.Service
{
    class SectionService
    {
        protected readonly ISectionRepository _sectionRepository;
        protected readonly IMapper _mapper;

        public SectionService(ISectionRepository repositoy, IMapper mapper)
        {
            _sectionRepository = repositoy;
            _mapper = mapper;
        }
        public SectionDto Get(int id)
        {
            return _mapper.Map<SectionDto>(_sectionRepository.Get(id));
        }

        public IEnumerable<SectionDto> GetAll()
        {
            return _mapper.Map<IEnumerable<SectionDto>>(_sectionRepository.GetAll());
        }

        public int Add(SectionDto sectionDto)
        {
            var conference = _mapper.Map<Section>(sectionDto);
            return _sectionRepository.Add(conference);
        }
    }
}
