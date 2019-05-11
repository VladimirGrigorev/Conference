using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using File = ConfModel.Model.File;

namespace ConfRepository.Interface
{
    public interface IFileRepository : IRepository<File>
    {
        void Delete(File file);

        File GetWithApplication(int id);
    }
}
