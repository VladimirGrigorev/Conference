using System;
using System.Collections.Generic;
using System.Text;
using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    public class LectureRepository : BaseRepository<Lecture>, ILectureRepository
    {
        public LectureRepository(ConfContext confContext) : base(confContext)
        {
        }
    }
}
