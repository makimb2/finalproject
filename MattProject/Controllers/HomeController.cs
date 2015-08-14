using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using MattProject.Database;
using MattProject.Models;

namespace MattProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Login Page";

            var model = new LoginPageModel();

            return View(model);
        }

        public ActionResult Login(LoginPageModel model)
        {
            
            User user;

            using (var db = new UsersDataContext())
            {
                user = db.Users.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);
            }

            if (user != null)
            {
                var log = new Log() { DateTime = DateTime.Now, UserId = user.Id };

                using (var db = new LogDataContext())
                {
                    db.Logs.InsertOnSubmit(log);

                    db.SubmitChanges();
                }

                var userModel = new UserModel() {UserId = user.Id};
                return View("Secret", userModel);
            }
            else
            {
                //var errorModel = new LoginPageModel();
                ModelState.AddModelError("Invalid Login", "The user name and password combination is incorrect.  Try again.");
            }

            return View("Index");
        }

    }
}
