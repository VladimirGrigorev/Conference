using ConfService.Dto;

namespace ConfService.Interface
{
    public interface IInfoPageService
    {
        int Add(int userId, int conferenceId, InfoPageDto infoPageDto);
        InfoPageDto GetById(int id);
    }
}