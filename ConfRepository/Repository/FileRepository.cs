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

        public IEnumerable<int> GetExpertIds()
        {
            return Set.Include(m => m.Application).ThenInclude(a => a.Section)
                .ThenInclude(s => s.SectionExperts).SelectMany(m => m.Application.Section.SectionExperts.Select(e => e.UserId));
        }

        public IEnumerable<File> GetAll(int appId, int userId)
        {
            return Set.Where(f => f.ApplicationId == appId)
                .Include(f => f.FileNotifications).Select(m => new FileIsNew()
                {
                    File = m,
                    IsNew = m.FileNotifications.Any(n => n.UserId == userId)
                }).ToList().Select(f =>
                {
                    f.File.IsNew = f.IsNew;
                    return f.File;
                });
        }

        private class FileIsNew
        {
            public File File { get; set; }
            public bool IsNew { get; set; }
        }
    }
}
