using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            ViewBag.Title = "LoginPage";
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginModel)
        {
            if(loginModel.UserName == "testaccount" && loginModel.Password == "testaccount")
            {
                //setlogin
                loginModel.AccessKey = "123456789";
                loginModel.Id = 1;
                var context = HttpContext;
                context.Session["LoggedInUserName"] = loginModel.UserName;
                context.Session["LoggedInUserAccessKey"] = loginModel.AccessKey;

                return RedirectToAction("Index", "Home", new { Login = true } );
            }
            else
            {
                return HttpNotFound();
            }
        }

    }
}