using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.EventManagement
{
    public class Plek
    {
        public int id { get; set; }
        public string nummer { get; set; }
        public int capacity { get; set; }
        public int locatie_id { get; set; }

        public Plek(int id, string nummer, int capacity, int locatie_id)
        {
            this.id = id;
            this.nummer = nummer;
            this.capacity = capacity;
            this.locatie_id = locatie_id;
        }

        public override string ToString()
        {
            return (id + " - " + nummer);
        }
    }
}