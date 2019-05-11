using System.Collections.Generic;
using System.IO;
using ConfService.Dto;
using Microsoft.AspNetCore.Http;

namespace ConfService.Interface
{
    public interface IFileService
    {
        IEnumerable<FileDto> GetAllByApplicationId(int applicationId);
        int Upload(int userId, IFormFile file, int applicationId);
        (Stream fileStream, string contentType, string fileDownloadName) Download(int id);
        void Delete(int userId, int id);
    }
}