using BLL.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
