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
    [Route("api/conferences")]
    [ApiController]
    [ValidateModel]
    public class ConferenceController : ControllerBase
    {
        private readonly IConferenceService _conferenceService;

        public ConferenceController(IConferenceService service)
        {
            _conferenceService = service;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_conferenceService.Get(id));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add([FromBody]ConferenceDto conference)
        {
            var id = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(_conferenceService.Add(id, conference));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_conferenceService.GetAll());
        }
        
    }
}