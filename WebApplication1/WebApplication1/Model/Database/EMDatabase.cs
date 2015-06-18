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
            List<Dictionary<string, object>> AllEvents = getQuery("SELECT * FROM event ORDER BY id");
            foreach (Dictionary<string, object> events in AllEvents)
            {
                ret.Add(new Events(Convert.ToInt32(events["id"]), (string)events["naam"], (DateTime)events["datumstart"], (DateTime)events["datumeinde"], Convert.ToInt32(events["maxbezoekers"]), Convert.ToInt32(events["locatie_id"])));
            }

            return ret;
        }

        public List<Locatie> GetAllLocations()
        {
            List<Locatie> ret = new List<Locatie>();
            List<Dictionary<string, object>> AllLocations = getQuery("SELECT * FROM locatie ORDER BY id");
            foreach (Dictionary<string, object> locatie in AllLocations)
            {
                ret.Add(new Locatie(Convert.ToInt32(locatie["id"]),Convert.ToString(locatie["naam"]), Convert.ToString(locatie["straat"]), Convert.ToString(locatie["nr"]), Convert.ToString(locatie["postcode"]), Convert.ToString(locatie["plaats"])));
            }

            return ret;
        }
        public List<Plek> GetAllPleks()
        {
            List<Plek> ret = new List<Plek>();
            List<Dictionary<string, object>> AllPleks = getQuery("SELECT * FROM plek ORDER BY id");
            foreach (Dictionary<string, object> plek in AllPleks)
            {
                ret.Add(new Plek(Convert.ToInt32(plek["id"]), (string)plek["nummer"], Convert.ToInt32(plek["capaciteit"]), Convert.ToInt32(plek["locatie_id"])));
            }

            return ret;
        }
        public List<Dictionary<string, object>> GetAllSpecs()
        {
            List<Dictionary<string, object>> AllSpecs = getQuery("SELECT * FROM specificatie ORDER BY id");

            return AllSpecs;
        }
        public bool AddLocation(Locatie locatie)
        {
            try
            {
                string query;
                query = "INSERT INTO LOCATIE VALUES(";
                query += locatie.id + ", '" + locatie.name + "', '" + locatie.straat + "', '" + locatie.nr + "', '" + locatie.postcode + "', '" + locatie.plaats + "')";
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
                query = "INSERT INTO EVENT VALUES(";
                query += events.id + ", '" + events.locatie_id + "', '" + events.name + "', to_date('" + events.dateStart.ToString("MM-dd-yyyy hh:mm") + "','MM-DD-YYYY hh24:MI'), to_date('" + events.dateEnd.ToString("MM-dd-yyyy hh:mm") + "','MM-DD-YYYY hh24:MI'), '" + events.maxCapacity + "')";
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
                query = "INSERT INTO PLEK VALUES(";
                query += plek.id + ", '" + plek.locatie_id + "', '" + plek.nummer + "', '" + plek.capacity + "')";
                doQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddSpecification(string spec,string waarde, int plek_id )
        {
            try
            {
                doQuery("INSERT INTO PLEK_SPECIFICATIE VALUES (" + getLatestId("plek_specificatie") + ", '" + spec + "', " + plek_id + ", '" + waarde + "')");
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