using OElite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace practice13.Controllers
{

    [NoCache]
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        FileContext db = new FileContext();
        public ActionResult Index(int id)
        {
            if (Session["userType"] != null)
            {
                Session["courseId"] = id;
                return View();
            }
            else
            {
                return RedirectToAction("Errorpage","Courses");
            }
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase fileName, FormCollection data, FileDetails dbfile)
        {
            if (Session["userType"] != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {


                        if (fileName != null && fileName.ContentType == "application/pdf" || fileName.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                        {
                            List<FileDetails> lst = db.FileDetails.Where(f => f.fileName == fileName.FileName).ToList();
                            if (lst.Any())
                            {
                                ViewBag.ermsg = "The File With the same name already exists please change the name and try again";
                                return View("Index");
                            }

                            string path = Path.Combine(Server.MapPath("~/Content/Files"), Path.GetFileName(fileName.FileName));
                            fileName.SaveAs(path);
                            dbfile.path = path;
                            dbfile.fileName = fileName.FileName;
                            dbfile.fileType = fileName.ContentType;
                            dbfile.Course = db.Course.Find(Session["courseId"]);
                            dbfile.Teacher = db.Teachers.Find(Session["userId"]);
                            // dbfile.seriesId = Convert.ToInt32(data["seriesId"]);


                            db.FileDetails.Add(dbfile);
                            db.SaveChanges();
                            ViewBag.FileStatus = "File uploaded successfully.";
                        }
                        else
                        {
                            ViewBag.FileStatus = "Can only upload Pdfs and Word files.";
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex);
                    }
                }
                return View("Index");
            }
            else
            {
                return RedirectToAction("Errorpage", "Courses");
            }
        }
        public ActionResult ViewVideo(int id)
        {
            if (Session["userType"] != null)
            {
                if (Session["userType"].Equals("Teacher"))
                {
                    List<FileDetails> flist = db.FileDetails.ToList();
                    List<FileDetails> tlist = new List<FileDetails>();
                    foreach (FileDetails f in flist)
                    {
                        if (f.Teacher.tId == (int)Session["userId"] && f.Course.courseId == id)
                        {
                            tlist.Add(f);
                        }
                    }
                    return View(tlist);
                }
                else
                {
                    List<FileDetails> vlist = db.FileDetails.ToList();
                    List<FileDetails> tlist = new List<FileDetails>();
                    foreach (FileDetails v in vlist)
                    {
                        if (v.Course.courseId == id)
                        {
                            tlist.Add(v);
                        }
                    }
                    return View(tlist);

                }
            }
            else
            {
                return RedirectToAction("Errorpage", "Courses");
            }

        }

        public ActionResult Media(int id)
        {
            if (Session["userType"] != null)
            {
                var w = db.FileDetails.Find(id);
                //Response.Write(w.path.Substring(61));
                // s = x.path.Substring(61);
                string fn = Server.MapPath("~/Content/Files/" + w.path.Substring(66));

                //I tried to load a local video and convert to stream, you need to replace the code to get the video from your data base.
                // string fn = Server.MapPath("~/Content/Files/small.mp4");
                var memoryStream = new MemoryStream(System.IO.File.ReadAllBytes(fn));
                return new FileStreamResult(memoryStream, MimeMapping.GetMimeMapping(System.IO.Path.GetFileName(fn)));
            }
            else
            {
                return RedirectToAction("Errorpage", "Courses");
            }
        }

        public FileResult Download(int id)
        {
            
                var w = db.FileDetails.Find(id);
                //Response.Write(w.path.Substring(61));
                // s = x.path.Substring(61);
                string fn = Server.MapPath("~/Content/Files/" + w.path.Substring(66));
                return File(fn, w.fileType, w.fileName);
            
            
        }

        public ActionResult Delete(FileDetails x)
        {
            if (Session["userType"] != null)
            {
                var w = db.FileDetails.Find(x.fileId);
                //Response.Write(w.path.Substring(61));
                // s = x.path.Substring(61);
                string fn = Server.MapPath("~/Content/Files/" + w.path.Substring(66));
                System.IO.File.Delete(fn);
                db.FileDetails.Remove(w);
                db.SaveChanges();
                return RedirectToAction("ViewVideo", "FileUpload", new { @id = Session["courseId"] }); //View("ViewVideo");
            }
            else
            {
                return RedirectToAction("Errorpage", "Courses");
            }
        }
    }

}
