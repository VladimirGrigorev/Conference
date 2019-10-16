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
        public MessageRepository(ConfContext confContext) : base(confContext){}

        public IEnumerable<Message> GetAll(int appId, int userId)
        {
            return Set.Where(m=> m.ApplicationId == appId).Include(m=>m.User)
                .Include(m=>m.MessageNotifications).Select(m=> new MessageIsNew()
                {
                    Message = m,
                    IsNew = m.MessageNotifications.Any(n=>n.UserId == userId)
                }).ToList().Select(m =>
                {
                    m.Message.IsNew = m.IsNew;
                    return m.Message;
                });
        }

        public IEnumerable<int> GetExpertIds()
        {
            return Set.Include(m=>m.Application).ThenInclude(a=>a.Section)
                .ThenInclude(s=>s.SectionExperts).SelectMany(m => m.Application.Section.SectionExperts.Select(e=>e.UserId));
        }

        private class MessageIsNew
        {
            public Message Message { get; set; }
            public bool IsNew { get; set; }
        }
    }
}
