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
    public class QuestionsController : Controller
    {
        private FileContext db = new FileContext();

        // GET: Questions
        public ActionResult Index(int id)
        {
            if (Session["userType"] != null)
            {
                List<Question> vlist = db.Questions.ToList();
            List<Question> tlist = new List<Question>();
            foreach (Question v in vlist)
            {
                if (v.Test.testId == id)
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
            // return View(db.Questions.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
                if (Session["userType"] != null)
                {
                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
                }
                else
                {
                    return RedirectToAction("Errorpage", "Courses");
                }
            }

        // GET: Questions/Create
        public ActionResult Create(int id)
        {
                    if (Session["userType"] != null)
                    {
                        Session["testId"] = id;
            return View();
                    }
                    else
                    {
                        return RedirectToAction("Errorpage", "Courses");
                    }
                }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "qId,qText,op1,op2,op3,op4,ans")] Question question)
        {
            if (Session["userType"] != null)
            {
                if (ModelState.IsValid)
            {
                question.Test = db.Tests.Find(Session["testId"]);
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index","Questions",new { @id= Session["testId"] });
            }

            return View(question);
            }
            else
            {
                return RedirectToAction("Errorpage", "Courses");
            }
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
                if (Session["userType"] != null)
                {
                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
                }
                else
                {
                    return RedirectToAction("Errorpage", "Courses");
                }
            }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "qId,qText,op1,op2,op3,op4,ans")] Question question)
        {
            if (Session["userType"] != null)
            {
                if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Questions",new { @id=Session["testId"]});
            }
            return View(question);
            }
            else
            {
                return RedirectToAction("Errorpage", "Courses");
            }
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
                if (Session["userType"] != null)
                {
                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
                }
                else
                {
                    return RedirectToAction("Errorpage", "Courses");
                }
            }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["userType"] != null)
            {
                Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index", "Questions", new { @id = Session["testId"] });
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
