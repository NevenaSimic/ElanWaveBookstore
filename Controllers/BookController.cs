using ElanWaveBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ElanWaveBookstore.Controllers
{
    public class BookController : Controller
    {
        BookContext db = new BookContext();

        public ActionResult Index()
        {
            if(Session["LogedUserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            } 

            return View(db.Book.Where(a => a.IsDeleted.Equals(0)).ToList());
        }

        [HttpGet]
        public ActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Book.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Book");
                }
                return View(book);
            }
            catch
            {
                return View();
            }
            
        }

        public ActionResult EditBook(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Book book = db.Book.Find(id);
            if (book == null)
                return HttpNotFound();
            return View(book);
        }

        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Book");
                }
                return View(book);
            }
            catch
            {
                return View();
            }
        }

        //[HttpPost]
        public ActionResult DeleteBook(int? id)
        {
            Book book = db.Book.Find(id);
            book.IsDeleted = 1;
            db.SaveChanges();
            return RedirectToAction("Index", "Book");
        }

    }
}