using AppKina.MainPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppKina.Reservation
{
    public class ReservationClass
    {
        public int id { get; set; }
        public string title { get; set; }
        public int projectionID { get; set; }
        public string dateTime { get; set; }
        public string seats { get; set; }

        public ReservationClass() { }
        public ReservationClass(int id, string title, int projectionID, string dateTime, string seats) 
        {
            this.id = id;
            this.title = title;
            this.projectionID = projectionID;
            this.dateTime = dateTime;
            this.seats = seats;
        }
    }
}
