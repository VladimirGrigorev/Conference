using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ConfModel.Model;

namespace ConfRepository.Interface
{
    public interface IApplicationRepository : IRepository<Application>
    {
        Application GetWithSectionAndConference(int id);

        IEnumerable<Application> GetWithSectionAndConferenceWhere(Expression<Func<Application, bool>> predicate);

        IEnumerable<Application> GetConsidered(int userId);

        void SaveChanges();
    }
}