using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ConfModel.Model;
using ConfRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ConfRepository.Repository
{
    public class ApplicationRepository: BaseRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(ConfContext confContext) : base(confContext){}
        
        public Application GetWithNotificationsAndSectionAndConference(int id)
        {
            var queryable = Set.Include(a => a.Section).ThenInclude(s => s.Conference)
                .Include(a => a.ApplicationNotifications)
                .Select(a => new AppIsNew()
                {
                    App = a,
                    IsNew = a.ApplicationNotifications.Any()
                });
            return FromAppIsNewToApplication(queryable.FirstOrDefault(a => a.App.Id == id));
        }

        public IEnumerable<Application> GetWithNotificationsAndSectionAndConferenceWhere(int userId)
        {
            return GetApps(userId, true);
        }

        //public IEnumerable<Application> GetConsidered(int userId)
        //{
        //     var apps = GetConsideredQuery(userId);
        //     return apps;
        //}

        public IEnumerable<Application> GetConsidered(int userId)
        {
            return GetApps(userId, false);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void RemoveFileNotifications(int appId)
        {
            _context.RemoveRange(IncludeMessNotifFileNotif(Set).Where(a => a.Id == appId)
                .SelectMany(a => a.Files.SelectMany(f => f.FileNotifications)));
            _context.SaveChanges();
        }

        public void RemoveMessageNotifications(int appId)
        {
            _context.RemoveRange(IncludeMessNotifFileNotif(Set).Where(a => a.Id == appId)
                .SelectMany(a => a.Messages.SelectMany(f => f.MessageNotifications)));
            _context.SaveChanges();
        }

        private IEnumerable<Application> GetApps(int userId, bool isMy)
        {
            var q = isMy ? GetMyConfQuery(userId) : GetConsideredQuery(userId);
            return SelectAppIsNew(q).ToList()
                .Select(FromAppIsNewToApplication);
        }

        private static Application FromAppIsNewToApplication(AppIsNew appIsNew)
        {
            appIsNew.App.IsNew = appIsNew.IsNew;
            return appIsNew.App;
        }

        private IQueryable<AppIsNew> SelectAppIsNew(IQueryable<Application> q)
        {
            return IncludeMessNotifFileNotif(q)
                .Include(a => a.ApplicationNotifications)
                .Select(a => new AppIsNew()
                {
                    App = a,
                    IsNew = a.ApplicationNotifications.Any()
                            || a.Messages.SelectMany(m => m.MessageNotifications).Any()
                            || a.Files.SelectMany(m => m.FileNotifications).Any()
                });
        }

        private IQueryable<Application> IncludeMessNotifFileNotif(IQueryable<Application> q)
        {
            return q
                .Include(a => a.Messages).ThenInclude(m => m.MessageNotifications)
                .Include(a => a.Files).ThenInclude(f => f.FileNotifications);
        }

        private IQueryable<Application> GetConsideredQuery(int userId)
        {
            return Set
                .Include(a => a.Section).ThenInclude(s => s.SectionExperts)
                .Include(a => a.Section).ThenInclude(s => s.Conference)
                .ThenInclude(c => c.AdminOfConferences)
                .Where(a => (a.Section.SectionExperts.FirstOrDefault(se => se.UserId == userId) != null)
                            ||
                            a.Section.Conference.AdminOfConferences.FirstOrDefault(ad => ad.UserId == userId) != null);
            //todo firstordefault to any
            //.Where(a => a.Section.SectionExperts.Any(se => se.UserId == userId))
        }

        private IQueryable<Application> GetMyConfQuery(int userId)
        {
            return Set
                .Include(a => a.Section).ThenInclude(s => s.Conference).Where(s=> s.UserId == userId);
        }

        private class AppIsNew
        {
            public Application App { get; set; }
            public bool IsNew { get; set; }
        }
        
    }
}