using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.DB
{
    // Account database
    public class ADatabase : Database
    {
        public List<Dictionary<string, object>> getAccount(int account_id)
        {
            List<Dictionary<string, object>> data = getQuery("SELECT \"voornaam\", NVL(\"tussenvoegsel\", ' ') tussenvoegsel, \"achternaam\", \"straat\", \"huisnr\", \"woonplaats\", NVL(\"betaald\", 0) betaald FROM persoon p LEFT JOIN reservering r on p.ID = r.\"persoon_id\" WHERE p.ID=" + account_id);

            return data;
        }

        public static void newAccount(int id, string username, string email, string activatiehash)
        {

        }

        public bool GetCode(string code)
        {
            List<Dictionary<string, object>> data = getQuery("SELECT id FROM persoon WHERE polsbandje.barcode = '" + code + "'");//TODO Query

            if (data.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool activateCode(int id, string barcode)
        {
            try
            {
                doQuery("INSERT INTO polsbandje VALUES('" + barcode + "')");//TODO goede query maken
                doQuery("UPDATE gebruiker SET polsbandje = '" + barcode + "'  WHERE id = " + id);//TODO goede query maken
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}