using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ConfModel.Interface;

namespace ConfModel.Model
{
    public class FileBase : IId
    {
        public int Id { get; set; }

        [StringLength(300)]
        public string Name { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        [StringLength(200)]
        public string TempName { get; set; }

        public double Size { get; set; }
    }

    public class File : FileBase
    {
        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public ICollection<FileNotification> FileNotifications { get; set; } = new List<FileNotification>();

        [NotMapped]
        public bool IsNew { get; set; }
    }


    //public class TemplateFile: FileBase
    //{
    //    public int ConferenceId { get; set; }
    //    public Conference Conference { get; set; }
    //}
}
