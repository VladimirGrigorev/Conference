using System.ComponentModel.DataAnnotations;
using ConfModel.Interface;

namespace ConfModel.Model
{
    public class Announcement: IId
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Data { get; set; }

        public int ConferenceId { get; set; }
        public Conference Conference { get; set; }
    }
}