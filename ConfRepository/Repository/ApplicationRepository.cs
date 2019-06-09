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
            return Set.Include(a => a.ApplicationNotifications)
                .Include(a => a.Section).ThenInclude(s => s.Conference)
                .FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Application> GetWithNotificationsAndSectionAndConferenceWhere(Expression<Func<Application, bool>> predicate)
        {
            return Set.Include(a=>a.ApplicationNotifications)
                .Include(a => a.Section).ThenInclude(s => s.Conference).Where(predicate);
        }
        
        //public IEnumerable<Application> GetConsidered(int userId)
        //{
        //     var apps = GetConsideredQuery(userId);
        //     return apps;
        //}

        public IEnumerable<Application> GetConsidered(int userId)
        {
            return GetConsideredQuery(userId)
                .Include(a => a.Messages).ThenInclude(m => m.MessageNotifications)
                .Include(a => a.Files).ThenInclude(f => f.FileNotifications)
                .Select(a => new
                {
                    app = a,
                    isNew = a.ApplicationNotifications.Any() 
                            || a.Messages.SelectMany(m => m.MessageNotifications).Any()
                            || a.Files.SelectMany(m => m.FileNotifications).Any()
                }).ToList()
                .Select(pair=>
                {
                    pair.app.IsNew = pair.isNew;
                    return pair.app;
                });
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private IQueryable<Application> GetConsideredQuery(int userId)
        {
            return Set.Include(a => a.ApplicationNotifications)
                .Include(a => a.Section).ThenInclude(s => s.SectionExperts)
                .Include(a => a.Section).ThenInclude(s => s.Conference)
                .ThenInclude(c => c.AdminOfConferences)
                .Where(a => (a.Section.SectionExperts.FirstOrDefault(se => se.UserId == userId) != null)
                            ||
                            a.Section.Conference.AdminOfConferences.FirstOrDefault(ad => ad.UserId == userId) != null);
            //todo firstordefault to any
            //.Where(a => a.Section.SectionExperts.Any(se => se.UserId == userId))
        }
        

    }
}