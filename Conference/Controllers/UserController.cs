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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ConfModel.Model.User Get()
        {
            var employees = new ConfModel.Model.User() { Name = "Alex" };
            return employees;
        }
        
        [HttpGet("users")]
        public IActionResult GetUserByEmail([FromQuery(Name = "email")]string email)
        {
            return Ok(_userService.GetUserByEmail(email));
        }

        [Authorize]
        [HttpGet("user/myname")]
        public IActionResult GetName()
        {
            var idS = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var idS = User.Identity.Name;
            var user = _userService.Get(Convert.ToInt32(idS));
            
            return Ok(new { name = user.Name });
        }

        [HttpPost("auth/signup")]
        public IActionResult SignUp([FromBody] UserDto userDto)
        {
            
            var userId = _userService.Add(userDto);
            return Ok(userId);
        }
        
        [HttpPost("auth/signin")]
        public IActionResult SignIn([FromBody] UserAuthDto userAuthDto)
        {
            var token = _userService.Authenticate(userAuthDto);
            return Ok(token);
        }
    }
}