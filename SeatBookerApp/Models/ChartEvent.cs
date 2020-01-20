using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatBookerApp.Models
{
    public class ChartEvent
    {

        public Event Event1 { get; set; }
        public IEnumerable<SeatBookerApp.Chart> Chart1 { get; set; }
    }
}