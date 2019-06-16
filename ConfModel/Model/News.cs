using System;
using System.ComponentModel.DataAnnotations;
using ConfModel.Interface;

namespace ConfModel.Model
{
    public class News : IId
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string Data { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; } = DateTime.Now;

        public int ConferenceId { get; set; }
        public Conference Conference { get; set; }

    }
}