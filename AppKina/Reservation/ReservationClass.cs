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
        public int userID { get; set; }
        public string title { get; set; }
        public int projectionID { get; set; }
        public DateTime dateTime { get; set; }
        public string seats { get; set; }

        public ReservationClass() { }
        public ReservationClass(int userID, string title, int projectionID, DateTime dateTime, string seats) 
        { 
            this.userID = userID;
            this.title = title;
            this.projectionID = projectionID;
            this.dateTime = dateTime;
            this.seats = seats;
        }
    }
}
