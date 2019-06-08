using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ConfModel.Model;

namespace ConfRepository.Interface
{
    public interface IInfoPageRepository : IRepository<InfoPage>
    {
        IEnumerable<InfoPage> GetWithoutDataWhere(Expression<Func<InfoPage, bool>> predicate);
    }
}