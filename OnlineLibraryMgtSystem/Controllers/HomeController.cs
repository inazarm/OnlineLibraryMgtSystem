using DataAccessLayer;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineLibraryMgtSystem.Controllers
{
    public class HomeController : Controller
    {
        OnlineLibraryMgtSystemDbEntities db = new OnlineLibraryMgtSystemDbEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            try
            {
                if (username != null && password != null)
                {
                    var finduser = db.UserTables.Where(u => u.UserName == username && u.Password == password && u.IsActive == true).ToList();
                    if (finduser.Count() == 1)
                    {
                        Session["uID"] = finduser[0].UserID;
                        Session["uTypeID"] = finduser[0].UserTypeID;
                        Session["uName"] = finduser[0].UserName;
                        Session["uPassword"] = finduser[0].Password;
                        Session["employeeID"] = finduser[0].EmployeeID;
                        string url = string.Empty;

                        if (finduser[0].UserTypeID == 2)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 3)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 4)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 1)
                        {
                            url = "About";
                        }
                        else
                        {
                            url = "About";
                        }
                        return RedirectToAction(url);
                    }
                    else
                    {
                        Session["uID"] = string.Empty;
                        Session["uTypeID"] = string.Empty;
                        Session["uName"] = string.Empty;
                        Session["uPassword"] = string.Empty;
                        Session["employeeID"] = string.Empty;
                        ViewBag.message = "User Name or Password is incorrect!";
                    }
                }
                else
                {
                    Session["uID"] = string.Empty;
                    Session["uTypeID"] = string.Empty;
                    Session["uName"] = string.Empty;
                    Session["uPassword"] = string.Empty;
                    Session["employeeID"] = string.Empty;
                    ViewBag.message = "some unexpected issue occure please try again!!";
                }
            }
            catch (Exception ex)
            {
                Session["uID"] = string.Empty;
                Session["uTypeID"] = string.Empty;
                Session["uName"] = string.Empty;
                Session["uPassword"] = string.Empty;
                Session["employeeID"] = string.Empty;
                ViewBag.message = "some unexpected issue occure please try again!!";
            }
            return View("login");
        }

        public ActionResult Logout()
        {
            Session["uID"] = string.Empty;
            Session["uTypeID"] = string.Empty;
            Session["uName"] = string.Empty;
            Session["uPassword"] = string.Empty;
            Session["employeeID"] = string.Empty;
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Logout");
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}