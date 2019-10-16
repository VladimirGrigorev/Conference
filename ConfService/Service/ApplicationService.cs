using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ConfModel.Model;
using ConfRepository.Interface;
using ConfService.Dto;
using ConfService.Interface;
using ConfService.ServiceException;

namespace ConfService.Service
{
    public class ApplicationService: IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicationNotificationRepository _applicationNotificationRepository;
        private readonly ISectionExpertRepository _sectionExpertRepository;
        private readonly IAdminOfConferenceRepository _adminOfConferenceRepository;
        private readonly IMapper _mapper;

        public ApplicationService(IApplicationRepository applicationRepository,
            IApplicationNotificationRepository applicationNotificationRepository,
            ISectionExpertRepository sectionExpertRepository, 
            IAdminOfConferenceRepository adminOfConferenceRepository,
            IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _applicationNotificationRepository = applicationNotificationRepository;
            _sectionExpertRepository = sectionExpertRepository;
            _adminOfConferenceRepository = adminOfConferenceRepository;
            _mapper = mapper;
        }

        public void RemoveNotifications(int userId, int id)
        {
            _applicationNotificationRepository.DeleteRelatedNotifications(userId, id);
        }

        public ApplicationDto Get(int userId, int id)
        {
            if (_applicationRepository.GetWithNotificationsAndSectionAndConference(userId, id) is Application app 
            && CheckUserPermission(userId, app, app.Section.Conference))
            {
                return _mapper.Map<ApplicationDto>(app);
            }

            throw new NotEnoughRightsException();
        }
        
        public IEnumerable<ApplicationDto> GetMy(int userId)
        {
            return _mapper.Map<IEnumerable <ApplicationDto>>(_applicationRepository
                .GetWithNotificationsAndSectionAndConferenceWhere(userId));
        }

        public IEnumerable<ApplicationDto> GetConsidered(int userId)
        {
            return _mapper.Map<IEnumerable<ApplicationDto>>(_applicationRepository.GetConsidered(userId)
                //, opt => opt.Items["userId"] = userId
                );
        }

        public int Add(int userId, ApplicationDto applicationDto)
        {
            applicationDto.UserId = userId;

            var application = _mapper.Map<Application>(applicationDto);
            
            foreach (var expertId in _sectionExpertRepository.GetExpertIds(applicationDto.SectionId))
            {
                application.ApplicationNotifications.Add(new ApplicationNotification()
                {
                    UserId = expertId
                });
            }

            return _applicationRepository.Add(application);
        }

        public void SetStatus(int userId, int id, ApplicationStatDto applicationStatDto)
        {
            if (_applicationRepository.GetWithNotificationsAndSectionAndConference(userId, id) is Application app
                && CheckUserPermission(userId, app, app.Section.Conference)
                && app.UserId != userId)
            {
                app.ApplicationStatus = applicationStatDto.ApplicationStatus;
                _applicationRepository.SaveChanges();
                return;
            }

            throw new NotEnoughRightsException();
        }

        //public void Update(int userId, ConferenceDto conferenceDto)
        //{
        //    if (CheckUserPermission(userId))
        //    {
        //        var conference = _mapper.Map<Conference>(conferenceDto);

        //        _conferenceRepository.Update(conference);
        //    }
        //    else
        //        throw new NotEnoughRightsException();
        //}
        
        /// <summary>
        /// Only creator, section's expert, conf's admin
        /// </summary>
        private bool CheckUserPermission(int userId, Application app, Conference conf)
        {
            if (app.UserId == userId)
                return true;

            if (_sectionExpertRepository.GetFirstOrDefault(e=>e.UserId == userId && e.SectionId == app.SectionId) != null)
                return true;

            if (conf.Sections.Any(s => s.Id == app.SectionId)
                && _adminOfConferenceRepository
                    .GetFirstOrDefault(a => a.UserId == userId && a.ConferenceId == conf.Id)!= null)
                return true;

            return false;
        }
    }
}