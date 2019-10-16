using System.ComponentModel.DataAnnotations;
using ConfModel.Interface;

namespace ConfModel.Model
{
    public class InfoPage : IId
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string Title { get; set; }

        //?what's the difference comparing to text?
        [DataType(DataType.MultilineText)]
        public string Data { get; set; }

        public int ConferenceId { get; set; }
        public Conference Conference { get; set; }

    }
}