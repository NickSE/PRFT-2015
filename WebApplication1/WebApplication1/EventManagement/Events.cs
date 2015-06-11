using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.EventManagement
{
    public class Events
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public int maxCapacity { get; set; }
        public int locatie_id { get; set; }

        public Events(int id, string name, DateTime dateStart, DateTime dateEnd, int maxCapacity, int locatie_id)
        {
            this.id = id;
            this.name = name;
            this.dateStart = dateStart;
            this.dateEnd = dateEnd;
            this.maxCapacity = maxCapacity;
            this.locatie_id = locatie_id;
        }
        public override string ToString()
        {
            return (id + " - " + name);
        }

    }
}