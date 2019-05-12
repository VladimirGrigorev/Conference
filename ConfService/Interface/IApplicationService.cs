using System.Collections.Generic;
using ConfService.Dto;

namespace ConfService.Interface
{
    public interface IApplicationService
    {
        ApplicationDto Get(int userId, int id);
        IEnumerable<ApplicationDto> GetMy(int userId);
        IEnumerable<ApplicationDto> GetConsidered(int userId);
        int Add(int userId, ApplicationDto applicationDto);
        void SetStatus(int userId, int id, ApplicationStatDto applicationStatDto);
    }
}