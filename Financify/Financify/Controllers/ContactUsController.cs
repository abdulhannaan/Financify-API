using BLL.Dtos;
using BLL.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Financify.Controllers
{
    [Produces("application/json")]
    [Route("api/contact-us")]
    public class ContactUsController : Controller
    {
        private ContactUsService contactUsService;

        public ContactUsController()
        {
            contactUsService = new ContactUsService();
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("Save")]
        [Route("save")]
        public IActionResult Save([FromBody] ContactU data)
        {
            var result = contactUsService.Save(data);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [ActionName("List")]
        [Route("list")]
        public IActionResult List()
        {
            ApiResponseMessage response = new ApiResponseMessage();
            var result = contactUsService.List();
            response.Message = result != null && result.Count > 0 ? "list found." : "No Record Found.";
            response.Status = HttpStatusCode.OK;
            response.Response = result;
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        [Route("delete")]
        public IActionResult Delete(int Id)
        {
            var result = contactUsService.Delete(Id);
            return Ok(result);
        }
    }
}
