using OElite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [NoCache]
    public class LoginController : Controller
    {
        FileContext db = new FileContext();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            Session["userId"] = null;
            Session["userName"] = null;
            Session["userType"] = null;
            Session.Remove("userType");
            Session.Remove("userName");
            Session.Remove("userId");

            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();

            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["lin"] = false;
            System.Web.HttpContext.Current.Application.UnLock();

            Response.ExpiresAbsolute = DateTime.Now;
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow);
           
            Response.Cache.SetValidUntilExpires(false);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
           
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetNoStore();


            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Expires", "Fri, 01 Jan 1990 00:00:00 GMT");
            Response.AddHeader("Pragma", "no-cache");
            ViewBag.count = 1;
            return RedirectToAction("Login");
        }


        [HttpPost]
        public ActionResult Login(FormCollection ln)
        {
            // ViewBag.msg = "";
            if (ln == null)
            {
                ViewBag.msg = "username is empty";
                return View();
            }
            else
            {
                if (ln["uType"].Equals("Teacher"))
                {
                    
                    string unm = ln["uName"];
                    string pwd = ln["pwd"];
                    List<Teacher> t = db.Teachers.Where(x => x.Fullname == unm && x.password == pwd).ToList();
                    if (!t.Any())
                    {
                        ViewBag.msg = "invalid credentials";
                        return View();
                    }
                    else
                    {
                        //t.First
                        
                        Session["userId"] = t.First().tId;
                        Session["userName"]= t.First().Fullname;
                        Session["userType"] = "Teacher";
                        System.Web.HttpContext.Current.Application.Lock();
                        System.Web.HttpContext.Current.Application["lin"] = true;
                        System.Web.HttpContext.Current.Application.UnLock();
                        return RedirectToAction("TeacherHome","Teacher");
                    }
                }
                else
                {
                    string unm = ln["uName"];
                    string pwd = ln["pwd"];
                    List<Student> s =db.Students.Where(x => x.studentName == unm && x.password == pwd).ToList();
                    if (!s.Any())
                    {
                        ViewBag.msg = "invalid credentials";
                        return View();
                    }
                    else
                    {
                        
                        Session["userId"] = s.First().studentId;
                        Session["userType"] = "Student";
                        Session["userName"] = s.First().studentName;
                        System.Web.HttpContext.Current.Application.Lock();
                        System.Web.HttpContext.Current.Application["lin"] = true;
                        System.Web.HttpContext.Current.Application.UnLock();
                        return RedirectToAction("StudentHome", "Student");
                    }
                }
            }
        }
    }
}