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
    public class PurchaseTablesController : Controller
    {
        private OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();

        // GET: PurchaseTables
        public ActionResult Index()
        {
            var purchaseTables = db.PurchaseTables.Include(p => p.SupplierTable).Include(p => p.UserTable);
            return View(purchaseTables.ToList());
        }

        // GET: PurchaseTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseTable purchaseTable = db.PurchaseTables.Find(id);
            if (purchaseTable == null)
            {
                return HttpNotFound();
            }
            return View(purchaseTable);
        }

        // GET: PurchaseTables/Create
        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(db.SupplierTables, "SupplierID", "SupplierName");
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName");
            return View();
        }

        // POST: PurchaseTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchaseID,PurchaseDate,UserID,PurchaseAmount,SupplierID")] PurchaseTable purchaseTable)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseTables.Add(purchaseTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierID = new SelectList(db.SupplierTables, "SupplierID", "SupplierName", purchaseTable.SupplierID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", purchaseTable.UserID);
            return View(purchaseTable);
        }

        // GET: PurchaseTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseTable purchaseTable = db.PurchaseTables.Find(id);
            if (purchaseTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierID = new SelectList(db.SupplierTables, "SupplierID", "SupplierName", purchaseTable.SupplierID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", purchaseTable.UserID);
            return View(purchaseTable);
        }

        // POST: PurchaseTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchaseID,PurchaseDate,UserID,PurchaseAmount,SupplierID")] PurchaseTable purchaseTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(db.SupplierTables, "SupplierID", "SupplierName", purchaseTable.SupplierID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "UserName", purchaseTable.UserID);
            return View(purchaseTable);
        }

        // GET: PurchaseTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseTable purchaseTable = db.PurchaseTables.Find(id);
            if (purchaseTable == null)
            {
                return HttpNotFound();
            }
            return View(purchaseTable);
        }

        // POST: PurchaseTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseTable purchaseTable = db.PurchaseTables.Find(id);
            db.PurchaseTables.Remove(purchaseTable);
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
