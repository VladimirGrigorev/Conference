using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ConfModel.Model;
using ConfRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ConfRepository.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ConfContext confContext) : base(confContext)
        {
        }
        
        public User GetFirstOrDefaultWithRoles(Expression<Func<User, bool>> ex)
        {
            //this._context.RoleInLectures.Add(new RoleInLecture() {LectureId = 1, UserId = 8, Role = Role.Speaker});
            //this._context.SaveChanges();
            return Set.Include(a => a.RoleInLectures).Include(a=>a.AdminOfConferences).FirstOrDefault(ex);
        }

        //public User GetIserWithConfsSecsApps(int id)
        //{
        //    return Set.Include(u => u.AdminOfConferences)
        //        .ThenInclude(ad => ad.Conference)
        //        .ThenInclude(c => c.Sections)
        //        .ThenInclude(s => s.Applications)
        //        .Include(u => u.SectionExperts)
        //        .ThenInclude(se => se.Section)
        //        .ThenInclude(s => s.Applications)
        //        .FirstOrDefault(u => u.Id == id);
        //}
    }
}
