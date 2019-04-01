using System;
using System.Collections.Generic;
using System.Text;

namespace ConfService.Dto
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
