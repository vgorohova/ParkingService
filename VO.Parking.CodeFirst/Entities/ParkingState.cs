//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VO.Parking.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ParkingState
    {
        public int AvailableParkingLots { get; set; }
        public int TotalParkingLots { get; set; }
        public int TotalCarsParkedCount { get; set; }
        public Nullable<System.DateTime> LastCarEntryDate { get; set; }
        public Nullable<System.DateTime> LastCarLeaveDate { get; set; }
    }
}
