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
    public class PlaceTypesController : Controller
    {
        private Moddah_DBEntities db = new Moddah_DBEntities();

        // GET: PlaceTypes
        public ActionResult Index()
        {
            return View(db.PlaceTypes.ToList());
        }

        // GET: PlaceTypes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceType placeType = db.PlaceTypes.Find(id);
            if (placeType == null)
            {
                return HttpNotFound();
            }
            return View(placeType);
        }

        // GET: PlaceTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlaceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaceTypeID,Name")] PlaceType placeType)
        {
            if (ModelState.IsValid)
            {
                db.PlaceTypes.Add(placeType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(placeType);
        }

        // GET: PlaceTypes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceType placeType = db.PlaceTypes.Find(id);
            if (placeType == null)
            {
                return HttpNotFound();
            }
            return View(placeType);
        }

        // POST: PlaceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlaceTypeID,Name")] PlaceType placeType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(placeType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(placeType);
        }

        // GET: PlaceTypes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceType placeType = db.PlaceTypes.Find(id);
            if (placeType == null)
            {
                return HttpNotFound();
            }
            return View(placeType);
        }

        // POST: PlaceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PlaceType placeType = db.PlaceTypes.Find(id);
            db.PlaceTypes.Remove(placeType);
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
