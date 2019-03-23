using System;
using System.Collections.Generic;
using System.Text;
using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ConfContext confContext) : base(confContext)
        {
        }
    }
}
