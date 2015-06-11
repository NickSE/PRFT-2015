using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Model;
using WebApplication1.EventManagement;

namespace WebApplication1.DB
{
    public class EMDatabase : Database
    {
        public List<Events> GetAllEvents()
        {
            List<Events> ret = new List<Events>();
            List<Dictionary<string, object>> AllEvents = getQuery("SELECT * FROM event");
            foreach (Dictionary<string, object> events in AllEvents)
            {
                ret.Add(new Events(Convert.ToInt32(events["id"]), (string)events["naam"], (DateTime)events["datumStart"], (DateTime)events["datumEinde"], Convert.ToInt32(events["maxBezoekers"]), Convert.ToInt32(events["locatie_id"])));
            }

            return ret;
        }

        public List<Locatie> GetAllLocations()
        {
            List<Locatie> ret = new List<Locatie>();
            List<Dictionary<string, object>> AllLocations = getQuery("SELECT * FROM locatie");
            foreach (Dictionary<string, object> locatie in AllLocations)
            {
                ret.Add(new Locatie(Convert.ToInt32(locatie["id"]),(string)locatie["naam"], (string)locatie["straat"], (string)locatie["nr"], (string)locatie["postcode"], (string)locatie["plaats"]));
            }

            return ret;
        }
        public List<Plek> GetAllPleks()
        {
            List<Plek> ret = new List<Plek>();
            List<Dictionary<string, object>> AllPleks = getQuery("SELECT * FROM plek");
            foreach (Dictionary<string, object> plek in AllPleks)
            {
                ret.Add(new Plek(Convert.ToInt32(plek["id"]),(string)plek["nummer"],Convert.ToInt32(plek["capaciteit"]), Convert.ToInt32(plek["locatie_id"])));
            }

            return ret;
        }
        public bool AddLocation(Locatie locatie)
        {
            try
            {
                string query;
                query = "INSERT INTO locatie(id, naam, straat, nr, postcode, plaats) VALUES(";
                query += locatie.id + ", " + locatie.name + ", " + locatie.straat + ", " + locatie.nr + ", " + locatie.postcode + ", " + locatie.plaats + ")";
                doQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddEvent(Events events)
        {
            try
            {
                string query;
                query = "INSERT INTO event(id, locatie_id, naam, datumstart, datumeind, maxbezoekers ) VALUES(";
                query += events.id + ", " + events.locatie_id + ", "+ events.name +  ", " + events.dateStart + ", "+ events.dateEnd + ", " + events.maxCapacity  +")";
                doQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddPlek(Plek plek)
        {
            try
            {
                string query;
                query = "INSERT INTO plek(id, id, locatie_id, nummer, capacity) VALUES(";
                query += plek.id + ", " + plek.nummer + ", " + plek.capacity + ", " + plek.locatie_id + ")";
                doQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddSpecification(string spec,string waarde, int Plek_id )
        {
            try
            {
                doQuery("INSERT INTO PLEK_SPECIFICATIE(ID, specificatie_id, plek_id, waarde)VALUES (" + getLatestId("plek_specificatie") + ", " + spec + ", " + Plek_id + "," + waarde + ")");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool deleteLocation(int locatie)
        {
            try
            {
                doQuery("DELETE FROM locatie WHERE id = " + locatie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deleteplek(int plek)
        {
            try
            {
                doQuery("DELETE FROM plek WHERE id = " + plek);
                doQuery("DELETE FROM plek_specificatie WHERE id = " + plek);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool deleteEvent(int events)
        {
            try
            {
                doQuery("DELETE FROM event WHERE id = " + events);
                return true;
            }
            catch
            {
                return false;
            }

        }
       
    }
}