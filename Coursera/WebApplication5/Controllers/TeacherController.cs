using OElite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [NoCache]
    public class TeacherController : Controller
    {
        private FileContext db = new FileContext();

        // GET: Teacher
        public ActionResult Index()
        {
            if (Session["userType"] != null)
            {
                return View(db.Teachers.ToList());
            }
            else
            {
                return RedirectToAction("Errorpage", "Courses");
            }
        }
        public ActionResult TeacherHome()
        {
                if (Session["userType"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Errorpage", "Courses");
                }
            }
        // GET: Teacher/Details/5
        public ActionResult Details(int? id)
        {
                    if (Session["userType"] != null)
                    {
                        if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
                    }
                    else
                    {
                        return RedirectToAction("Errorpage", "Courses");
                    }
                }

        // GET: Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
                        if (Session["userType"] != null)
                        {
                            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
                        }
                        else
                        {
                            return RedirectToAction("Errorpage", "Courses");
                        }
                    }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tId,Fullname,email,password,mobileno")] Teacher teacher)
        {
            if (Session["userType"] != null)
            {
                if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Tests",new { id=Session["courseId"]});
            }
            return View(teacher);
            }
            else
            {
                return RedirectToAction("Errorpage", "Courses");
            }
        }

        // GET: Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
                if (Session["userType"] != null)
                {
                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
                }
                else
                {
                    return RedirectToAction("Errorpage", "Courses");
                }
            }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["userType"] != null)
            {
                Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index", "Tests", new { id = Session["courseId"] });
            }
            else
            {
                return RedirectToAction("Errorpage", "Courses");
            }
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
