using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    public class RoleInLectureRepository : BaseRepository<RoleInLecture>, IRoleInLectureRepository
    {
        public RoleInLectureRepository(ConfContext confContext) : base(confContext){}

        public bool Any(Expression<Func<RoleInLecture, bool>> expression)
        {
            return Set.Any(expression);
        }

        public void Delete(RoleInLecture roleInLecture)
        {
            Set.Remove(roleInLecture);
            _context.SaveChanges();
        }
    }
}
