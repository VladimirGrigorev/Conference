using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfModel.Model;
using ConfRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ConfRepository.Repository
{
    public class ConferenceRepository : BaseRepository<Conference>, IConferenceRepository
    {
        public ConferenceRepository(ConfContext confContext) : base(confContext)
        {}

        public override Conference Get(int id)
        {
            return Set.Include(c => c.Sections).ThenInclude(s=> s.Lectures).FirstOrDefault(c=>c.Id == id);
            //return Set.Find(id);
        }
    }
}
