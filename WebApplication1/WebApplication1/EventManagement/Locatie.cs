using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.EventManagement
{
    public class Locatie
    {
        public int id { get; set; }
        public string name { get; set; }
        public string straat { get; set; }
        public string nr { get; set; }
        public string postcode { get; set; }
        public string plaats { get; set; }

        public Locatie(int id, string name, string straat, string nr, string postcode, string plaats)
        {
            this.id = id;
            this.name = name;
            this.straat = straat;
            this.nr = nr;
            this.postcode = postcode;
            this.plaats = plaats;
        }
        public override string ToString()
        {
            return (id + " - " + name);
        }
    }
    
}