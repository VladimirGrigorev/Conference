using System;
using System.Collections.Generic;
using System.Text;
using ConfModel.Model;

namespace ConfRepository.Interface
{
    public interface IMessageRepository : IRepository<Message>
    {
        IEnumerable<int> GetExpertIds();
        IEnumerable<Message> GetAll(int appId, int userId);
    }
}
