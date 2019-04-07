using System.Collections.Generic;
using ConfService.Dto;

namespace ConfService.Interface
{
    public interface ILectureService
    {
        LectureDto Get(int id);
        int Add(int userId, LectureDto lectureDto);
    }
}