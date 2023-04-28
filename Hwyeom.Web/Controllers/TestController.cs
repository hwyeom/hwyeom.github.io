using Hwyeom.Data.ViewModels;
using Hwyeom.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hwyeom.Web.Controllers
{
    public class TestController : Controller
    {
        //의존성 주입
        private HttpContext _context;
        private string _sessionMessageName = "_sessionMessage";

        public TestController(IHttpContextAccessor contextAccessor)
        {
            _context = contextAccessor.HttpContext;
        }

        private List<TestMessageInfo> GetMSGInfos(ref string message)
        {
            var msgInfos = _context.Session.Get<List<TestMessageInfo>>(key: _sessionMessageName);

            if (msgInfos == null || msgInfos.Count() < 1)
            {
                message = "메시지가 없습니다.";
            }

            return msgInfos;
        }

        private void SetMSGInfos(TestMessageInfo item, List<TestMessageInfo> msgInfos = null)
        {
            if (msgInfos == null)
            {
                msgInfos = _context.Session.Get<List<TestMessageInfo>>(_sessionMessageName);

                if (msgInfos == null)
                {
                    msgInfos = new List<TestMessageInfo>();
                }
            }

            msgInfos.Add(item);
            _context.Session.Set<List<TestMessageInfo>>(_sessionMessageName, msgInfos);
        }

        public IActionResult Index()
        {
            string message = string.Empty;
            var msgInfos = GetMSGInfos(ref message);

            ViewData["Message"] = message;

            return View(msgInfos);
        }
    }
}
