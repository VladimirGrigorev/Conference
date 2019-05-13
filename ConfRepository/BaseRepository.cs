using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ConfModel.Model;
using Microsoft.EntityFrameworkCore;
using ConfModel.Interface;

namespace ConfRepository
{
    public class BaseRepository<T> : IRepository<T> where T: class, IId, new()
    {
        protected readonly ConfContext _context;

        private DbSet<T> _set;
        
        protected DbSet<T> Set => _set ?? (_set = _context.Set<T>());
        
        public BaseRepository(ConfContext confContext)
        {
            _context = confContext;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Set;
        }

        public virtual T Get(int id)
        {
            return Set.Find(id);
        }

        public virtual IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public int Add(T entity)
        {
            var entityEntry = Set.Add(entity);
            _context.SaveChanges();
            return entityEntry.Entity.Id;
        }

        public virtual void Update(T entity)
        {
            Set.Update(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            Set.Remove(new T(){Id = id});
            _context.SaveChanges();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> ex)
        {
            return Set.FirstOrDefault(ex);
        }
    }
}
