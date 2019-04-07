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
    [Route("api/lectures")]
    [ApiController]
    [ValidateModel]
    public class LecturesController : ControllerBase
    {
        private readonly ILectureService _lectureService;

        public LecturesController(ILectureService service)
        {
            _lectureService = service;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_lectureService.Get(id));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromBody]LectureDto conference)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(_lectureService.Add(Convert.ToInt32(userId), conference));
        }

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    return Ok(_lectureService.GetAll());
        //}
    }
}