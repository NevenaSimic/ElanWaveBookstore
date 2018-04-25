using ElanWaveBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElanWaveBookstore.Controllers
{
    public class UserController : Controller
    {
        UserContext db = new UserContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.User.ToList());
        }


        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreateUser(int? id, User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                if (user != null)
                {
                    Session["LogedUserID"] = user.UserAccountID.ToString();
                    Session["LogedUserName"] = user.Username.ToString();
                    return RedirectToAction("Index", "Book");
                }
 
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult SignedUp(User user)
        {
            ViewBag.Message = "";
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();

                return RedirectToAction("Index", "Book");
            }
            return RedirectToAction("Index", "Book");
        }

        //public ActionResult CheckUsernameExist(string userdata)
        //{
        //    System.Threading.Thread.Sleep(200);
        //    var searchData = db.User.Where(x => x.Username == userdata).SingleOrDefault();
        //    if(searchData != null)
        //    {
        //        return Json(1);
        //    }
        //    else
        //    {
        //        return Json(0);
        //    }
        //}
    }
}