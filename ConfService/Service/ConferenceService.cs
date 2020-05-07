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
        private readonly IAdminOfConferenceRepository _adminOfConferenceRepository;
        protected readonly IMapper _mapper;

        public ConferenceService(IConferenceRepository repositoy,
            IUserRepository userRepository,
            IAdminOfConferenceRepository adminOfConferenceRepository,
            IMapper mapper)
        {
            _conferenceRepository = repositoy;
            _userRepository = userRepository;
            _adminOfConferenceRepository = adminOfConferenceRepository;
            _mapper = mapper;
        }
        public ConferenceDto Get(int id)
        {
            var conference = _conferenceRepository.Get(id);
            return _mapper.Map<ConferenceDto>(conference);
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
            if (CheckUserUpdatePermission(userId, conferenceDto.Id))
            {
                var conference = _mapper.Map<Conference>(conferenceDto);
                
                _conferenceRepository.Update(conference);
            }
            else
                throw new NotEnoughRightsException();
        }

        public void DeleteById(int id, int userId)
        {
            if (CheckUserPermission(userId))
            {
                _conferenceRepository.Delete(id);
            }
            else
                throw new NotEnoughRightsException();
        }   

        private bool CheckUserPermission(int userId)
        {
            return _userRepository.Get(userId)?.IsGlobalAdmin?? false;
        }

        private bool CheckUserUpdatePermission(int userId, int id)
        {
            return (_adminOfConferenceRepository.GetFirstOrDefault(a => a.UserId == userId && a.ConferenceId == id) !=
                   null) || (_userRepository.Get(userId)?.IsGlobalAdmin ?? false);
        }
    }
}
