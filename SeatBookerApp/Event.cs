//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SeatBookerApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public int ID { get; set; }
        public string Event_Name { get; set; }
        public string Description { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public Nullable<int> Row { get; set; }
        public Nullable<int> Col { get; set; }
        public string Row_Div { get; set; }
        public string Col_Div { get; set; }
        public string Image { get; set; }
        public string Chart_Title { get; set; }
    }
}
