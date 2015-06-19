using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.UserManagement
{
    public class Reservation
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public string payed { get; set; }

        public Reservation(int id, int user_id, DateTime startdate, DateTime enddate, string payed)
        {
            this.id = id;
            this.user_id = id;
            this.startdate = startdate;
            this.enddate = enddate;
            this.payed = payed;
        }
    }
}