using System;
using System.Collections.Generic;
using System.Text;
using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    public class ConferenceRepository : BaseRepository<Conference>, IConferenceRepository
    {
        public ConferenceRepository(ConfContext confContext) : base(confContext)
        {}
    }
}
