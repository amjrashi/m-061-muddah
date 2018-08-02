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
    public class HostsController : Controller
    {
        private Moddah_DBEntities db = new Moddah_DBEntities();

        // GET: Hosts
        public ActionResult Index()
        {
            return View(db.Hosts.ToList());
        }

        // GET: Hosts/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Host host = db.Hosts.Find(id);
            if (host == null)
            {
                return HttpNotFound();
            }
            return View(host);
        }

        // GET: Hosts/Create
        public ActionResult Create()
        {
            List<Nationality> CatList = db.Nationalities.ToList();

            ViewBag.CatList = new SelectList(CatList, "NationalityID", "Name");
            return View();
        }

        // POST: Hosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HostID,HostName,Password,Enabled,Email,Phone,SSN,NationalityID,PhotoURL,RegistrationDate,Languages,Gender,File")] Host host)
        {
            //Default he is disabled until confirmation
            host.Enabled = false;

            List<Nationality> CatList = db.Nationalities.ToList();

            ViewBag.CatList = new SelectList(CatList, "NationalityID", "Name");
            if (ModelState.IsValid)
            {
                if (host.File != null)
                {
                    string day = DateTime.Now.Day.ToString();
                    string month = DateTime.Now.Month.ToString();
                    string year = DateTime.Now.Year.ToString();
                    string seconds = DateTime.Now.Second.ToString();
                    string msecond = DateTime.Now.Millisecond.ToString();
                    string unq = day + month + year + seconds + msecond;
                    var fname = unq + Path.GetFileName(host.File.FileName);
                    var path = Path.Combine(Server.MapPath("~/ProfilePictures"), fname);
                    host.File.SaveAs(path);
                    host.PhotoURL = fname;

                    db.Hosts.Add(host);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("View_msg", new { m = "Please attach one file - يجب رفع مرفق", cont = "Hosts", col = "Red", w = "Create" });

                }

            }

            return View(host);
        }
        public ActionResult View_msg()
        {
            return View();
        }
        // GET: Hosts/Edit/5
        public ActionResult Edit(long? id)
        {
            List<Nationality> CatList = db.Nationalities.ToList();

            ViewBag.CatList = new SelectList(CatList, "NationalityID", "Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Host host = db.Hosts.Find(id);
            if (host == null)
            {
                return HttpNotFound();
            }
            return View(host);
        }

        // POST: Hosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HostID,HostName,Password,Enabled,Email,Phone,SSN,NationalityID,PhotoURL,RegistrationDate,Languages,Gender")] Host host)
        {
            List<Nationality> CatList = db.Nationalities.ToList();

            ViewBag.CatList = new SelectList(CatList, "NationalityID", "Name");
            if (ModelState.IsValid)
            {
                db.Entry(host).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(host);
        }

        // GET: Hosts/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Host host = db.Hosts.Find(id);
            if (host == null)
            {
                return HttpNotFound();
            }
            return View(host);
        }

        // POST: Hosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Host host = db.Hosts.Find(id);
            db.Hosts.Remove(host);
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
