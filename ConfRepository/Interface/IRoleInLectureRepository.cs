using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ConfModel.Model;

namespace ConfRepository.Interface
{
    public interface IRoleInLectureRepository : IRepository<RoleInLecture>
    {
        //bool Any(Expression<Func<RoleInLecture, bool>> expression);
        void Delete(RoleInLecture roleInLecture);
    }
}
