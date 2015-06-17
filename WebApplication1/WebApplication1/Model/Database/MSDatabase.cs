using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Model;

namespace WebApplication1.DB
{
    public class MSDatabase : Database
    {

        internal List<Contribution> getContributions(int account)
        {
            List<Contribution> ret = new List<Contribution>();
            List<Dictionary<string, object>> all = getQuery("SELECT b.id, NVL(a.\"like\", 0) AS \"liked\", ac.\"gebruikersnaam\", b.\"datum\", b.\"soort\", COUNT(ab.\"like\") AS \"likes\" FROM bijdrage b LEFT JOIN account_bijdrage a ON b.id = a.\"bijdrage_id\" AND a.\"account_id\" = "+account+" LEFT JOIN account_bijdrage ab ON b.id = ab.\"bijdrage_id\" JOIN account ac ON ac.id = b.\"account_id\" LEFT JOIN BIJDRAGE_BERICHT bb ON b.id = bb.\"bericht_id\" WHERE bb.\"bijdrage_id\" IS NULL GROUP BY b.id, ac.\"gebruikersnaam\", NVL(a.\"like\", 0), b.\"datum\", b.\"soort\" ORDER BY b.\"datum\" DESC, b.id");

            List<Dictionary<string, object>> contributions = new List<Dictionary<string, object>>();
            int length = 10;
            if (all.Count < length)
                length = all.Count;

            for (int i = 0; i < length; i++)
            {
                contributions.Add(all[i]);
            }

            foreach (Dictionary<string, object> con in contributions)
            {
                Contribution c = new Contribution(Convert.ToInt32(con["id"]), (DateTime)con["datum"], Convert.ToInt32(con["likes"]), (string)con["gebruikersnaam"], Convert.ToInt32(con["liked"]) == 1);

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



        internal List<Message> getReaction(Contribution con)
        {
            List<Message> ret = new List<Message>();
            List<Dictionary<string, object>> reactions = getQuery("SELECT b.id, bb.\"bijdrage_id\" as \"reactieId\", ac.\"gebruikersnaam\", b.\"datum\", b.\"soort\", COUNT(ab.\"like\") AS \"likes\", NVL(\"inhoud\", ' ') as \"inhoud\", NVL(\"titel\", ' ') as \"titel\" FROM bijdrage b LEFT JOIN account_bijdrage ab ON b.id = ab.\"bijdrage_id\" JOIN account ac ON ac.id = b.\"account_id\" LEFT JOIN BIJDRAGE_BERICHT bb ON b.id = bb.\"bericht_id\" JOIN BERICHT br ON br.\"bijdrage_id\" = b.ID WHERE bb.\"bijdrage_id\" = " + con.id + " GROUP BY b.id, ac.\"gebruikersnaam\", b.\"datum\", b.\"soort\", \"inhoud\", \"titel\", bb.\"bijdrage_id\" ORDER BY b.\"datum\" DESC, b.id");

            foreach (Dictionary<string, object> reaction in reactions)
            {
                ret.Add(new Message(new Contribution(Convert.ToInt32(reaction["id"]), (DateTime)reaction["datum"], Convert.ToInt32(reaction["likes"]), (string)reaction["gebruikersnaam"], false), (string)reaction["inhoud"], null, con));
            }

            return ret;
        }

        internal List<Category> getCategories()
        {
            List<Category> ret = new List<Category>();
            List<Dictionary<string, object>> categories = getQuery("SELECT \"bijdrage_id\", NVL(\"categorie_id\", -1) \"categorie_id\", \"naam\" FROM CATEGORIE c CONNECT BY c.\"categorie_id\" = prior c.\"bijdrage_id\" start with \"categorie_id\" IS NULL");

            foreach (Dictionary<string, object> cat in categories)
            {
                ret.Add(new Category(new Contribution(Convert.ToInt32(cat["bijdrage_id"]), new DateTime(), 0, "", false), (string)cat["naam"], null));
            }

            return ret;
        }

        internal void sendMessage(string title, string content, string account)
        {
            int bijdrage_id = getLatestId("bijdrage");
            int account_id = getSessionId(account);

            if (!(doQuery("INSERT INTO BIJDRAGE VALUES(" + bijdrage_id + ", " + account_id + ", SYSDATE, \'bericht\')") > 0 && doQuery("INSERT INTO BERICHT VALUES(" + bijdrage_id + ", '" + title + "', '" + content + "')") > 0))
            {
                // Bericht is niet toegevoegd
            }

        }

        internal void sendCategory(string naam, int parent, string account)
        {
            int bijdrage_id = getLatestId("bijdrage");
            int account_id = getSessionId(account);
            string query = "INSERT INTO CATEGORIE VALUES(" + bijdrage_id + ", " + parent + ", '" + naam + "')";

            if (parent == -1)
                query = "INSERT INTO CATEGORIE VALUES(" + bijdrage_id + ", NULL, '" + naam + "')";

            if (doQuery("INSERT INTO BIJDRAGE VALUES(" + bijdrage_id + ", " + account_id + ", SYSDATE, \'categorie\')") > 0)
                if(doQuery(query) <= 0)
                {
                    doQuery("DELETE FROM BIJDRAGE WHERE ID="+bijdrage_id);
                }
        }

        internal bool doAction(int id, string account, string action)
        {
            int account_id = getSessionId(account);
            return doQuery("BEGIN DOACTION(" + account_id + ", " + id + ", '" + action + "'); END;") > 0;
            
        }

    }
}