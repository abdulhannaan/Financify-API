using BLL.Dtos;
using BLL.Services;
using Utility.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace Financify.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : BaseController
    {
        private UserService userService;
        private readonly MailSettingsDto _mailSettings;

        public UserController(IOptions<MailSettingsDto> mailSettings)
        {
            _mailSettings = mailSettings.Value;
            userService = new UserService();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("sign-up")]
        public IActionResult SignUp([FromBody] UserDto user)
        {
            var result = userService.SignUp(user);

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            ApiResponseMessage response = new ApiResponseMessage();
            var result = userService.List();

            response.Message = result != null && result.Count > 0 ? "Users list found." : "Not Record Found.";
            response.Status = HttpStatusCode.OK;
            response.Response = result;

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("save")]
        public ActionResult Save([FromBody] UserDto data)
        {
            var loggedInUserId = GetUserId();
            var result = userService.Save(data, loggedInUserId);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("get")]
        public ActionResult Get(int Id)
        {

            ApiResponseMessage response = new ApiResponseMessage();
            var result = userService.Get(Id);

            if (result != null && result.Id > 0)
                response.Message = "user Found";
            else
                response.Message = "Not Found";
            response.Status = HttpStatusCode.OK;
            response.Response = result;

            return Ok(response);
        }

        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public ActionResult Delete(string Ids)
        {
            ApiResponseMessage response = new ApiResponseMessage();
            var result = userService.Delete(Ids);

            response.Message = result ? "Deleted" : "Not Deleted";
            response.Status = HttpStatusCode.OK;
            response.Response = result;

            return Ok(response);
        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("forgot-password")]
        //public ActionResult ForgotPassword(string emailAddress)
        //{
        //    ApiResponseMessage response = new ApiResponseMessage();
        //    if (Helper.IsValidEmail(emailAddress))
        //    {
        //        var loggedInUserId = GetUserId();
        //        var result = userService.ForgotPassword(emailAddress, _mailSettings, loggedInUserId);
        //        response.Message = result;
        //        response.Response = result;
        //    }
        //    else
        //    {
        //        response.Message = "Invalid Email Address.";
        //        response.Response = false;
        //    }

        //    response.Status = HttpStatusCode.OK;
        //    return Ok(response);
        //}

        //[Authorize]
        //[HttpPost]
        //[Route("change-password")]
        //public ActionResult ChangePassword([FromBody] PasswordDto passwordRequest)
        //{
        //    var loggedInUserId = GetUserId();
        //    ApiResponseMessage response = new ApiResponseMessage();
        //    var result = userService.ChangePassword(passwordRequest, loggedInUserId);
        //    response.Message = result;
        //    response.Response = result;
        //    response.Status = HttpStatusCode.OK;

        //    return Ok(response);
        //}


        [AllowAnonymous]
        [HttpGet]
        [Route("verify")]
        public IActionResult Verify(string type, string value, int userId)
        {
            ApiResponseMessage response = new ApiResponseMessage();
            var result = userService.ValidateUser(type, value, userId);

            response.Status = HttpStatusCode.OK;
            response.Response = result;
            if (result)
                response.Message = "This " + type + " already exists. Please try a new one.";
            else
                response.Message = "This " + type + " does not exist.";

            return Ok(result);
        }


    }
}
