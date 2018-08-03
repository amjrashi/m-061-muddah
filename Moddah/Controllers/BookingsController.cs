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
    public class BookingsController : Controller
    {
        private Moddah_DBEntities db = new Moddah_DBEntities();

        // GET: Bookings
        public ActionResult Index()
        {
            return View(db.Bookings.ToList());
        }
        public ActionResult Index2(long id)
        {
            return View(db.Bookings.ToList().Where(p=>p.GestID==id));
        }
        // GET: Bookings/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingID,GestID,PlaceID,TimeFrom,TimeTo")] Booking booking)
        {
            long userID = (long)Session["UserID"];
            booking.GestID = userID;
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
               // return RedirectToAction("Index");
            }

            long hostID = (long)db.Places.FirstOrDefault(p => p.PlaceID == booking.PlaceID).HostID;
            string GuestName = db.Guests.FirstOrDefault(p => p.GuestID == booking.GestID).Name;
            Place plc = db.Places.FirstOrDefault(p => p.PlaceID == booking.PlaceID);
            string city = db.Cities.FirstOrDefault(p => p.CityID == plc.CityID).Name;
            string mob1 = db.Guests.FirstOrDefault(p => p.GuestID == booking.GestID).Phone;
            string mob2= db.Hosts.FirstOrDefault(p => p.HostID == hostID).Phone;
            Inbox inbox = new Inbox()
            {
                Subject = "طلب حجز جديد من العميل / " + GuestName,
                DateofMessage = DateTime.Now,
                FromUserID = long.Parse(mob1),
                ToUserID = hostID,
                BodyofMessage = "طلب حجز جديد من العميل / " + GuestName + Environment.NewLine +
                "من تاريخ" + booking.TimeFrom + "الى تاريخ" + booking.TimeTo + Environment.NewLine +
                "للغرفة الموجوده بمدينة  " + city + " المكان " + plc.Location
            };
            db.Inboxes.Add(inbox);
            db.SaveChanges();

            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,GestID,PlaceID,TimeFrom,TimeTo")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
