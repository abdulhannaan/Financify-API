using BLL.Dtos;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HRM.Controllers
{
    [Produces("application/json")]
    [Route("api/role")]
    public class RoleController : Controller
    {
        RoleService roleService = new RoleService();



        [Authorize]
        [HttpPost]
        [Route("save")]
        public ActionResult Save([FromBody] RoleDto data)
        {
            ApiResponseMessage response = new ApiResponseMessage();
            var result = roleService.Save(data);
            if (result)
            {
                response.Message = "Role saved successfully.";
                response.Status = HttpStatusCode.OK;
                response.Response = result;
            }
            else
            {
                response.Message = "Fail to save role.";
                response.Status = HttpStatusCode.InternalServerError;
                response.Response = null;
            }
            return Ok(response);
        }

        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public ActionResult Delete(int Id)
        {
            ApiResponseMessage response = new ApiResponseMessage();
            var result = roleService.Delete(Id);

            response.Message = result ? "Deleted" : "Not Deleted";
            response.Status = HttpStatusCode.OK;
            response.Response = result;

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("get")]
        public ActionResult Get(int Id)
        {
            ApiResponseMessage response = new ApiResponseMessage();
            var result = roleService.Get(Id);

            if (result != null && result.RoleId > 0)
                response.Message = "Success All Roles Found";
            else
                response.Message = "Not Found";
            response.Status = HttpStatusCode.OK;
            response.Response = result;

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("getall")]
        public ActionResult GetAll()
        {
            ApiResponseMessage response = new ApiResponseMessage();
            var result = roleService.GetAll();

            if (result != null && result.Count > 0)
                response.Message = "Success All Roles Found";
            else
                response.Message = "Not Found";
            response.Status = HttpStatusCode.OK;
            response.Response = result;

            return Ok(response);
        }

    }
}
