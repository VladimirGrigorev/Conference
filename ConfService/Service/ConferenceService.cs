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
    public class ConferenceService: IConferenceService
    {
        protected readonly IConferenceRepository _conferenceRepository;
        protected readonly IMapper _mapper;

        public ConferenceService(IConferenceRepository repositoy, IMapper mapper)
        {
            _conferenceRepository = repositoy;
            _mapper = mapper;
        }
        public ConferenceDto Get(int id)
        {
            return _mapper.Map<ConferenceDto>(_conferenceRepository.Get(id));
        }

        public IEnumerable<ConferenceDto> GetAll()
        {
            return _mapper.Map<IEnumerable<ConferenceDto>>(_conferenceRepository.GetAll());
        }

        public int Add(ConferenceDto conferenceDto)
        {
            var conference = _mapper.Map<Conference>(conferenceDto);
            return _conferenceRepository.Add(conference);
        }
    }
}
