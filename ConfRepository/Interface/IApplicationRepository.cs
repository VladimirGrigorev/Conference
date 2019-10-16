using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ConfModel.Model;

namespace ConfRepository.Interface
{
    public interface IApplicationRepository : IRepository<Application>
    {
        Application GetWithNotificationsAndSectionAndConference(int userId, int id);

        IEnumerable<Application> GetWithNotificationsAndSectionAndConferenceWhere(int userId);

        IEnumerable<Application> GetConsidered(int userId);

        void RemoveMessageNotifications(int appId);

        void RemoveFileNotifications(int appId);

        void SaveChanges();
    }
}