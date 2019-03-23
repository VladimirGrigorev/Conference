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
        protected readonly IConferenceRepository _iConferenceRepository;
        protected readonly IMapper _iMapper;

        public ConferenceService(IConferenceRepository repositoy, IMapper mapper)
        {
            _iConferenceRepository = repositoy;
            _iMapper = mapper;
        }
        public ConferenceDto Get(int id)
        {
            return _iMapper.Map<ConferenceDto>(_iConferenceRepository.Get(id));
        }

        public IEnumerable<ConferenceDto> GetAll()
        {
            return _iMapper.Map<IEnumerable<ConferenceDto>>(_iConferenceRepository.GetAll());
        }

        public int Add(ConferenceDto conferenceDto)
        {
            var conference = _iMapper.Map<Conference>(conferenceDto);
            return _iConferenceRepository.Add(conference);
        }
    }
}
