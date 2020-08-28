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
        public ActionResult AddItem(int BID,int Qty,float Price)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["uID"])))
                return RedirectToAction("Login", "Home");

            var isAvailable = db.PurTemDetailsTables.Any(b => b.BookID == BID);
            if (isAvailable!=true)
            {
                if (BID>0 && Qty>0 && Price>0)
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
            if (book!=null)
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
    }
}
