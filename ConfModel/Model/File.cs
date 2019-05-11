using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ConfModel.Interface;

namespace ConfModel.Model
{
    public class File : IId
    {
        public int Id { get; set; }

        [StringLength(300)]
        public string Name { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        [StringLength(200)]
        public string TempName { get; set; }

        public double Size { get; set; }
        
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
