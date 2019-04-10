using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Conference.Filter;
using ConfService.Dto;
using ConfService.Interface;

namespace Conference.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService service)
        {
            _messageService = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetAllByLectureId(int id)
        {
            return Ok(_messageService.GetAllByLectureId(id));
        }
    }
}