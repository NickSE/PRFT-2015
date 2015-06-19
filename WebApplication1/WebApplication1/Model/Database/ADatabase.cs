using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Model;

namespace WebApplication1.DB
{
    // Account database
    public class ADatabase : Database
    {
        public List<Dictionary<string, object>> getAccount(int account_id)
        {
            List<Dictionary<string, object>> data = getQuery("SELECT \"voornaam\", NVL(\"tussenvoegsel\", ' ') tussenvoegsel, \"achternaam\", \"straat\", \"huisnr\", \"woonplaats\", NVL(\"betaald\", 0) betaald, \"email\" FROM persoon p LEFT JOIN reservering r on p.ID = r.\"persoon_id\" WHERE p.ID=" + account_id);

            return data;
        }

        public static void newAccount(int id, string username, string email, string activatiehash)
        {

        }

        public bool GetCode(string code)
        {
            List<Dictionary<string, object>> data = getQuery("SELECT p.id FROM persoon p inner join reservering r on p.id = r.\"persoon_id\" inner join reservering_polsbandje rp on r.id = rp.\"reservering_id\" inner join polsbandje pb on rp.\"polsbandje_id\" = pb.id where \"barcode\" =  '" + code + "'");

            if (data.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool getAllEntries()
        {
            try
            {
                doQuery("SELECT DISTINCT \"voornaam\", \"tussenvoegsel\", \"achternaam\" FROM persoon p left join reservering r on p.id = r.\"persoon_id\" left join reservering_polsbandje re on r.id = re.\"reservering_id\" where \"aanwezig\" = 1");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool activateCode(int id, string barcode)
        {
            try
            {
                int id_pols = getLatestId("polsbandje");
                string query;
                string aanwezig = "1";
                query = "INSERT INTO polsbandje VALUES(";
                query += id_pols + ", '" + barcode + "', '" + aanwezig + "')";
                //doQuery("INSERT INTO polsbandje VALUES('" + barcode + "', '" + id + "', '" + aanwezig +"')");//TODO goede query maken
                //doQuery("UPDATE polsbandje SET \"barcode\" = '" + barcode + "'  WHERE id = " + id);//TODO goede query maken
                doQuery(query);                
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool deActivateCode(string barcode)
        {
            try
            {
                doQuery("update polsbandje set \"actief\" = '0' where \"barcode\" = '" + barcode + "'");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool createAccount(string gebruikersnaam, string email, string activatiehash)
        {
            try
            {
                string query;
                int id = getLatestId("account");
                query = "INSERT INTO account VALUES(";
                query += id + ", '" + gebruikersnaam + "', '" + email + "', '" + activatiehash + "', '" + 1 + "')";
                doQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Category> getCategories()
        {
            List<Category> ret = new List<Category>();
            List<Dictionary<string, object>> categories = getQuery("select id, \"naam\" from productcat");
            foreach (Dictionary<string, object> cat in categories)
            {
                ret.Add(new Category(new Contribution(Convert.ToInt32(cat["id"])), (string)cat["naam"]));
            }
            return ret;
        }

        public List<Category> getSubCategories(string categorie)
        {
            List<Category> ret = new List<Category>();
            List<Dictionary<string, object>> categories = getQuery("select p.id, p.\"productcat_id\", p.\"merk\", p.\"serie\", p.\"typenummer\", p.\"prijs\", pro.\"naam\" from product p left join productcat pro on p.\"productcat_id\" = pro.ID where \"naam\" ='" + categorie + "'");
            foreach (Dictionary<string, object> cat in categories)
            {
                ret.Add(new Category(new Contribution(Convert.ToInt32(cat["id"])), (string)cat["serie"]));
            }
            return ret;
        }
   
    }
}