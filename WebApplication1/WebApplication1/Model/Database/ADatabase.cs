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
            List<Dictionary<string, object>> data = getQuery("SELECT \"voornaam\", \"tussenvoegsel\", \"achternaam\", \"straat\", \"huisnr\", \"woonplaats\", \"betaald\" FROM persoon p LEFT JOIN reservering r on p.ID = r.\"persoon_id\" WHERE p.ID=" + account_id);

            return data;
        }

        public static void newAccount(int id, string username, string email, string activatiehash)
        {

        }
    }
}