using AutoMapper;
using ConfModel.Model;
using ConfService.Dto;

namespace ConfService.Mapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Conference, ConferenceDto>().ReverseMap();
            CreateMap<Section, SectionDto>().ReverseMap();
            CreateMap<Lecture, LectureDto>().ReverseMap();
            CreateMap<File, FileDto>().ReverseMap();
        }
    }
}