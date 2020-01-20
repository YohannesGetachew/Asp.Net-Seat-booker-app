using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatBookerApp.Models
{
    public class All
    {

        public Event event1 { get; set; }
        public Chart chart1 { get; set; }
        public Event login1 { get; set; }
        public Seating seating1 { get; set; }
        public List<SeatBookerApp.Seating> seating2 { get; set; }
        public IEnumerable<SeatBookerApp.Chart> chart2 { get; set; }
        public IEnumerable<SeatBookerApp.Login_Table> login2 { get; set; }
        public IEnumerable<SeatBookerApp.Event> event2 { get; set; }
 
        public int i;
        public string name;

    }
}