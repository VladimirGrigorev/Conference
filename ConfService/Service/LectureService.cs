using AutoMapper;
using ConfModel.Model;
using ConfRepository.Interface;
using ConfService.Dto;
using ConfService.Interface;
using ConfService.ServiceException;

namespace ConfService.Service
{
    public class LectureService: ILectureService
    {
        private readonly IMapper _mapper;
        private readonly ILectureRepository _lectureRepository;
        private readonly IAdminOfConferenceRepository _adminOfConferenceRepository;
        private readonly ISectionRepository _sectionRepository;

        public LectureService(ILectureRepository lectureRepository,
            IAdminOfConferenceRepository adminOfConferenceRepository, 
            ISectionRepository sectionRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _lectureRepository = lectureRepository;
            _adminOfConferenceRepository = adminOfConferenceRepository;
            _sectionRepository = sectionRepository;
        }

        public LectureDto Get(int id)
        {
            return _mapper.Map<LectureDto>(_lectureRepository.Get(id));
        }

        public int Add(int userId, LectureDto lectureDto)
        {
            if (CheckUserPermission(userId, lectureDto.SectionId))
            {
                return _lectureRepository.Add(_mapper.Map<Lecture>(lectureDto));
            }

            throw new NotEnoughRightsException();
        }

        protected bool CheckUserPermission(int userId, int sectionId)
        {
            //todo rewrite
            //lecture can be created only by it's conference admin
            //find lecture's conf id
            //check if current user is conf admin
            if (_sectionRepository.Get(sectionId) is Section sect
                && _adminOfConferenceRepository.IsAdminOfConf(userId, sect.ConferenceId))
                return true;
            return false;
        }
    }
}