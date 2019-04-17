using System.Collections.Generic;
using System.IO;
using AutoMapper;
using ConfModel.Model;
using ConfRepository.Interface;
using ConfService.Dto;
using ConfService.Interface;
using ConfService.ServiceException;
using Microsoft.AspNetCore.Http;

namespace ConfService.Service
{
    public class FileService: IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IRoleInLectureRepository _roleInLectureRepository;
        private readonly IMapper _mapper;

        public FileService(IFileRepository fileRepository, IRoleInLectureRepository roleInLectureRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _roleInLectureRepository = roleInLectureRepository;
            _mapper = mapper;
        }

        public IEnumerable<FileDto> GetAllByLectureId(int idLecture)
        {
            return _mapper.Map<IEnumerable<FileDto>>(_fileRepository.GetWhere(f => f.LectureId == idLecture));
        }

        public int Upload(int userId, IFormFile file, int lectureId)
        {
            if (CheckUserPermission(userId, lectureId))
            {
                return _fileRepository.Upload(file, lectureId);
            }

            throw new NotEnoughRightsException();
        }

        private bool CheckUserPermission(int userId, int lectureId)
        {
            return _roleInLectureRepository.Any(r =>
                r.UserId == userId && r.LectureId == lectureId && r.Role == Role.Speaker);
        }

        public (Stream fileStream, string contentType, string fileDownloadName) Download(int id)
        {
            return _fileRepository.Download(id);
        }


    }
}