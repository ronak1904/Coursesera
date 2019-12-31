using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class RegisterController : Controller
    {
        FileContext db = new FileContext();
        // GET: Register
        public ActionResult TeacherReg()
        {
            
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TeacherReg([Bind(Include = "tId,Fullname,email,password,mobileno")] Teacher teacher)
        {
            
                if (ModelState.IsValid)
            {
                List<Teacher> lst = db.Teachers.Where(f => f.Fullname == teacher.Fullname).ToList();

                if (lst.Any())
                {
                    ViewBag.ermsg = "The User With the same name already exists please change the name and try again";
                    return View("TeacherReg");
                }
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Login","Login");
            }

            return View(teacher);
        }

        public ActionResult StudentReg()
        {
            
                    return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentReg([Bind(Include = "password,studentName,emailId")] Student student)
        {
            
                if (ModelState.IsValid)
            {
                List<Student> lst = db.Students.Where(f => f.studentName == student.studentName).ToList();

                if (lst.Any())
                {
                    ViewBag.ermsg = "The User With the same name already exists please change the name and try again";
                    return View("StudentReg");
                }
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Login","Login");
            }

            return View(student);
        }
    }
}