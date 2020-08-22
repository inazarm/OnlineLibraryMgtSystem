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
    public class DesignationTablesController : Controller
    {
        private OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();

        // GET: DesignationTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");
            return View(db.DesignationTables.ToList());
        }

        // GET: DesignationTables/Details/5
        public ActionResult Details(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DesignationTable designationTable = db.DesignationTables.Find(id);
            if (designationTable == null)
            {
                return HttpNotFound();
            }
            return View(designationTable);
        }

        // GET: DesignationTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DesignationTable designationTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");
            if (ModelState.IsValid)
            {
                db.DesignationTables.Add(designationTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", designationTable.UserID);
            return View(designationTable);
        }

        // GET: DesignationTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DesignationTable designationTable = db.DesignationTables.Find(id);
            if (designationTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", designationTable.UserID);
            return View(designationTable);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( DesignationTable designationTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");
            if (ModelState.IsValid)
            {
                db.Entry(designationTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", designationTable.UserID);
            return View(designationTable);
        }

        // GET: DesignationTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DesignationTable designationTable = db.DesignationTables.Find(id);
            if (designationTable == null)
            {
                return HttpNotFound();
            }
            return View(designationTable);
        }

        // POST: DesignationTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DesignationTable designationTable = db.DesignationTables.Find(id);
            db.DesignationTables.Remove(designationTable);
            db.SaveChanges();
            return RedirectToAction("Index");
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
