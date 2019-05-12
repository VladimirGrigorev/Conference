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
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(_applicationService.Get(userId, id));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add([FromBody]ApplicationDto appl)
        {
            var id = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(_applicationService.Add(id, appl));
        }

        [HttpGet("my")]
        [Authorize]
        public IActionResult GetMine()
        {
            var id = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(_applicationService.GetMy(id));
        }

        [HttpGet("considered")]
        [Authorize]
        public IActionResult GetConsidered()
        {
            var id = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(_applicationService.GetConsidered(id));
        }

        [HttpPatch("{id}")]
        [Authorize]
        public IActionResult SetStatus(int id, [FromBody]ApplicationStatDto applicationStatDto)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _applicationService.SetStatus(userId, id, applicationStatDto);
            return Ok();
        }

        //[HttpPut]
        //[Authorize]
        //public IActionResult Update([FromBody]ConferenceDto conference)
        //{
        //    var id = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    _conferenceService.Update(id, conference);
        //    return Ok();
        //}
    }
}