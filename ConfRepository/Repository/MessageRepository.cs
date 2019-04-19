using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ConfModel.Model;
using ConfRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ConfRepository.Repository
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(ConfContext confContext) : base(confContext)
        {
            
        }

        public override IEnumerable<Message> GetWhere(Expression<Func<Message, bool>> predicate)
        {
            return Set.Where(predicate).Include(m=>m.User);
        }
    }
}
