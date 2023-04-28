using Hwyeom.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hwyeom.Web.Controllers
{
    public class MembershipController : Controller
    {
        private IPasswordHasher _hasher;
        private HttpContext _httpContext;

        public MembershipController(IPasswordHasher hasher, IHttpContextAccessor accessor)
        {
            _hasher = hasher;
            _httpContext = accessor.HttpContext;
        }

        public IActionResult Login()
        {

            return View();
        }
    }
}
