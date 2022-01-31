using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using Financify.Common;
using BLL.Dtos;
using BLL.Services;

namespace Financify.Controllers
{
    [Produces("application/json")]
    [Route("api/oauth/Token")]
    public class TokenController : Controller
    {
        private IConfiguration configuration;

        public TokenController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("CreateToken")]
        [Route("createToken")]
        public IActionResult CreateToken([FromBody] LoginModel Content)
        {

            string Username = Content.Username;
            string Password = Content.Password;
            if (!ModelState.IsValid)
                return Unauthorized(new { responseMessage = "Please enter Valid Username and Password." });

            var result = new UserService().Login(Username, Password);

            if (result == null || result.User.Id == 0)
                return Unauthorized(new { responseMessage = "Please enter Valid Username and Password." });

            var tokenStr = new Utils(configuration).BuildToken(result.User);

            var response = new
            {
                Token = tokenStr,
                User = result,
                Expires = DateTime.Now.AddMinutes(810)
            };

            return Ok(response);
        }

    }

}