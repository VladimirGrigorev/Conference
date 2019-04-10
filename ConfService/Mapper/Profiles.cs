using System.Dynamic;
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
            CreateMap<Conference, ConferenceDto>()
                //.ForMember(conferenceDto => conferenceDto.Sections,
                //a => a.MapFrom(conf => conf.Sections))
                .ReverseMap();
            CreateMap<Section, SectionDto>()
                //.ForMember(sectionDto => sectionDto.ConferenceId,
                //    a=>a.MapFrom(sect=> sect.ConferenceId))
                //.ForMember(sectionDto => sectionDto.Lectures,
                //    a => a.MapFrom(sect => sect.Lectures))
                .ReverseMap();
            CreateMap<Lecture, LectureDto>()
                //.ForMember(lectureDto => lectureDto.SectionId,
                //    a => a.MapFrom(lect => lect.SectionId))
                .ReverseMap();
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