using System;
using System.Collections.Generic;
using System.Text;
using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    public class AdminOfConferenceRepository : BaseRepository<AdminOfConference>, IAdminOfConferenceRepository
    {
        public AdminOfConferenceRepository(ConfContext confContext) : base(confContext)
        {
        }
    }
}
