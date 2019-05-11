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
                //.ForMember(conferenceDto => conferenceDto.Sections,
                //a => a.MapFrom(conf => conf.Sections))
                //.ReverseMap();
            CreateMap<Section, SectionDto>()
                //.ForMember(sectionDto => sectionDto.ConferenceId,
                //    a=>a.MapFrom(sect=> sect.ConferenceId))
                //.ForMember(sectionDto => sectionDto.Lectures,
                //    a => a.MapFrom(sect => sect.Lectures))
                .ReverseMap();
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
        }
    }
}