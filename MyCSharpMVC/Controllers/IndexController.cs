using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace MyCSharpMVC.Controllers
{
    public class IndexController : Controller
    {
        // GET: 访问 /Index
        public ActionResult Index()
        {
            Object data = new
            {
                str = "Hello, Index"
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // Get 访问 /Index/GetSomething 路径获取 Json 数据
        public ActionResult GetSomething()
        {
            Object data = new
            {
                str = "Hello, GetSomething"
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // Post 访问 /Index/PostSomething 路径获取 Json 数据, 发送数据如： [{"testTxt":"Hello"}, {"testTxt":"World"}]
        [HttpPost]
        public ActionResult PostSomething(List<TestBo> param)
        {
            Object data = new
            {
                str = param[0].testTxt,
                str2 = param[1].testTxt
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}