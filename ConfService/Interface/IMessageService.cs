using ConfService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfService.Interface
{
    public interface IMessageService
    {
        int Add(MessageDto messageDto);
        IEnumerable<MessageDto> GetAllByApplicationId(int applicationId, int userId);

        IEnumerable<MessageDto> GetAllByLectureId(int lectureId, int userId);
        void RemoveMessages(int appId);
        //object GetAllByLectureId(int id, int userId);
    }
}
 