using AutoMapper;
using ConfModel.Model;
using ConfRepository.Interface;
using ConfService.Dto;
using ConfService.Interface;
using ConfService.ServiceException;

namespace ConfService.Service
{
    public class InfoPageService : IInfoPageService
    {
        private readonly IInfoPageRepository _infoPageRepository;
        private readonly IAdminOfConferenceRepository _adminOfConferenceRepository;
        private readonly IMapper _mapper;

        public InfoPageService(IInfoPageRepository infoPageRepository,
            IAdminOfConferenceRepository adminOfConferenceRepository, 
            IMapper mapper)
        {
            _infoPageRepository = infoPageRepository;
            _adminOfConferenceRepository = adminOfConferenceRepository;
            _mapper = mapper;
        }


        public int Add(int userId, int conferenceId, InfoPageDto infoPageDto)
        {
            if (CheckUserUpdatePermission(userId, conferenceId))
            {
                var infoPage = _mapper.Map<InfoPage>(infoPageDto);
                infoPage.ConferenceId = conferenceId;

                return _infoPageRepository.Add(infoPage);
            }

            throw new NotEnoughRightsException();
        }

        public InfoPageDto GetById(int id)
        {
            return _mapper.Map<InfoPageDto>(_infoPageRepository.Get(id));
        }

        private bool CheckUserUpdatePermission(int userId, int conferenceId)
        {
            //todo rewrite Any
            return _adminOfConferenceRepository.GetFirstOrDefault(a => a.UserId == userId && a.ConferenceId == conferenceId) !=
                   null;
        }
    }
}