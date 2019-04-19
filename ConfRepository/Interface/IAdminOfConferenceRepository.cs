using System;
using System.Collections.Generic;
using System.Text;
using ConfModel.Model;

namespace ConfRepository.Interface
{
    public interface IAdminOfConferenceRepository : IRepository<AdminOfConference>
    {
        bool IsAdminOfConf(int userId, int confId);
    }
}
