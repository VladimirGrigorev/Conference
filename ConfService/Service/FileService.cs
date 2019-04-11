using System.Collections.Generic;
using System.IO;
using AutoMapper;
using ConfRepository.Interface;
using ConfService.Dto;
using ConfService.Interface;
using Microsoft.AspNetCore.Http;

namespace ConfService.Service
{
    public class FileService: IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public FileService(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public IEnumerable<FileDto> GetAllByLectureId(int idLecture)
        {
            return _mapper.Map<IEnumerable<FileDto>>(_fileRepository.GetWhere(f => f.LectureId == idLecture));
        }

        public int Upload(IFormFile file, int lectureId)
        {
            return _fileRepository.Upload(file, lectureId);
        }

        public (Stream fileStream, string contentType, string fileDownloadName) Download(int id)
        {
            return _fileRepository.Download(id);
        }
    }
}