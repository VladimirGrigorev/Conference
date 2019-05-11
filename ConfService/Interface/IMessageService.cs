using ConfService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfService.Interface
{
    public interface IMessageService
    {
        int Add(MessageDto messageDto);
        IEnumerable<MessageDto> GetAllByApplicationId(int applicationId);

    }
}
 