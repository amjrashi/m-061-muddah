//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Moddah.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Place
    {
        public long PlaceID { get; set; }
        public Nullable<long> HostID { get; set; }
        public Nullable<long> PlaceTypeID { get; set; }
        public string Location { get; set; }
        public string NightPrice { get; set; }
        public string Detailes { get; set; }
        public Nullable<bool> IsTure { get; set; }
        public Nullable<long> CityID { get; set; }
    }
}