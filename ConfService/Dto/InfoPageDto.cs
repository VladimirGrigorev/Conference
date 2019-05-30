using System.ComponentModel.DataAnnotations;

namespace ConfService.Dto
{
    public class InfoPageDto
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        
        public string Data { get; set; }

        public int ConferenceId { get; set; }
    }
}