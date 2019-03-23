using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ConfRepository
{
    public interface IRepository<T>  where T:class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate);
        int Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
