using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conference.Filter;
using ConfService.Dto;
using ConfService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conference.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    public class ConferenceController : ControllerBase
    {
        private readonly IConferenceService _iConferenceService;

        public ConferenceController(IConferenceService service)
        {
            _iConferenceService = service;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_iConferenceService.Get(id));
        }

        [HttpPost]
        public IActionResult Add([FromBody]ConferenceDto conference)
        {
            throw new Exception("Dooooom");
            return Ok(_iConferenceService.Add(conference));
        }
    }
}