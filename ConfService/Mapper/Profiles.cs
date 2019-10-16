using System.Dynamic;
using System.Linq;
using AutoMapper;
using ConfModel.Model;
using ConfService.Dto;

namespace ConfService.Mapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserInfoDto>().ReverseMap();

            CreateMap<ConferenceDto, Conference>()
                .ForMember(c => c.AdminOfConferences,
                    a => a.MapFrom(cd => cd.Admins
                        .Select(ad => new AdminOfConference()
                        {
                            UserId = ad.Id, ConferenceId = cd.Id
                        })));
            CreateMap<Conference, ConferenceDto>()
                .ForMember(cd => cd.Admins,
                    a => a.MapFrom(c => c.AdminOfConferences
                        .Select(ad => new UserInfoDto()
                        {
                            Id = ad.UserId, Email = ad.User.Email, Name = ad.User.Name
                        })));

            CreateMap<SectionDto, Section>()
                .ForMember(s => s.SectionExperts,
                    a => a.MapFrom(cd => cd.Experts
                        .Select(se => new SectionExpert()
                        {
                            UserId = se.Id,
                            SectionId = cd.Id
                        })));
            CreateMap<Section, SectionDto>()
                .ForMember(sd => sd.Experts,
                    a => a.MapFrom(c => c.SectionExperts
                        .Select(ad => new UserInfoDto()
                        {
                            Id = ad.UserId,
                            Email = ad.User.Email,
                            Name = ad.User.Name
                        })));
            //.ForMember(conferenceDto => conferenceDto.Sections,
            //a => a.MapFrom(conf => conf.Sections))
            //.ReverseMap();
            CreateMap<LectureDto, Lecture>()
                .ForMember(l => l.RoleInLectures,
                    a => a.MapFrom(ld => ld.Speakers/*.GroupBy(s=>s.Id).FirstOrDefault()*/
                        .Select(s => new RoleInLecture()
                        {
                            UserId = s.Id, Role = Role.Speaker
                        })));
            CreateMap<Lecture, LectureDto>()
                .ForMember(ld => ld.Speakers,
                    a => a.MapFrom(l => l.RoleInLectures.Where(r => r.Role == Role.Speaker)
                        .Select(r => new UserInfoDto()
                        {
                            Id = r.UserId, Email = r.User.Email, Name = r.User.Name
                        })));
                //.ForMember(lectureDto => lectureDto.SectionId,
                //    a => a.MapFrom(lect => lect..SectionId))
                //.ReverseMap()
                //;
            CreateMap<File, FileDto>()
                //.ForMember(fileDto => fileDto.LectureId,
                //    a => a.MapFrom(file => file.LectureId))
                .ReverseMap();
            CreateMap<Message, MessageDto>()
                .ForMember(messageDto => messageDto.UserName,
                    a=> a.MapFrom(message => message.User.Name)) //todo null check?
                .ReverseMap();

            CreateMap<Application, ApplicationDto>()
                .ForMember(applicationDto => applicationDto.SectionName,
                    a => a.MapFrom(application => application.Section.Name))
                .ForMember(applicationDto => applicationDto.ConferenceName,
                    a => a.MapFrom(application => application.Section.Conference.Name))
                //.ForMember(applicationDto=> applicationDto.IsNew, 
                //    opt=> opt.MapFrom((a, b,c, d )=>
                //    {
                //        var userId = (int)d.Items["userId"];
                //        //b.IsNew = a.ApplicationNotifications.Any(n => n.UserId == userId);
                //        return a.ApplicationNotifications.Any(n => n.UserId == userId);
                //    }))
                ;

            CreateMap<ApplicationDto, Application>();

            CreateMap<InfoPage, InfoPageDto>().ReverseMap();
        }
    }
}