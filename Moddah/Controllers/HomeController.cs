using Moddah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moddah.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        private Moddah_DBEntities db = new Moddah_DBEntities();
        public ActionResult Index(string option, string search,string city,string placetype)
        {
            var lis = db.Places.ToList();
            List<City> CityList = db.Cities.ToList();
            List<PlaceType> CatList = db.PlaceTypes.ToList();

            ViewBag.CatList = new SelectList(CatList, "PlaceTypeID", "Name");
            ViewBag.CityList = new SelectList(CityList, "CityID", "Name");

            long placetypeid = 0;
            try { placetypeid = long.Parse(placetype); } catch { }
            long cityy = 0;
            try { cityy = long.Parse(city);}catch{ }

                return View(db.Places.Where(p => p.CityID==cityy &&p.PlaceTypeID== placetypeid).ToList());
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}