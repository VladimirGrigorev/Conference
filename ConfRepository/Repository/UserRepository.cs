using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ConfContext confContext) : base(confContext)
        {
        }
        
        public User GetFirstOrDefault(Expression<Func<User, bool>> ex)
        {
           return  Set.FirstOrDefault(ex);
        }
    }
}
