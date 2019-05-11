using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using ConfModel.Model;
using ConfRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using File = ConfModel.Model.File;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ConfRepository.Repository
{
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        public FileRepository(ConfContext confContext) : base(confContext){}

        public void Delete(File file)
        {
            Set.Remove(file);
            _context.SaveChanges();
        }

        public File GetWithApplication(int id)
        {
            return Set.Include(f => f.Application).FirstOrDefault(f => f.Id == id);
        }
        
    }
}
