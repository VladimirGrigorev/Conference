using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Conference.Filter;
using ConfService.Dto;
using ConfService.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Conference.Controllers
{
    [Route("api")]
    [ValidateModel]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService service)
        {
            _messageService = service;
        }

        [Authorize]
        [HttpGet("message/{id}")]
        public IActionResult GetAllByApplicationId(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(_messageService.GetAllByApplicationId(id, userId));
        }

        [Authorize]
        [HttpPost("message")]
        public IActionResult Add([FromBody]MessageDto message)
        {
            message.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(_messageService.Add(message));
        }

        [Authorize]
        [HttpDelete("application/{id}/messages/notifications")]
        public IActionResult DeleteNotifications(int id)
        {
            _messageService.RemoveMessages(id);
            return Ok();
        }
    }
}