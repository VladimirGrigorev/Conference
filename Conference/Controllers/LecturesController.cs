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
    [Route("api")]
    [ApiController]
    [ValidateModel]
    public class LecturesController : ControllerBase
    {
        private readonly ILectureService _lectureService;

        public LecturesController(ILectureService service)
        {
            _lectureService = service;
        }

        [HttpGet("lectures/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_lectureService.Get(id));
        }

        [Authorize]
        [HttpPost("lectures")]
        public IActionResult Add([FromBody]LectureDto conference)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(_lectureService.Add(Convert.ToInt32(userId), conference));
        }
        
        [Authorize]
        [HttpGet("my/lectures")]
        public IActionResult GetUserSubscribedLectures()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(_lectureService.GetUserSubscribedLectures(Convert.ToInt32(userId)));
        }

        [Authorize]
        [HttpPost("my/lectures")]
        public IActionResult AddListenerToLectures([FromBody]int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(_lectureService.AddListener(Convert.ToInt32(userId), id));
        }

        [Authorize]
        [HttpDelete("my/lectures/{id}")]
        public IActionResult DeleteListenerFromLectures(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _lectureService.DeleteListener(Convert.ToInt32(userId), id);
            return Ok();
        }

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    return Ok(_lectureService.GetAll());
        //}
    }
}