using DataAccessLayer;
using OnlineLibraryMgtSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLibraryMgtSystem.Controllers
{
    public class PurchaseController : Controller
    {
        private OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();
        // GET: Purchase
        public ActionResult NewPurchase()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");

            double totalamount = 0;
            var temppur = db.PurTemDetailsTables.ToList();
            foreach (var item in temppur)
            {
                totalamount += (item.Qty * item.UnitPrice);
            }
            ViewBag.TotalAmount = totalamount;
            return View(temppur);
        }

        // GET: Purchase/PurchaseCart
        public ActionResult AddItem(int BID, int Qty, float Price)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");

            var isAvailable = db.PurTemDetailsTables.Any(b => b.BookID == BID);
            if (isAvailable != true)
            {
                if (BID > 0 && Qty > 0 && Price > 0)
                {
                    var newItem = new PurTemDetailsTable()
                    {
                        BookID = BID,
                        Qty = Qty,
                        UnitPrice = Price
                    };

                    db.PurTemDetailsTables.Add(newItem);
                    db.SaveChanges();
                    ViewBag.Message = "Book Added Successfully";
                }
            }
            else
            {
                ViewBag.Message = "Already Exist! Please Check";
            }
            return RedirectToAction("NewPurchase");
        }

        public ActionResult DeleteConfirm(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");

            var book = db.PurTemDetailsTables.Find(id);
            if (book != null)
            {
                db.Entry(book).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                ViewBag.Message = "Deleted Successfully";
                return RedirectToAction("NewPurchase");
            }
            ViewBag.Message = "Some unexpected issue is occurse";
            return View("NewPurchase");
        }

        [HttpGet]
        public ActionResult GetBooks()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");


            List<BookMV> list = new List<BookMV>();
            var booklist = db.BookTables.ToList();
            foreach (var item in booklist)
            {
                list.Add(new BookMV
                {
                    BookName = item.BookName,
                    BookID = item.BookID
                });
            }
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CancelPurchase()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");

            var list = db.PurTemDetailsTables.ToList();
            bool cancelstatus = false;
            foreach (var item in list)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                int noofrecoreds = db.SaveChanges();
                if (cancelstatus == false)
                {
                    if (noofrecoreds > 0)
                        cancelstatus = true;

                }
            }
            if (cancelstatus == true)
            {
                ViewBag.Message = "Purchase is Canceled";
                return RedirectToAction("NewPurchase");
            }
            ViewBag.Message = "Some Unexpected issue is occur";
            return RedirectToAction("NewPurchase");
        }

        [HttpPost]
        public ActionResult PurchaseConfirm(FormCollection collection)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");

            int userid =Convert.ToInt32(Convert.ToString(Session["uID"]));
            int supplierid = 0;
            string[] keys = collection.AllKeys;
            foreach (var name in keys)
            {
                if (name.Contains("name"))
                {
                    string idname = name;
                    string[] valueids = idname.Split(' ');
                    supplierid = Convert.ToInt32(valueids[1]);
                }
               supplierid = Convert.ToInt32(keys[1]);
            }

            var puchasedetails = db.PurchaseDetailTables.ToList();
            double totalamount = 0;
            foreach (var item in puchasedetails)
            {
                totalamount = totalamount + (item.Qty * item.UnitPrice);
            }
            if (totalamount==0)
            {
                ViewBag.Message = "Purchase Cart Empty!";
                return View("NewPurchase");
            }

            var purchaseheader = new PurchaseTable();
            purchaseheader.SupplierID = supplierid;
            purchaseheader.PurchaseDate = DateTime.Now;
            purchaseheader.PurchaseAmount = totalamount;
            purchaseheader.UserID = userid;
            db.PurchaseTables.Add(purchaseheader);
            db.SaveChanges();

            foreach (var item in puchasedetails)
            {
                var purdetails = new PurchaseDetailTable()
                {
                    BookID = item.BookID,
                    PurchaseID = purchaseheader.PurchaseID,
                    Qty = item.Qty,
                    UnitPrice = item.UnitPrice
                };
                db.PurchaseDetailTables.Add(purdetails);
                db.SaveChanges();

                var updatebookstock = db.BookTables.Find(item.BookID);
                updatebookstock.TotalCopies = updatebookstock.TotalCopies + item.Qty;
                updatebookstock.Price = item.UnitPrice;
                db.Entry(updatebookstock).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            ViewBag.Message = "Purchase confirm successfully!";
            return RedirectToAction("AllPurchase");

        }

        public ActionResult AllPurchase()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");

            return View(db.PurchaseTables.ToList());
        }

        public ActionResult SelectSupplier()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");

            var purchasedetails = db.PurTemDetailsTables.ToList();
            if (purchasedetails.Count==0)
            {
                ViewBag.Message = "Purchase cart empty";
                return View("NewPurchase");
            }
            var supplier = db.SupplierTables.ToList();
            return View(supplier);

        }


       

    }
}
