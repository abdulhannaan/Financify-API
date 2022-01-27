using BLL.Dtos;
using BLL.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Financify.Controllers
{
    [Produces("application/json")]
    [Route("api/join-us")]
    public class JoinUsController : Controller
    {
        private JoinUsService joinUsService;

        public JoinUsController()
        {
            joinUsService = new JoinUsService();
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("Save")]
        [Route("save")]
        public IActionResult Save([FromBody] JoinU data)
        {
            var result = joinUsService.Save(data);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [ActionName("List")]
        [Route("list")]
        public IActionResult List()
        {
            ApiResponseMessage response = new ApiResponseMessage();
            var result = joinUsService.List();
            response.Message = result != null && result.Count > 0 ? "list found." : "No Record Found.";
            response.Status = HttpStatusCode.OK;
            response.Response = result;
            return Ok(response);
        }

        [Authorize]
        [HttpDelete]
        [ActionName("Delete")]
        [Route("delete")]
        public IActionResult Delete(int Id)
        {
            var result = joinUsService.Delete(Id);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [ActionName("View")]
        [Route("view")]
        public IActionResult View(int Id)
        {
            ApiResponseMessage response = new ApiResponseMessage();
            var result = joinUsService.View(Id);
            response.Message = result != null ? "Record found." : "No Record Found.";
            response.Status = HttpStatusCode.OK;
            response.Response = result;
            return Ok(response);
        }
    }
}
