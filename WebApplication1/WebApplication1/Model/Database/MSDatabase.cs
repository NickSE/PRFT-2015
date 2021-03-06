﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Model;

namespace WebApplication1.DB
{
    public class MSDatabase : Database
    {

        internal List<Contribution> getContributions(int account, string forCat)
        {
            List<Contribution> ret = new List<Contribution>();
            string order = "";
            string onlyCat = "";
            if (forCat != "-1" && forCat != "")
            {
                order = "AND \"categorie_id\" = " + forCat + " ";
                onlyCat = "AND \"soort\" = 'bestand' ";
            }
            List<Dictionary<string, object>> all = getQuery("SELECT b.id, NVL(a.\"like\", 0) AS \"liked\", NVL(a.\"ongewenst\", 0) AS \"reported\", ac.\"gebruikersnaam\", b.\"datum\", b.\"soort\", COUNT(ab.\"like\") AS \"likes\" FROM bijdrage b LEFT JOIN account_bijdrage a ON b.id = a.\"bijdrage_id\" AND a.\"account_id\" = " + account + " LEFT JOIN account_bijdrage ab ON b.id = ab.\"bijdrage_id\" AND ab.\"like\" = 1 JOIN account ac ON ac.id = b.\"account_id\" LEFT JOIN BIJDRAGE_BERICHT bb ON b.id = bb.\"bericht_id\" WHERE bb.\"bijdrage_id\" IS NULL " + onlyCat + " GROUP BY b.id, ac.\"gebruikersnaam\", NVL(a.\"like\", 0), NVL(a.\"ongewenst\", 0), b.\"datum\", b.\"soort\" ORDER BY b.\"datum\" DESC, b.id");

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
                Contribution c = new Contribution(Convert.ToInt32(con["id"]), (DateTime)con["datum"], Convert.ToInt32(con["likes"]), (string)con["gebruikersnaam"], Convert.ToInt32(con["liked"]) == 1, Convert.ToInt32(con["reported"]) == 1);

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
                    List<Dictionary<string, object>> userfiles = getQuery("SELECT be.\"bijdrage_id\", be.\"categorie_id\", be.\"bestandslocatie\", be.\"grootte\" FROM BESTAND be WHERE be.\"bijdrage_id\" = " + Convert.ToInt32(con["id"]) + " " + order);
                    Dictionary<string, object> userfile;
                    if (userfiles.Count < 1)
                        continue;
                    else
                        userfile = userfiles[0];

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
                    List<Dictionary<string, object>> messages = getQuery("SELECT NVL(\"inhoud\", ' ') as inhoud, NVL(\"titel\", ' ') as titel FROM bericht WHERE \"bijdrage_id\" = " + Convert.ToInt32(con["id"]));
                    Dictionary<string, object> message;
                    if (messages.Count < 1)
                        continue;
                    else
                        message = messages[0];
                    
                    ret.Add(new Message(c, (string)message["inhoud"], (string)message["titel"], null));
                }
            }
            return ret;
        }



        internal List<Message> getReaction(Contribution con)
        {
            List<Message> ret = new List<Message>();
            List<Dictionary<string, object>> reactions = getQuery("SELECT b.id, ac.\"gebruikersnaam\", b.\"datum\", NVL(\"inhoud\", ' ') as \"inhoud\" FROM bijdrage b JOIN account ac ON ac.id = b.\"account_id\" LEFT JOIN BIJDRAGE_BERICHT bb ON b.id = bb.\"bericht_id\" JOIN BERICHT br ON br.\"bijdrage_id\" = b.ID WHERE bb.\"bijdrage_id\" = "+con.id+" ORDER BY b.\"datum\" DESC, b.id");

            foreach (Dictionary<string, object> reaction in reactions)
            {
                ret.Add(new Message(new Contribution(Convert.ToInt32(reaction["id"]), (DateTime)reaction["datum"], 0, (string)reaction["gebruikersnaam"], false, false), (string)reaction["inhoud"], null, con));
            }

            return ret;
        }

        internal List<Category> getCategories()
        {
            List<Category> ret = new List<Category>();
            List<Dictionary<string, object>> categories = getQuery("SELECT \"bijdrage_id\", NVL(\"categorie_id\", -1) \"categorie_id\", \"naam\" FROM CATEGORIE c CONNECT BY c.\"categorie_id\" = prior c.\"bijdrage_id\" start with \"categorie_id\" IS NULL");

            foreach (Dictionary<string, object> cat in categories)
            {
                ret.Add(new Category(new Contribution(Convert.ToInt32(cat["bijdrage_id"]), new DateTime(), 0, "", false, false), (string)cat["naam"], null));
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

        internal void sendReaction(int parent, string content, string account)
        {
            int bijdrage_id = getLatestId("bijdrage");
            int account_id = getSessionId(account);

            if (doQuery("INSERT INTO BIJDRAGE VALUES(" + bijdrage_id + ", " + account_id + ", SYSDATE, \'bericht\')") > 0)
                if (doQuery("INSERT INTO BERICHT VALUES(" + bijdrage_id + ", '', '" + content + "')") > 0)
                    doQuery("INSERT INTO BIJDRAGE_BERICHT VALUES(" + parent + ", " + bijdrage_id + ")");
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


        internal void sendFile(string fname, int size, int parent, string account)
        {
            int bijdrage_id = getLatestId("bijdrage");
            int account_id = getSessionId(account);

            if (doQuery("INSERT INTO BIJDRAGE VALUES(" + bijdrage_id + ", " + account_id + ", SYSDATE, \'bestand\')") > 0)
                if (doQuery("INSERT INTO BESTAND VALUES(" + bijdrage_id + ", " + parent + ", '" + fname + "', "+size+")") <= 0)
                {
                    doQuery("DELETE FROM BIJDRAGE WHERE ID=" + bijdrage_id);
                }
        }
    }
}