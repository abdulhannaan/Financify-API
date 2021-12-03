
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Financify.Controllers
{
    public abstract class BaseController : Controller
    {
        protected int GetUserId()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                return int.Parse(claims.FirstOrDefault(s => s.Type.Contains("nameid"))?.Value);
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }

        protected int GetUserCompanyId()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                return int.Parse(claims.FirstOrDefault(s => s.Type.Contains("surname"))?.Value);
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }

        protected string GetUsername()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                return claims.FirstOrDefault(s => s.Type.Contains("uniquename"))?.Value;
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }

        protected string GetUserEmailAddress()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                return claims.FirstOrDefault(s => s.Type.Contains("email"))?.Value;
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }
    }
}
