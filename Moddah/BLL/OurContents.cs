using Moddah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moddah.BLL
{
    public class OurContents
    {
        public List<Host> hostList{ get; set; }

        public List<City> cityList { get; set; }

        public List<PlaceType> placetypeList { get; set; }

        public List<Place> placeList { get; set; }
    }
}