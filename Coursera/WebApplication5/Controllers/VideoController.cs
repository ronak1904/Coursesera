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
    public class VideoController : Controller
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
                return RedirectToAction("Errorpage", "Courses");
            }
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase videoName, FormCollection data, Video dbfile)
        {
            if (Session["userType"] != null)
            {
                if (ModelState.IsValid)
            {
                try
                {
                    if (videoName != null) //(videoName.ContentType == "video/mp4" || videoName.ContentType == "video/mkv")

					{
                        List<Video> lst = db.Videos.Where(f => f.videoName == videoName.FileName).ToList();
                        if (lst.Any())
                        {
                            ViewBag.ermsg = "The Video With the same name already exists please change the name and try again";
                            return View("Index");
                        }
                        string path = Path.Combine(Server.MapPath("~/Content/Videoes"), Path.GetFileName(videoName.FileName));
                        videoName.SaveAs(path);
                        dbfile.path = path;
                        dbfile.videoName = videoName.FileName;
                        dbfile.videoType = videoName.ContentType;
                        dbfile.Course = db.Course.Find(Session["courseId"]);
                        dbfile.Teacher = db.Teachers.Find(Session["userId"]);
                        // dbfile.seriesId = Convert.ToInt32(data["seriesId"]);


                        db.Videos.Add(dbfile);
                        db.SaveChanges();
                        ViewBag.FileStatus = "File uploaded successfully.";
                    }
                    else
                    {
                        ViewBag.FileStatus = "Can only upload Videos";
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
                    Session["courseId"] = id;
            if (Session["userType"].Equals("Teacher"))
            {
                List<Video> vlist = db.Videos.ToList();
                List<Video> tlist=new List<Video>();
                foreach (Video v in vlist)
                {
                    if (v.Teacher.tId == (int)Session["userId"] && v.Course.courseId==id)
                    {
                        tlist.Add(v);
                    }
                }
                return View(tlist);
            }
            else
            {

                List<Video> vlist = db.Videos.ToList();
                List<Video> tlist = new List<Video>();
                foreach (Video v in vlist)
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
                        var w = db.Videos.Find(id);
                        //Response.Write(w.path.Substring(61));
                        // s = x.path.Substring(61);
                        string fn = Server.MapPath("~/Content/Videoes/" + w.path.Substring(68));


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
                        
                            var w = db.Videos.Find(id);
                            //Response.Write(w.path.Substring(61));
                            // s = x.path.Substring(61);
                            string fn = Server.MapPath("~/Content/Videoes/" + w.path.Substring(68));
                            return File(fn, w.videoType, w.videoName);
                        }

                        public ActionResult Delete(Video x)
                        {
                            if (Session["userType"] != null)
                            {
                                var w = db.Videos.Find(x.videoId);
                                //Response.Write(w.path.Substring(61));
                                // s = x.path.Substring(61);
                                string fn = Server.MapPath("~/Content/Videoes/" + w.path.Substring(68));
                                System.IO.File.Delete(fn);
                                db.Videos.Remove(w);
                                db.SaveChanges();

                                return RedirectToAction("ViewVideo", "Video", new { @id = Session["courseId"] }); //View("ViewVideo");
                            }

                            else
                            {
                                return RedirectToAction("Errorpage", "Courses");
                            }
                        }

                    }
                }
