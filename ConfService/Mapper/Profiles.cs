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
                .ForMember(conferenceDto=>conferenceDto.SectionsDto, 
                a=>a.MapFrom(conf=>conf.Sections))
                .ReverseMap();
            CreateMap<Section, SectionDto>()
                .ForMember(sectionDto => sectionDto.ConferenceDtoId,
                    a=>a.MapFrom(sect=> sect.ConferenceId))
                .ForMember(sectionDto => sectionDto.LecturesDto,
                    a => a.MapFrom(sect => sect.Lectures))
                .ReverseMap();
            CreateMap<Lecture, LectureDto>()
                .ForMember(lectureDto => lectureDto.SectionDtoId,
                    a => a.MapFrom(lect => lect.SectionId))
                .ReverseMap();
            CreateMap<File, FileDto>()
                .ForMember(fileDto => fileDto.LectureDtoId,
                    a => a.MapFrom(file => file.LectureId))
                .ReverseMap();
        }
    }
}