using System.Collections.Generic;
using ConfModel.Model;

namespace ConfRepository.Interface
{
    public interface IApplicationRepository : IRepository<Application>
    {
        Application GetWithSectionAndConference(int id);

        IEnumerable<Application> GetConsidered(int userId);

        void SaveChanges();
    }
}