using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfModel.Model;
using ConfRepository.Interface;
using Microsoft.EntityFrameworkCore.Internal;

namespace ConfRepository.Repository
{
    public class AdminOfConferenceRepository : BaseRepository<AdminOfConference>, IAdminOfConferenceRepository
    {
        public AdminOfConferenceRepository(ConfContext confContext) : base(confContext)
        {
        }

        public bool IsAdminOfConf(int userId, int confId)
        {
            return Set.Any(a => a.UserId == userId && a.ConferenceId == confId);
        }
    }
}
