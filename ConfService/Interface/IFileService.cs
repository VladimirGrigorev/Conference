using System.Collections.Generic;
using System.IO;
using ConfService.Dto;
using Microsoft.AspNetCore.Http;

namespace ConfService.Interface
{
    public interface IFileService
    {
        IEnumerable<FileDto> GetAllByLectureId(int idLecture);
        int Upload(int userId, IFormFile file, int lectureId);
        (Stream fileStream, string contentType, string fileDownloadName) Download(int id);
        void Delete(int userId, int id);
    }
}