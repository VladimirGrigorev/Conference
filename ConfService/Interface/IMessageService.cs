using ConfService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfService.Interface
{
    public interface IMessageService
    {
        IEnumerable<MessageDto> GetAllByLectureId(int idLecture);

    }
}
 