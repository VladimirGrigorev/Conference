using System.Collections.Generic;
using ConfService.Dto;

namespace ConfService.Interface
{
    public interface IConferenceService
    {
        ConferenceDto Get(int id);
        IEnumerable<ConferenceDto> GetAll();
        int Add(ConferenceDto conferenceDto);
    }
}