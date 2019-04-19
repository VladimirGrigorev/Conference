using System.Collections.Generic;
using ConfService.Dto;

namespace ConfService.Interface
{
    public interface ILectureService
    {
        LectureDto Get(int id);
        int Add(int userId, LectureDto lectureDto);
        IEnumerable<LectureDto> GetUserSubscribedLectures(int userId);
        int AddListener(int userId, int lectureId);
        void DeleteListener(int userId, int lectureId);
    }
}