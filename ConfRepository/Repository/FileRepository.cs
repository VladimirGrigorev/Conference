using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using ConfModel.Model;
using ConfRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using File = ConfModel.Model.File;

namespace ConfRepository.Repository
{
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        protected const string FolderName = "Upload";

        public FileRepository(ConfContext confContext) : base(confContext)
        {
        }

        public int Upload(IFormFile file, int lectureId)
        {
            string savePath = GetSavePath();

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            //string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            
            if (file?.Length > 0 
                && !string.IsNullOrEmpty(file.FileName))
            {
                //todo create guid and save with guid name
                string fullPath = Path.Combine(savePath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                
                return this.Add(new File() {Name = file.FileName, LectureId = lectureId, Size = file.Length});
            }

            throw new Exception("Bad file");
        }

        protected bool IsNullOrEmptyFileName(string contentDisposition, out string fileName)
        {
            fileName = ContentDispositionHeaderValue.Parse(contentDisposition).FileName.Trim('"');
            return string.IsNullOrEmpty(fileName);
        }

        public void Delete(File file)
        {
            Set.Remove(file);
            _context.SaveChanges();
            var path = Path.Combine(GetSavePath(), file.Name);
            System.IO.File.Delete(path);
        }

        public (Stream fileStream, string contentType, string fileDownloadName) Download(int id)
        {
            if (this.Get(id) is File file)
            {
                var path = Path.Combine(GetSavePath(), file.Name);
                
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    stream.CopyTo(memory);
                }
                memory.Position = 0;
                var mimeType = GetContentType(path);

                return (memory, mimeType, file.Name);
            }

            throw new Exception("File not found");
        }

        protected string GetSavePath() => Path.Combine(Directory.GetCurrentDirectory(), FolderName);

        //todo rewrite
        private static string GetContentType(string file)
        {
            string extension = Path.GetExtension(file).ToLowerInvariant();
            switch (extension)
            {
                case ".txt": return "text/plain";
                case ".pdf": return "application/pdf";
                case ".doc": return "application/vnd.ms-word";
                case ".docx": return "application/vnd.ms-word";
                case ".xls": return "application/vnd.ms-excel";
                case ".xlsx": return "application/vnd.ms-excel";
                case ".png": return "image/png";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".gif": return "image/gif";
                case ".csv": return "text/csv";
                default: return "";
            }
        }
    }
}
