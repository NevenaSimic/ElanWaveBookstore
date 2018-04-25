using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ElanWaveBookstore.Models;


namespace ElanWaveBookstore.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(User user)
        {
             UserContext db = new UserContext();
             var v = db.User.Where(a => a.Username.Equals(user.Username) && a.Password.Equals(user.Password)).FirstOrDefault();
             if (v != null)
             {
                 Session["LogedUserID"] = v.UserAccountID.ToString();
                 Session["LogedUserName"] = v.Username.ToString();
                 return RedirectToAction("Index", "Book");
             }
            
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            if (Session["LogedUserID"] != null)
            {
                Session["LogedUserID"] = null;
                Session["LogedUserName"] = null;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}