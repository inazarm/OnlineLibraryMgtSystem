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
    public class SupplierTablesController : Controller
    {
        private OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();

        // GET: SupplierTables
        public ActionResult Index()
        {
            var supplierTables = db.SupplierTables.Include(s => s.UserTable);
            return View(supplierTables.ToList());
        }

        // GET: SupplierTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierTable supplierTable = db.SupplierTables.Find(id);
            if (supplierTable == null)
            {
                return HttpNotFound();
            }
            return View(supplierTable);
        }

        // GET: SupplierTables/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName");
            return View();
        }

        // POST: SupplierTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierID,SupplierName,UserID,ContactNo,Email,Description")] SupplierTable supplierTable)
        {
            if (ModelState.IsValid)
            {
                db.SupplierTables.Add(supplierTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", supplierTable.UserID);
            return View(supplierTable);
        }

        // GET: SupplierTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierTable supplierTable = db.SupplierTables.Find(id);
            if (supplierTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", supplierTable.UserID);
            return View(supplierTable);
        }

        // POST: SupplierTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierID,SupplierName,UserID,ContactNo,Email,Description")] SupplierTable supplierTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", supplierTable.UserID);
            return View(supplierTable);
        }

        // GET: SupplierTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierTable supplierTable = db.SupplierTables.Find(id);
            if (supplierTable == null)
            {
                return HttpNotFound();
            }
            return View(supplierTable);
        }

        // POST: SupplierTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupplierTable supplierTable = db.SupplierTables.Find(id);
            db.SupplierTables.Remove(supplierTable);
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
