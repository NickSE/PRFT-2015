using System;
using Oracle.DataAccess.Client;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Events_Group1
{
    class EventDatabase : Database
    {
        internal Employee Employee
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        internal Event Event
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        public List<Employee> getEmployees()
        {
            List<Employee> ret = new List<Employee>();

            List<Dictionary<string, object>> data = getQuery("SELECT Id, Username, Password, Adminrights FROM Employee");

            if (data == null)
                return ret;

            for(int c = 0; c < data.Count; c++)
                ret.Add(new Employee(Convert.ToInt16(data[c]["id"]), (string)data[c]["username"], (string)data[c]["password"], Convert.ToInt16(data[c]["adminrights"]) == 1));

            return ret;
        }

        public List<Employee> getEmployees(string filter)
        {
            List<Employee> ret = new List<Employee>();

            List<Dictionary<string, object>> data = getQuery("SELECT Id, Username, Password, Adminrights FROM Employee WHERE Username LIKE '%" + filter + "%' OR Id LIKE '%" + filter + "%'");

            if (data == null)
                return ret;

            for (int c = 0; c < data.Count; c++)
                ret.Add(new Employee(Convert.ToInt16(data[c]["id"]), (string)data[c]["username"], (string)data[c]["password"], Convert.ToInt16(data[c]["adminrights"]) == 1));

            return ret;
        }

        public bool deleteEmployee(Employee e)
        {
            int dox = doQuery("DELETE FROM Employee WHERE Id = " + e.id);
            return dox > 0;
        }

        public bool createEmployee(Employee e)
        {
            string query = "INSERT INTO Employee (Id, Username, Password) VALUES (" + e.id + ", '" + e.username + "', '" + e.password + "')";

            return doQuery(query) > 0;
        }


        public List<Event> getEvents()
        {
            List<Event> ret = new List<Event>();

            List<Dictionary<string, object>> data = getQuery("SELECT Id, Naam, Startdatum, Einddatum, Beschrijving, Kosten FROM Event");

            if (data == null)
                return ret;

            for (int c = 0; c < data.Count; c++)
                ret.Add(new Event(Convert.ToInt16(data[c]["id"]), (string)data[c]["naam"], (string)data[c]["beschrijving"], (DateTime)data[c]["startdatum"], (DateTime)data[c]["einddatum"], (float) data[c]["kosten"]));
            
            return ret;
        }

        public List<Event> getEvents(string filter)
        {
            List<Event> ret = new List<Event>();

            string query = "SELECT Id, Naam, Startdatum, Einddatum, Beschrijving, Kosten FROM Event WHERE Naam LIKE '%" + filter + "%' OR Beschrijving LIKE '%" + filter + "%'";

            List<Dictionary<string, object>> data = getQuery(query);

            if (data == null)
                return ret;

            for (int c = 0; c < data.Count; c++)
                ret.Add(new Event(Convert.ToInt16(data[c]["id"]), (string)data[c]["naam"], (string)data[c]["beschrijving"], (DateTime)data[c]["startdatum"], (DateTime)data[c]["einddatum"], (float)data[c]["kosten"]));

            return ret;
        }

        public bool deleteEvent(Event e)
        {
            int dox= doQuery("DELETE FROM Event WHERE Id = " + e.id);
            return dox > 0;
        }

        public bool createEvent(Event e)
        {
            string query = "INSERT INTO Event (Id, Naam, Startdatum, Einddatum, Beschrijving, Kosten) VALUES (" + e.id + ", '" + e.Name + "', to_date('" + e.startDate.ToString("MM-dd-yyyy hh:mm") + "','MM-DD-YYYY hh24:MI'),  to_date('" + e.endDate.ToString("MM-dd-yyyy hh:mm") + "','MM-DD-YYYY hh24:MI'), '"+e.Desc+"', " + e.price + ")";

            return doQuery(query) > 0;
        }
    }
}
