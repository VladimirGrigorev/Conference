using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    public class InfoPageRepository: BaseRepository<InfoPage>, IInfoPageRepository
    {
        public InfoPageRepository(ConfContext confContext) : base(confContext){}

        public IEnumerable<InfoPage> GetWithoutDataWhere(Expression<Func<InfoPage, bool>> predicate)
        {
            return Set.Where(predicate)
                .Select(p=> new InfoPage(){Id = p.Id, ConferenceId = p.ConferenceId, Title = p.Title});
        }
    }
}