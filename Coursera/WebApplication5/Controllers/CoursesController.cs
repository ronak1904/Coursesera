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
    public class CoursesController : Controller
    {
        private FileContext db = new FileContext();
        public ActionResult Unsub(int id)
        {

            if (Session["userType"] != null)
            {
                Course c1 = db.Course.Find(id);
                c1.Students.Remove(db.Students.Find(Session["userId"]));
                //db.Students.Find(Session["userId"]);
                //db.Course.Add(c1);
 //               db.Entry(c1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewSubs", "Courses");
            }
            else
            {
                return RedirectToAction("Errorpage");
            }
        }
        public ActionResult ViewSubs()
        {
            if (Session["userType"] != null)
            {
                Student s = db.Students.Find(Session["userId"]);
                var x = db.Course.ToList();
                List<Course> clist = new List<Course>();
                foreach (Course c in x)
                {
                    bool decider = c.Students.Contains(s);
                    if (decider == true)
                    {
                        clist.Add(c);
                    }
                }
                return View(clist);
            }
            else
            {
                return RedirectToAction("Errorpage");
            }
        }
        // GET: Courses
        public ActionResult Index()
        {
            if (Session["userType"]!=null)
            {
                List<Course> vlist = db.Course.ToList();
                List<Course> tlist = new List<Course>();
                foreach (Course v in vlist)
                {
                    if (v.Teacher.tId == (int)Session["userId"])
                    {
                        tlist.Add(v);
                    }
                }
                return View(tlist);
            }
            else
            {
               return RedirectToAction("Errorpage");
            }
        }
        public ActionResult Errorpage()
        {
            return View();
        }
        public ActionResult Subs(int id)
        {
            if (Session["userType"] != null)
            {
                Course c1 = db.Course.Find(id);
                c1.Students.Add(db.Students.Find(Session["userId"]));
                //db.Students.Find(Session["userId"]);
                //db.Course.Add(c1);
                db.Entry(c1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewSubs", "Courses");
            }
            else
            {
                return RedirectToAction("Errorpage");
            }
        }
        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["userType"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Course course = db.Course.Find(id);
                if (course == null)
                {
                    return HttpNotFound();
                }
                return View(course);
            }
            else
            {
                return RedirectToAction("Errorpage");
            }
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            if (Session["userType"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Errorpage");
            }
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title,Category")] Course course)
        {
            if (Session["userType"] != null)
            {
                if (ModelState.IsValid)
                {
                    if (course.title.Length <= 0 || course.Category.Length <= 0)
                    {
                        ViewBag.errmsg = "The Title or Category Can not be empty";
                        return View();
                    }
                    course.Teacher = db.Teachers.Find(Session["userId"]);
                    db.Course.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(course);
            }
            else
            {
                return RedirectToAction("Errorpage");
            }
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["userType"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Course course = db.Course.Find(id);
                if (course == null)
                {
                    return HttpNotFound();
                }
                return View(course);
            }
            else
            {
                return RedirectToAction("Errorpage");
            }
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "courseId,title,Category")] Course course)
        {
            if (Session["userType"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(course).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(course);
            }
            else
            {
                return RedirectToAction("Errorpage");
            }
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["userType"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Course course = db.Course.Find(id);
                if (course == null)
                {
                    return HttpNotFound();
                }
                return View(course);
            }
            else
            {
                return RedirectToAction("Errorpage");
            }
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["userType"] != null)
            {
                Course course = db.Course.Find(id);
                var tst = db.Tests.ToList();
                foreach (Test x in tst)
                {
                    if (x.Course.courseId == course.courseId)
                    {
                        var q = db.Questions.ToList();
                        foreach (Question qq in q)
                        {
                            if (qq.Test.testId == x.testId)
                            {
                                db.Questions.Remove(qq);
                                db.SaveChanges();
                            }
                        }
                        db.Tests.Remove(x);
                        db.SaveChanges();
                    }
                }
                var vid = db.Videos.ToList();
                foreach (Video x in vid)
                {
                    if (x.Course.courseId == course.courseId)
                    {
                        db.Videos.Remove(x);
                        db.SaveChanges();
                    }
                }
                var fl = db.FileDetails.ToList();
                foreach (FileDetails x in fl)
                {
                    if (x.Course.courseId == course.courseId)
                    {
                        db.FileDetails.Remove(x);
                        db.SaveChanges();
                    }
                }

                List<Student> l = course.Students.ToList();
                foreach (Student ss in l)
                {
                    course.Students.Remove(ss);
                }
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                db.Course.Remove(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Errorpage");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (Session["userType"] != null)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
        }
    }
}
