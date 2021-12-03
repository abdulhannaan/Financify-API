using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financify.Controllers
{
    [Produces("application/json")]
    [Route("short/list")]
    public class CommonController : Controller
    {
        CommonService commonHandler = null;

        public CommonController()
        {
            commonHandler = new BLL.Services.CommonService();
        }

        [Authorize]
        [HttpGet]
        [Route("countries")]
        public ActionResult Countries()
        {
            var result = commonHandler.Countries();
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("cities")]
        public ActionResult Cities(int stateId)
        {
            var result = commonHandler.Cities(stateId);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("states")]
        public ActionResult States(int countryId)
        {
            var result = commonHandler.States(countryId);
            return Ok(result);
        }

    }
}
