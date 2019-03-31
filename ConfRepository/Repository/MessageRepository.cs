using System;
using System.Collections.Generic;
using System.Text;
using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(ConfContext confContext) : base(confContext)
        {
        }
    }
}
