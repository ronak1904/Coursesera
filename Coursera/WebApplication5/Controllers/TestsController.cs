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
    public class TestsController : Controller
    {
        private FileContext db = new FileContext();

        // GET: Tests
        public ActionResult Index(int? id)
        {
            if (Session["userType"] != null)
            {
                Session["allowed"] = true;
            List<Test> vlist = db.Tests.ToList();
            List<Test> tlist = new List<Test>();
            foreach (Test v in vlist)
            {
                if (v.Course.courseId == id)
                {
                    tlist.Add(v);
                }
            }
            return View(tlist);
            }
            else
            {
                return RedirectToAction("Errorpage", "Courses");
            }
        }

        
        public ActionResult TestBegin(int id)
        {
                if (Session["userType"] != null)
                {
                    Session["testId"] = id;
            if ((bool)Session["allowed"] == true)
            {
                return View(db.Questions.Where(x => x.Test.testId == id).ToList());
            }
            else
            {
                return RedirectToAction("Errorpage","courses");
            }
                }
                else
                {
                    return RedirectToAction("Errorpage", "Courses");
                }
            }
        [HttpPost]
        public ActionResult TestBegin(FormCollection data)
        {
            if (Session["userType"] != null)
            {
                int count = 0;
                int ttl = 0;
                int temp = Convert.ToInt32(Session["testId"]);
                var itr = db.Questions.Where(x => x.Test.testId == temp).ToList();
                foreach (Question x in itr)
                {
                    ttl++;
                    if (data[x.qId.ToString()].ToString().Equals(x.ans))
                    {
                        count++;
                    }
                }
                Session["ttl"] = ttl * 5;
                Session["count"] = count * 5;
                TestResult res = new TestResult();
                res.marks = count * 5;
                res.total = ttl * 5;
                res.Student = db.Students.Find(Session["userId"]);
                res.Test = db.Tests.Find(Session["testId"]);
                db.TestResults.Add(res);
                db.SaveChanges();
                return RedirectToAction("Result", "Tests");
            }
            else
            {
                return RedirectToAction("Errorpage", "Courses");
            }
        }

        public ActionResult Result()
        {
                if (Session["userType"] != null)
                {
                    Session["allowed"] = false;
            return View();
                }
                else
                {
                    return RedirectToAction("Errorpage", "Courses");
                }
            }
        // GET: Tests/Details/5
        public ActionResult Details(int? id)
        {
                    if (Session["userType"] != null)
                    {
                        if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
                    }
                    else
                    {
                        return RedirectToAction("Errorpage", "Courses");
                    }
                }

        // GET: Tests/Create
        public ActionResult Create(int? id)
        {
                        if (Session["userType"] != null)
                        {
                            Session["courseId"] = id;
            
            return View();
                        }
                        else
                        {
                            return RedirectToAction("Errorpage", "Courses");
                        }
                    }

        // POST: Tests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Test test)
        {
            if (Session["userType"] != null)
            {
                if (ModelState.IsValid)
            {
                test.Course = db.Course.Find(Session["courseId"]);
                db.Tests.Add(test);
                db.SaveChanges();
                return RedirectToAction("Index","Tests", new { @id = Session["courseId"] });
            }

            return View(test);
            }
            else
            {
                return RedirectToAction("Errorpage", "Courses");
            }
        }

        // GET: Tests/Edit/5
        public ActionResult Edit(int? id)
        {
                if (Session["userType"] != null)
                {
                    Session["testId"] = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
                }
                else
                {
                    return RedirectToAction("Errorpage", "Courses");
                }
            }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "testId,testName")] Test test)
        {
            if (Session["userType"] != null)
            {
                if (ModelState.IsValid)
            {
                db.Entry(test).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Tests", new { @id = Session["courseId"] });
            }
            return View(test);
            }
            else
            {
                return RedirectToAction("Errorpage", "Courses");
            }
        }

        // GET: Tests/Delete/5
        public ActionResult Delete(int? id)
        {
                if (Session["userType"] != null)
                {
                    Session["testId"] = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
                }
                else
                {
                    return RedirectToAction("Errorpage", "Courses");
                }
            }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["userType"] != null)
            {
                Test test = db.Tests.Find(id);
            

            var q = db.Questions.ToList();
            foreach (Question qq in q)
            {
                if (qq.Test.testId == test.testId)
                {
                    db.Questions.Remove(qq);
                    db.SaveChanges();
                }
            }
          

            db.Tests.Remove(test);
            db.SaveChanges();
            return RedirectToAction("Index","Tests",new { @id=Session["courseId"]});
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
