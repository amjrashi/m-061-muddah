using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Moddah.Models;
using System.IO;

namespace Moddah.Controllers
{
    public class ImagesController : Controller
    {
        private Moddah_DBEntities db = new Moddah_DBEntities();

        // GET: Images
        public ActionResult Index()
        {
            return View(db.Images.ToList());
        }

        // GET: Images/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageID,Path,PlaceID,Files")] Image image)
        {
            //===================================================================
            //Images
            string day = DateTime.Now.Day.ToString();
            string month = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString();
            string seconds = DateTime.Now.Second.ToString();
            string msecond = DateTime.Now.Millisecond.ToString();

            string unq = day + month + year + seconds + msecond;

            //var fname = unq + Path.GetFileName(image.File.FileName);
            string filesnames = "";
            int countfiles = 0;
            foreach (var item in image.Files)
            {
                if (item==null)
                {
                    countfiles = 0;
                }
                else
                {
                    countfiles++;
                }
            }
            if (countfiles>0)
            {
                try
                {
                    foreach (var item in image.Files)
                    {
                        filesnames += unq + Path.GetFileName(item.FileName) + ":";
                    }
                }
                catch { }

            }
            else
            {
                return RedirectToAction("View_msg", new { m = "Please attach one file - يجب رفع مرفق", cont = "Images", col = "Red", w = "Create" });

            }

            //fname += filesnames;

            //var path = Path.Combine(Server.MapPath("~/NewsAttach"), fname);
            // string extension = Path.GetExtension(fname);
            //image/jpg,image/png,image/jpeg,image/gif
            //if (extension.ToLower().Trim() != ".jpg" && extension != ".png" && extension != ".jpeg" && extension != ".gif" && extension != ".BMP" && extension != ".ico")
            //{
            //    return RedirectToAction("View_msg", new { m = "Images only are accepted - مسموح برفع الصور فقط", cont = "NewsMasters", col = "Red", w = "Create" });

            //}
            //else
            //{
            string[] arrnames = filesnames.Split(':');
                if (image.Files != null)
                {
                    try
                    {
                        foreach (var y in image.Files)
                        {
                            var fnm = unq + Path.GetFileName(y.FileName);
                            var ppath = Path.Combine(Server.MapPath("~/NewsAttach"), fnm);
                            try { y.SaveAs(ppath); } catch { }
                        }
                    }
                    catch { }
                }
            //}
            //===================================================================

            image.Path = filesnames;
                if (ModelState.IsValid)
            {
                db.Images.Add(image);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return View(image);
        }

        public ActionResult View_msg()
        {
            return View();
        }

        // GET: Images/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageID,Path,PlaceID")] Image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(image);
        }

        // GET: Images/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");
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
