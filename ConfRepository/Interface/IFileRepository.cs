using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using File = ConfModel.Model.File;

namespace ConfRepository.Interface
{
    public interface IFileRepository : IRepository<File>
    {
        int Upload(IFormFile file, int lectureId);
        (Stream fileStream, string contentType, string fileDownloadName) Download(int id);
        void Delete(File file);
    }
}
