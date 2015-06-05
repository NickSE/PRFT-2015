using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Model;

namespace WebApplication1.DB
{
    public class MSDatabase : Database
    {

        internal List<Contribution> getContributions()
        {
            List<Contribution> ret = new List<Contribution>();
            List<Dictionary<string, object>> contributions = getQuery("SELECT b.id, ac.\"gebruikersnaam\", b.\"datum\", b.\"soort\", COUNT(ab.\"like\") AS \"likes\" FROM bijdrage b LEFT JOIN account_bijdrage ab ON b.id = ab.\"bijdrage_id\" JOIN account ac ON ac.id = b.\"account_id\" GROUP BY b.id, ac.\"gebruikersnaam\", b.\"datum\", b.\"soort\" ORDER BY b.\"datum\" DESC, b.id");

            foreach (Dictionary<string, object> con in contributions)
            {
                Contribution c = new Contribution(Convert.ToInt32(con["id"]), (DateTime)con["datum"], Convert.ToInt32(con["likes"]), (string)con["gebruikersnaam"]);

                if ((string)con["soort"] == "categorie")
                {
                    List<Dictionary<string, object>> cats = getQuery("SELECT \"naam\" FROM CATEGORIE START WITH \"bijdrage_id\" = " + Convert.ToInt32(con["id"]) + " CONNECT BY PRIOR \"categorie_id\" = \"bijdrage_id\" ORDER BY \"bijdrage_id\"");
                    Category cur = null;
                    foreach(Dictionary<string, object> cat in cats)
                    {
                        cur = new Category(c, (string)cat["naam"], cur);
                    }
                    ret.Add(cur);
                }
                else if ((string)con["soort"] == "bestand")
                {
                    Dictionary<string, object> userfile = getQuery("SELECT be.\"bijdrage_id\", be.\"categorie_id\", be.\"bestandslocatie\", be.\"grootte\" FROM BESTAND be WHERE be.\"bijdrage_id\" = " + Convert.ToInt32(con["id"]))[0];

                    List<Dictionary<string, object>> cats = getQuery("SELECT \"naam\" FROM CATEGORIE START WITH \"categorie_id\" = " + Convert.ToInt32(userfile["bijdrage_id"]) + " CONNECT BY PRIOR \"categorie_id\" = \"bijdrage_id\" ORDER BY \"bijdrage_id\"");
                    Category cur = null;
                    foreach (Dictionary<string, object> cat in cats)
                    {
                        cur = new Category(c, (string)cat["naam"], cur);
                    }
                    ret.Add(new UserFile(c, (string)userfile["bestandslocatie"], Convert.ToInt32(userfile["grootte"]), cur));
                }

                else if ((string)con["soort"] == "bericht")
                {
                    Dictionary<string, object> message = getQuery("SELECT NVL(\"inhoud\", ' ') as inhoud, NVL(\"titel\", ' ') as titel FROM bericht WHERE \"bijdrage_id\" = " + Convert.ToInt32(con["id"]))[0];
                    ret.Add(new Message(c, (string)message["inhoud"], (string)message["titel"], null));
                }
            }
            return ret;
        }
    }
}