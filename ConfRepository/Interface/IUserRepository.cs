using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ConfModel.Model;

namespace ConfRepository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        User GetFirstOrDefault(Expression<Func<User, bool>> ex);
    }
}
