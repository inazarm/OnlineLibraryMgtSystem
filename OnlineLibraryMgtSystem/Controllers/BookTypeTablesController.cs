using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;

namespace OnlineLibraryMgtSystem.Controllers
{
    public class BookTypeTablesController : Controller
    {
        private OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();

        // GET: BookTypeTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");
            return View(db.BookTypeTables.ToList());
        }

        // GET: BookTypeTables/Details/5
        public ActionResult Details(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTypeTable bookTypeTable = db.BookTypeTables.Find(id);
            if (bookTypeTable == null)
            {
                return HttpNotFound();
            }
            return View(bookTypeTable);
        }

        // GET: BookTypeTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookTypeTable bookTypeTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");
            int userID = Convert.ToInt32(Session["uID"]);
            bookTypeTable.UserID = userID;
            if (ModelState.IsValid)
            {
                db.BookTypeTables.Add(bookTypeTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookTypeTable);
        }

        // GET: BookTypeTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTypeTable bookTypeTable = db.BookTypeTables.Find(id);
            if (bookTypeTable == null)
            {
                return HttpNotFound();
            }
            return View(bookTypeTable);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookTypeTable bookTypeTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");
            int userID = Convert.ToInt32(Session["uID"]);
            bookTypeTable.UserID = userID;
            if (ModelState.IsValid)
            {
                db.Entry(bookTypeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookTypeTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
