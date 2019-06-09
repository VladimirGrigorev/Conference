using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ConfModel.Model;

namespace ConfRepository.Interface
{
    public interface IApplicationRepository : IRepository<Application>
    {
        Application GetWithNotificationsAndSectionAndConference(int id);

        IEnumerable<Application> GetWithNotificationsAndSectionAndConferenceWhere(Expression<Func<Application, bool>> predicate);

        IEnumerable<Application> GetConsidered(int userId);

        void SaveChanges();
    }
}