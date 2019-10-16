using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Conference.Filter;
using ConfService.Dto;
using ConfService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conference.Controllers
{
    [Route("api/")]
    [ValidateModel]
    [ApiController]
    public class InfoPageController : ControllerBase
    {
        private readonly IInfoPageService _infoPageService;

        public InfoPageController(IInfoPageService infoPageService)
        {
            _infoPageService = infoPageService;
        }

        [HttpGet("pages/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_infoPageService.GetById(id));
        }

        [HttpGet("conferences/{id}/pages")]
        public IActionResult GetAllByConferenceId(int id)
        {
            return Ok(_infoPageService.GetAllByConferenceId(id));
        }

        [HttpPost("conferences/{id}/pages")]
        [Authorize]
        public IActionResult Add(int id, [FromBody]InfoPageDto infoPage)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(_infoPageService.Add(userId, id, infoPage));
        }
    }
}