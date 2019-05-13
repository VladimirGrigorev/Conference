using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfModel.Model;
using ConfRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ConfRepository.Repository
{
    public class ConferenceRepository : BaseRepository<Conference>, IConferenceRepository
    {
        public ConferenceRepository(ConfContext confContext) : base(confContext)
        {}

        public override IEnumerable<Conference> GetAll()
        {
            return Set.Include(c=>c.Sections);
        }

        public override Conference Get(int id)
        {
            return Set.Include(c => c.AdminOfConferences).ThenInclude(a=>a.User)
                    .Include(c => c.Sections).ThenInclude(s => s.SectionExperts)
                    .ThenInclude(ex=>ex.User)
                    .Include(c => c.Sections).ThenInclude(s => s.Lectures)
                    .ThenInclude(l => l.RoleInLectures)
                    .ThenInclude(r=>r.User)
                    .FirstOrDefault(c => c.Id == id)
                ;
            //return Set.Find(id);
        }

        public override void Update(Conference entity)
        {
            //detach all entities so nothing is tracked by context
            //(to make sure we won't delete smth important)
            _context.ChangeTracker.Entries()
                //.Where(e => e.State != EntityState.Unchanged)
                .ToList().ForEach(e=>e.State = EntityState.Detached);
            
            //add or update conf and children
            Set.Update(entity);
            
            //load old conference with all needed children
            var confDb = Set.Include(c => c.AdminOfConferences)
                .Include(c => c.Sections).ThenInclude(s => s.SectionExperts)
                .Include(c => c.Sections).ThenInclude(s => s.Lectures)
                .ThenInclude(l => l.RoleInLectures)
                .FirstOrDefault(c => c.Id == entity.Id)
                ;

            //get all unchanged entities (they are deleted ones)
            var entries = _context.ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Unchanged)
                .Select(t=>t.Entity)
                //.Where(o=> !(o is User) && !(o is AdminOfConference))
                ;

            //delete deleted entities
            _context.RemoveRange(entries);
            
            _context.SaveChanges();
        }
    }
}
