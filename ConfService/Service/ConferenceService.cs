using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using ConfModel.Model;
using ConfRepository.Interface;
using ConfService.Dto;
using ConfService.Interface;
using ConfService.ServiceException;

namespace ConfService.Service
{
    public class ConferenceService: IConferenceService
    {
        protected readonly IConferenceRepository _conferenceRepository;
        private readonly IUserRepository _userRepository;
        protected readonly IMapper _mapper;

        public ConferenceService(IConferenceRepository repositoy, IUserRepository userRepository, IMapper mapper)
        {
            _conferenceRepository = repositoy;
            _userRepository = userRepository;
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

        public int Add(int userId, ConferenceDto conferenceDto)
        {
            if (CheckUserPermission(userId))
            {
                var conference = _mapper.Map<Conference>(conferenceDto);
                //foreach (var admin in conferenceDto.Admins)//.GroupBy(s => s.Id).FirstOrDefault()
                //{
                //    conference.AdminOfConferences.Add(new AdminOfConference() { UserId = admin.Id});
                //}
                return _conferenceRepository.Add(conference);
            }

            throw new NotEnoughRightsException();
        }

        public void Update(int userId, ConferenceDto conferenceDto)
        {
            if (CheckUserPermission(userId))
            {
                var conference = _mapper.Map<Conference>(conferenceDto);
                
                _conferenceRepository.Update(conference);
            }
            else
                throw new NotEnoughRightsException();
        }

        private bool CheckUserPermission(int userId)
        {
            return _userRepository.Get(userId)?.IsGlobalAdmin?? false;
        }
    }
}
