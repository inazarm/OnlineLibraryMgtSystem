using DataAccessLayer;
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
            return View(db.PurTemDetailsTables.ToList());
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

       
    }
}
