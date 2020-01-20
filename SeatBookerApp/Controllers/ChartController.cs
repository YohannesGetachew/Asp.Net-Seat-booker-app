using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeatBookerApp.Models;

namespace SeatBookerApp.Controllers
{
    public class ChartController : Controller
    {
        GrabASeatEntities es = new GrabASeatEntities();
        string user;


        public ActionResult Index()
        {

            return View();
        }

        public ActionResult UserIndex() {

            return View();
        }

        public ActionResult SeatingChart() {

            return View( es.Charts.ToList() );

        }

        [HttpGet]
        public ActionResult SeatingForm()
        {

            return View();

        }

        public ActionResult MyEvents(All a) {
            a = new All
            {

                event1 = new Event(),
                chart1 = new Chart(),

                seating2 = es.Seatings.ToList(),
                chart2 = es.Charts.ToList(),
                login2 = es.Login_Table.ToList(),

            };

            return View(a);
        }

        
        public ActionResult MakeSeat(int index) {

            ViewData["index"]=index;
            
            return View(es.Charts.ToList() );
        }

        public ActionResult MakeEventSeat(All a, int i, string Name, string userNameCurr)
        {

            ViewData["i"] = i;
            ViewData["Name"] = Name;

            if (userNameCurr != null)
            {
                List<Seating> seatList = es.Seatings.ToList();
                Seating found = seatList.Find(c => c.User_Name == userNameCurr && c.Event_Name == Name);

                es.Seatings.Remove(found);
                es.SaveChanges();
            }
            a = new All
            {
                name = user,
                event1 = new Event(),
                chart1 = new Chart(),

                seating2 = es.Seatings.ToList(),
                chart2 = es.Charts.ToList(),
                login2 = es.Login_Table.ToList()
            };

            return View(a);
        }



        [HttpPost]
        public ActionResult SeatingForm(Chart chart)
        {

            if (ModelState.IsValid) {
                
                es.Charts.Add(chart);
                es.SaveChanges();
                
            }
            return Redirect("SeatingChart");

        }

      
        

        [HttpGet]
        public ActionResult PostEvents(ChartEvent a) {

            a = new ChartEvent
            {
                Event1 = new Event(),
                Chart1 = es.Charts.ToList()
            };
            
            return View( a  );
        }

        [HttpPost]
        public ActionResult PostEvents(ChartEvent ab, HttpPostedFileBase file2)
        {
            SeatBookerApp.Event ev = ab.Event1;
          

            if (file2 != null) {
                string imageName = System.IO.Path.GetFileName(file2.FileName);
                string physicalPath = Server.MapPath("~/Content/img/" + imageName);
                file2.SaveAs(physicalPath);
                ev.Image = "Content/img/" + imageName;
            }

            string selected = Request.Form["ChartList"];
            List<Chart> a = es.Charts.ToList();
            Chart found = a.Find(c => c.Chart_ID == selected );



            ev.Row = found.Seat_Row;
            ev.Col = found.Seat_Column;
            ev.Row_Div = found.Row_Div_No;
            ev.Col_Div = found.Col_Div_No;
            ev.Chart_Title = found.Chart_ID;

            if (ModelState.IsValid) {
                es.Events.Add(ev);
                es.SaveChanges();
            }

            return Redirect("Index");
        }



        public ActionResult AvailableEvents() {

            return View(es.Events.ToList());
        }

        ///////////////////////////////////////////////////////////////////// Participator     ///////////////////////////////////////////////////////


        public ActionResult BookEvents() {

            return View(es.Events.ToList());
        }

        public ActionResult ShowSeats(All a,int i,string Name ,string aid,  string existance ) {

            ViewData["i"] = i;
            ViewData["Name"] = Name;

            List<Seating> seatList = es.Seatings.ToList();
            Seating found = seatList.Find(c => c.User_Name == (string) Session["Name"]   );
            Seating found2 = seatList.Find(c => c.User_Name == (string)Session["Name"] && c.Hold_Places==aid);

            if (existance == "No_one") {

                Seating newSeating = new Seating();

                newSeating.Event_Name = Name;
                newSeating.Hold_Places = aid;
                newSeating.User_Name = (string)Session["Name"];

                es.Seatings.Add(newSeating);
                es.SaveChanges();
            }

            else if (existance == "No_one+Booked")
            {
                found.Hold_Places = aid;

                es.SaveChanges();

            }

            else if (existance == "userExists") {

                es.Seatings.Remove(found2);

                es.SaveChanges();

            }

            

            a = new All
            {
                name = user,
                event1 = new Event(),
                chart1 = new Chart(),

                seating2 = es.Seatings.ToList(),
                chart2 = es.Charts.ToList(),
                login2 = es.Login_Table.ToList()
            };
            



            return View(a);
        }
        [HttpPost]
        public ActionResult ShowSeats(All a, int i,int k)
        {

            ViewData["i"] = i;
            

            a = new All
            {
               
                event1 = new Event(),
                chart1 = new Chart(),

                seating2 = es.Seatings.ToList(),
                chart2 = es.Charts.ToList(),
                login2 = es.Login_Table.ToList(),
               
            };




            return View(a);
        }




        //  ////////////////////////////////////////          Log-in And Register Block    /////////////////////////////////////

        public ActionResult RegisterForm() {

            return View();
        }

        [HttpPost]
        public ActionResult RegisterForm(Login_Table lt)
        {
            es.Login_Table.Add(lt);
            es.SaveChanges();

            return View();
        }

        public ActionResult FrontPage() {


            return View();
        }




        public ActionResult Login() {

            Login_Table lt = new Login_Table();
            return View( lt);
        }

        [HttpPost]
        public ActionResult Login(Login_Table lt,Seating s)
        {
            List<Login_Table> a = es.Login_Table.ToList();
            Login_Table found = a.Find(c => c.User_Name == lt.User_Name && c.Password == lt.Password && c.Role==lt.Role);

         

            if (found != null && lt.Role=="Event-Handler") {


                Session["Adminname"] = lt.User_Name;
                return Redirect("Index");

               
            }
            if (found != null && lt.Role == "Participator")
            {
                user = lt.User_Name;
                Session["name"] = lt.User_Name;
                return Redirect("UserIndex");
               
            }

            return View();
        }

        
        /////////////////////////////////////////////             end of register and login ////////////////////////////////////

    }
}