using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Moddah.Models;

namespace Moddah.Controllers
{
    public class InboxesController : Controller
    {
        private Moddah_DBEntities db = new Moddah_DBEntities();

        // GET: Inboxes
        public ActionResult Index()
        {
            return View(db.Inboxes.ToList());
        }

        // GET: Inboxes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inbox inbox = db.Inboxes.Find(id);
            if (inbox == null)
            {
                return HttpNotFound();
            }
            return View(inbox);
        }

        // GET: Inboxes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inboxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InboxID,DateofMessage,Subject,BodyofMessage")] Inbox inbox)
        {
            inbox.FromUserID = (long)Session["UserID"];
            inbox.ToUserID = 1;
            if (ModelState.IsValid)
            {
                db.Inboxes.Add(inbox);
                db.SaveChanges();
                return RedirectToAction("Viewmsg");
            }

            return RedirectToAction("Viewmsg");
        }

        public ActionResult Viewmsg()
        {
            return View();
        }
        // GET: Inboxes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inbox inbox = db.Inboxes.Find(id);
            if (inbox == null)
            {
                return HttpNotFound();
            }
            return View(inbox);
        }

        // POST: Inboxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InboxID,FromUserID,ToUserID,DateofMessage,Subject,BodyofMessage")] Inbox inbox)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inbox).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inbox);
        }

        // GET: Inboxes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inbox inbox = db.Inboxes.Find(id);
            if (inbox == null)
            {
                return HttpNotFound();
            }
            return View(inbox);
        }

        // POST: Inboxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Inbox inbox = db.Inboxes.Find(id);
            db.Inboxes.Remove(inbox);
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
