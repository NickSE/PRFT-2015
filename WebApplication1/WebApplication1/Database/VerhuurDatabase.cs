using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Events_Group1
{
    class VerhuurDatabase : Database
    {
        internal Item Item
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        public enum Soort { Alles, Huurbaar, Verhuurd };

        public Dictionary<string, int> getCatAantal(Soort soort)
        {
            Dictionary<string, int> ret = new Dictionary<string, int>();

            string query;

            switch (soort)
            {
                case Soort.Alles:
                    query = "SELECT * FROM (SELECT TYPE as CATEGORIE, COUNT(CODE) aantal FROM ITEM GROUP BY TYPE UNION ALL SELECT 'Spot' as CATEGORIE, COUNT(*) aantal FROM SPOT) ORDER BY aantal DESC";
                    break;
                case Soort.Huurbaar:
                    query = "SELECT * FROM (SELECT TYPE as CATEGORIE, COUNT(CODE) aantal FROM ITEM i JOIN RENTABLE r ON r.Id = i.soort WHERE aanwezig = 1 GROUP BY type UNION ALL SELECT 'Spot' as CATEGORIE, COUNT(*) aantal FROM SPOT s JOIN RENTABLE r ON r.Id = s.soort WHERE aanwezig = 1) ORDER BY aantal DESC";
                    break;
                default:
                    query = "SELECT * FROM (SELECT TYPE as CATEGORIE, COUNT(CODE) aantal FROM ITEM i JOIN RENTABLE r ON r.Id = i.soort WHERE aanwezig = 0 GROUP BY type UNION ALL SELECT 'Spot' as CATEGORIE, COUNT(*) aantal FROM SPOT s JOIN RENTABLE r ON r.Id = s.soort WHERE aanwezig = 0) ORDER BY aantal DESC";
                    break;
            }

            List<Dictionary<string, object>> data = getQuery(query);

            if (data == null)
                return ret;

            for (int c = 0; c < data.Count; c++)
                ret.Add((string)data[c]["categorie"], Convert.ToInt16(data[c]["aantal"]));

            return ret;
        }

        public Dictionary<string, int> getCatAantal(Soort soort, string find)
        {
            Dictionary<string, int> ret = new Dictionary<string, int>();

            string query;

            switch (soort)
            {
                case Soort.Alles:
                    query = "SELECT * FROM (SELECT TYPE as CATEGORIE, COUNT(CODE) aantal FROM ITEM GROUP BY TYPE UNION ALL SELECT 'Spot' as CATEGORIE, COUNT(*) aantal FROM SPOT) WHERE CATEGORIE LIKE '%" + find + "%' ORDER BY aantal DESC";
                    break;
                case Soort.Huurbaar:
                    query = "SELECT * FROM (SELECT TYPE as CATEGORIE, COUNT(CODE) aantal FROM ITEM i JOIN RENTABLE r ON r.Id = i.soort WHERE aanwezig = 1 GROUP BY type UNION ALL SELECT 'Spot' as CATEGORIE, COUNT(*) aantal FROM SPOT s JOIN RENTABLE r ON r.Id = s.soort WHERE aanwezig = 1) WHERE CATEGORIE LIKE '%" + find + "%' ORDER BY aantal DESC";
                    break;
                default:
                    query = "SELECT * FROM (SELECT TYPE as CATEGORIE, COUNT(CODE) aantal FROM ITEM i JOIN RENTABLE r ON r.Id = i.soort WHERE aanwezig = 0 GROUP BY type UNION ALL SELECT 'Spot' as CATEGORIE, COUNT(*) aantal FROM SPOT s JOIN RENTABLE r ON r.Id = s.soort WHERE aanwezig = 0) WHERE CATEGORIE LIKE '%" + find + "%' ORDER BY aantal DESC";
                    break;
            }

            List<Dictionary<string, object>> data = getQuery(query);

            if (data == null)
                return ret;

            for (int c = 0; c < data.Count; c++)
                ret.Add((string)data[c]["categorie"], Convert.ToInt16(data[c]["aantal"]));

            return ret;
        }

        public List<Item> getItems(string cat, Soort soort)
        {
            List<Item> ret = new List<Item>();

            string query;

            switch (soort)
            {
                case Soort.Alles:
                    query = "SELECT ID, PRIJSPERDAG, NAAM, BESCHRIJVING, AANWEZIG FROM (SELECT r.*, i.TYPE AS CATEGORIE FROM RENTABLE r JOIN ITEM i ON i.SOORT = r.ID UNION ALL SELECT ID,  PRIJSPERDAG, NAAM, BESCHRIJVING || ' | ' || OMGEVING || ' | Personen: ' || PERSONEN as BESCHRIJVING, AANWEZIG, 'Spot' AS CATEGORIE FROM RENTABLE r JOIN SPOT s ON s.SOORT = r.ID ) WHERE CATEGORIE = '" + cat + "'";
                    break;
                case Soort.Huurbaar:
                    query = "SELECT ID,  PRIJSPERDAG, NAAM, BESCHRIJVING, AANWEZIG FROM (SELECT r.*, i.TYPE AS CATEGORIE FROM RENTABLE r JOIN ITEM i ON i.SOORT = r.ID UNION ALL SELECT ID,  PRIJSPERDAG, NAAM, BESCHRIJVING || ' | ' || OMGEVING || ' | Personen: ' || PERSONEN as BESCHRIJVING, AANWEZIG, 'Spot' AS CATEGORIE FROM RENTABLE r JOIN SPOT s ON s.SOORT = r.ID ) WHERE CATEGORIE = '" + cat + "' AND AANWEZIG = 1";
                    break;
                default:
                    query = "SELECT ID,  PRIJSPERDAG, NAAM, BESCHRIJVING, AANWEZIG FROM (SELECT r.*, i.TYPE AS CATEGORIE FROM RENTABLE r JOIN ITEM i ON i.SOORT = r.ID UNION ALL SELECT ID,  PRIJSPERDAG, NAAM, BESCHRIJVING || ' | ' || OMGEVING || ' | Personen: ' || PERSONEN as BESCHRIJVING, AANWEZIG, 'Spot' AS CATEGORIE FROM RENTABLE r JOIN SPOT s ON s.SOORT = r.ID ) WHERE CATEGORIE = '" + cat + "' AND AANWEZIG = 0";
                    break;
            }

            List<Dictionary<string, object>> data = getQuery(query);

            if (data == null)
                return ret;

            for (int c = 0; c < data.Count; c++)
                ret.Add(new Item(Convert.ToInt16(data[c]["id"]), (string)data[c]["naam"], (string)data[c]["beschrijving"], (float)data[c]["prijsperdag"]));

            return ret;
        }

        public List<Item> getItems(string cat, Soort soort, string find)
        {
            List<Item> ret = new List<Item>();

            string query;

            switch (soort)
            {
                case Soort.Alles:
                    query = "SELECT ID,  PRIJSPERDAG, NAAM, BESCHRIJVING, AANWEZIG FROM (SELECT r.*, i.TYPE AS CATEGORIE FROM RENTABLE r JOIN ITEM i ON i.SOORT = r.ID UNION ALL SELECT ID,  PRIJSPERDAG, NAAM, BESCHRIJVING || ' | ' || OMGEVING || ' | Personen: ' || PERSONEN as BESCHRIJVING, AANWEZIG, 'Spot' AS CATEGORIE FROM RENTABLE r JOIN SPOT s ON s.SOORT = r.ID ) WHERE CATEGORIE = '" + cat + "' AND NAAM LIKE '%" + find + "%' OR BESCHRIJVING LIKE '%" + find + "%'";
                    break;
                case Soort.Huurbaar:
                    query = "SELECT ID,  PRIJSPERDAG, NAAM, BESCHRIJVING, AANWEZIG FROM (SELECT r.*, i.TYPE AS CATEGORIE FROM RENTABLE r JOIN ITEM i ON i.SOORT = r.ID UNION ALL SELECT ID,  PRIJSPERDAG, NAAM, BESCHRIJVING || ' | ' || OMGEVING || ' | Personen: ' || PERSONEN as BESCHRIJVING, AANWEZIG, 'Spot' AS CATEGORIE FROM RENTABLE r JOIN SPOT s ON s.SOORT = r.ID ) WHERE CATEGORIE = '" + cat + "' AND AANWEZIG = 1 AND NAAM LIKE '%" + find + "%' OR BESCHRIJVING LIKE '%" + find + "%'";
                    break;
                default:
                    query = "SELECT ID,  PRIJSPERDAG, NAAM, BESCHRIJVING, AANWEZIG FROM (SELECT r.*, i.TYPE AS CATEGORIE FROM RENTABLE r JOIN ITEM i ON i.SOORT = r.ID UNION ALL SELECT ID,  PRIJSPERDAG, NAAM, BESCHRIJVING || ' | ' || OMGEVING || ' | Personen: ' || PERSONEN as BESCHRIJVING, AANWEZIG, 'Spot' AS CATEGORIE FROM RENTABLE r JOIN SPOT s ON s.SOORT = r.ID ) WHERE CATEGORIE = '" + cat + "' AND AANWEZIG = 0 AND NAAM LIKE '%" + find + "%' OR BESCHRIJVING LIKE '%" + find + "%'";
                    break;
            }

            List<Dictionary<string, object>> data = getQuery(query);

            if (data == null)
                return ret;

            for (int c = 0; c < data.Count; c++)
                ret.Add(new Item(Convert.ToInt16(data[c]["id"]), (string)data[c]["naam"], (string)data[c]["beschrijving"], (float)data[c]["prijsperdag"]));

            return ret;

        }

        public bool addVerhuur(int item, string rfid, DateTime start, DateTime back)
        {
            int Id = getLatestId("Rented");
            List<Dictionary<string, object>> data = getQuery("SELECT id FROM gebruiker WHERE rfid ='" + rfid + "'");



            int user = Convert.ToInt32(data[0]["id"]);

            List<Dictionary<string, object>> data2 = getQuery("SELECT aanwezig FROM rentable WHERE id = " + user);
            if (Convert.ToInt32(data2[0]["aanwezig"]) == 0)
            { return false; }


            else if (doQuery("INSERT INTO Rented (Id, Gebruiker, Rentable, AfstaanEmployee, AfstaanDatum, TerugkomDatum) VALUES (" + Id + ", " + user + ", " + item + ", " + ((Employee)Logged).id + ", to_date('" + start.ToString("MM-dd-yyyy hh:mm") + "','MM-DD-YYYY hh24:MI'), to_date('" + back.ToString("MM-dd-yyyy hh:mm") + "','MM-DD-YYYY hh24:MI'))") > 0)
            { return doQuery("UPDATE Rentable SET Aanwezig = 0 WHERE Id = " + item) > 0; }



            return false;
        }

        public bool brengTerug(int item, bool betaald)
        {
            if (doQuery("UPDATE Rented SET AanneemEmployee = " + ((Employee)Logged).id + " WHERE Rentable = " + item) > 0)
                return doQuery("UPDATE Rentable SET Aanwezig = 1 WHERE Id = " + item) > 0;

            return false;
        }

        public bool checkrfid(string rfid)
        {
            List<Dictionary<string, object>> data = getQuery("SELECT id FROM gebruiker WHERE rfid ='" + rfid + "'");
            if( data[0]["id"] == null)
            {  return true; }
            else 
            { return false;}
        }
    }
}

// Get Items (toon)
// Get Items (toon, query)
// Find item (Code)
// Huur Item (Item)
// Return Item (Item)
