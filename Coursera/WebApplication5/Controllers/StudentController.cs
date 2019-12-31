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
    public class StudentController : Controller
    {
        private FileContext db = new FileContext();

        // GET: Student
        public ActionResult Index()
        {
            if (Session["userType"] != null)
            {
                return View(db.Students.ToList());
            }
            else
            {
                return RedirectToAction("Errorpage", "Courses");
            }
        }
        
        public ActionResult StudentHome()
        {
                if (Session["userType"] != null)
                {
                    return View(db.Course.ToList());
                }
                else
                {
                    return RedirectToAction("Errorpage", "Courses");
                }
            }

        // GET: Student/Details/5
        public ActionResult Details(string id)
        {
                    if (Session["userType"] != null)
                    {
                        if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
                    }
                    else
                    {
                        return RedirectToAction("Errorpage", "Courses");
                    }
                }

        

        // GET: Student/Edit/5
        public ActionResult Edit(string id)
        {
                        if (Session["userType"] != null)
                        {
                            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
                        }
                        else
                        {
                            return RedirectToAction("Errorpage", "Courses");
                        }
                    }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "studentId,password,studentName,emailId")] Student student)
        {
            if (Session["userType"] != null)
            {
                if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
            }
            else
            {
                return RedirectToAction("Errorpage", "Courses");
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(string id)
        {
                if (Session["userType"] != null)
                {
                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
                }
                else
                {
                    return RedirectToAction("Errorpage", "Courses");
                }
            }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["userType"] != null)
            {
                Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
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
