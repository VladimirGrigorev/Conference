using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConfService.Dto
{
    public class MessageDto
    {
        public int Id { get; set; }

        [StringLength(200)]
        [Required]
        public string Text { get; set; }
        
        public DateTime DateTimeSent { get; set; } = DateTime.Now;

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int LectureId { get; set; }
    }

}
