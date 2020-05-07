using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Xml;
using AutoMapper;
using ConfModel.Model;
using ConfRepository.Interface;
using ConfService.Dto;
using ConfService.Interface;
using ConfService.ServiceException;
using Microsoft.AspNetCore.Http;
using File = System.IO.File;

namespace ConfService.Service
{
    public class FileService: IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationRepository _applicationRepository;

        protected const string FolderName = "Upload";

        public FileService(IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
            _applicationRepository = applicationRepository;
        }

        public IEnumerable<FileDto> GetAllByApplicationId(int userId, int applicationId)
        {
            return _mapper.Map<IEnumerable<FileDto>>(_fileRepository.GetAll(applicationId, userId));
        }

        public void DeleteNotifications(int appId)
        {
            _applicationRepository.RemoveFileNotifications(appId);
        }

        #region upload&check
        public int Upload(int userId, IFormFile file, int applicationId)
        {
            if (CheckUserPermission(userId, applicationId))
            {
                return Upload(file, applicationId);
            }
            else
              throw new NotEnoughRightsException();
        }

        public int Upload(IFormFile file, int applicationId)
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

                if (TryCheckFile(fullPath, out var checkStatus))
                {
                    if (checkStatus.IsOk)
                    {
                        var entity = new ConfModel.Model.File() { Name = file.FileName, ApplicationId = applicationId, Size = file.Length };

                        foreach (var expertId in _fileRepository.GetExpertIds())
                        {
                            entity.FileNotifications.Add(new FileNotification()
                            {
                                UserId = expertId
                            });
                        }

                        return _fileRepository.Add(entity);
                    }

                    DeleteFile(fullPath);
                    throw new ObjectException(checkStatus);
                }
                else
                {
                    DeleteFile(fullPath);
                    throw new Exception("Exception while checking.");
                }
            }

            throw new Exception("Bad file");
        }

        private bool TryCheckFile(string fullPath, out CheckedStatus checkedStatus)
        {
            checkedStatus = null;

            string batchFileLocation = GetCheckPath();

            Process p = new Process();
            p.StartInfo.FileName = batchFileLocation;
            p.StartInfo.WorkingDirectory = Path.GetDirectoryName(batchFileLocation);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.Arguments = fullPath;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            var output = p.StandardOutput.ReadLine();
            p.WaitForExit();

            try
            {
                checkedStatus = ParseXml(output);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                DeleteFile(output);
            }
            return true;
        }

        protected void DeleteFile(string filename)
        {
            if (File.Exists(filename))
                File.Delete(filename);
        }

        public class CheckedStatus
        {
            public string Result { get; set; }
            public bool IsOk => Result == "Ok";
            public ICollection<Item> Warnings { get; set; } = new List<Item>();
            public ICollection<Item> Errors { get; set; } = new List<Item>();
        }

        public class Item
        {
            public string Paragraph { get; set; }
            public string Comment { get; set; }
        }

        private CheckedStatus ParseXml(string filename)
        {
            var checkStatus = new CheckedStatus();

           
           XmlDocument xDoc = new XmlDocument();
           //var encodeName = XmlConvert.EncodeName(File.ReadAllText(filename));
           xDoc.Load(filename);//(filename);

            XmlElement xRoot = xDoc.DocumentElement;
            var result = xRoot.GetAttribute("Result"); //.Attributes.GetNamedItem();

            checkStatus.Result = result;
            if (result == "Ok")
                return checkStatus;
            
            foreach (XmlNode xnode in xRoot)
            {
                var item = new Item();

                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "Paragraph")
                    {
                        item.Paragraph = childnode.InnerText;
                    }
                    else if (childnode.Name == "Comment")
                    {
                        item.Comment = childnode.InnerText;
                    }
                }

                if (xnode.Name == "Error")
                {
                    checkStatus.Errors.Add(item);
                }
                else if (xnode.Name == "Warning")
                {
                    checkStatus.Warnings.Add(item);
                }
            }
            return checkStatus;
        }

        #endregion
       
        public void Delete(int userId, int id)
        {
            if (_fileRepository.GetWithApplication(id) is ConfModel.Model.File file 
                && CheckUserPermission(userId, file, file.Application))
            {
                _fileRepository.Delete(file);

                var path = Path.Combine(GetSavePath(), file.Name);
                System.IO.File.Delete(path);
                return;
            }
            throw new NotEnoughRightsException();
        }
        
        public (Stream fileStream, string contentType, string fileDownloadName) Download(int id)
        {
            if (_fileRepository.Get(id) is ConfModel.Model.File file)
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
        protected string GetCheckPath() => @"B:\CSharp\conf\conference\conf-util\check.bat";

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

        protected bool IsNullOrEmptyFileName(string contentDisposition, out string fileName)
        {
            fileName = ContentDispositionHeaderValue.Parse(contentDisposition).FileName.Trim('"');
            return string.IsNullOrEmpty(fileName);
        }

        private bool CheckUserPermission(int userId, ConfModel.Model.File file, Application app)
        {
            return file.ApplicationId == app.Id && app.UserId == userId;
        }

        private bool CheckUserPermission(int userId, int applicationId)
        {
            return _applicationRepository.Get(applicationId)?.UserId == userId;
        }
    }
}