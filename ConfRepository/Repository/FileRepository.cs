using System;
using System.Collections.Generic;
using System.Text;
using ConfModel.Model;
using ConfRepository.Interface;

namespace ConfRepository.Repository
{
    class FileRepository : BaseRepository<File>, IFileRepository
    {
        public FileRepository(ConfContext confContext) : base(confContext)
        {
        }
    }
}
