using System;
using Oracle.DataAccess.Client;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Events_Group1
{
    class MediaSharingDatabase : Database
    {       
        //fields
        //propperties
        //constructor
        //methodes
        public bool sendMessage(Message bericht)
        {
            //Stuurt het nieuw geplaatste bericht naar de database
            if (bericht.ReactieOp != 0)
            {
                //Add reactie
                return doQuery("INSERT INTO Message(id, gebruiker, reaction, titel, inhoud, path, datum, zichtbaar) VALUES(" + bericht.Id + ", " + bericht.Auteur.Id + ", " + bericht.ReactieOp + ", '" + bericht.Title + "', '" + bericht.Inhoud + "', '" + bericht.Attachment + "', TIMESTAMP'1999-12-01 10:00:00', 1)") > 0;
            }
            else
            {
                //Add post
                return doQuery("INSERT INTO Message(id, gebruiker, reaction, titel, inhoud, path, datum, zichtbaar) VALUES(" + bericht.Id + ", " + bericht.Auteur.Id + ", null, '" + bericht.Title + "', '" + bericht.Inhoud + "', '" + bericht.Attachment + "', TIMESTAMP'1999-12-01 10:00:00', 1)") > 0;
            }
        }
        //GET MESSAGES
        public List<Message> getMessages(Event eventnaam)
        {
            List<Message> mes = new List<Message>();

            List<Dictionary<string, object>> messages = getQuery("SELECT m.id as id, Titel, Inhoud, g.id as gebruiker, username, voornaam, tussenvoegsel, achternaam FROM Message m JOIN Gebruiker g ON m.Gebruiker = g.id WHERE reaction IS NULL AND zichtbaar = 1 ORDER BY id DESC");
            if (messages == null)
            {
                return null;
            }

            int count = messages.Count;

            if (count > 100)
                count = 100;

            Dictionary<int, User> users = new Dictionary<int, User>();

            for (int i = 0; i < count; i++)
            {
                Dictionary<string, object> data = messages[i];
                User auteur;
                int id = Convert.ToInt32(data["gebruiker"]);
                if (users.ContainsKey(id))
                    auteur = users[id];
                else
                {
                    auteur = new User(Convert.ToInt32(data["gebruiker"]), (string)data["username"], (string)data["voornaam"], (string)data["tussenvoegsel"], (string)data["achternaam"]);
                    users.Add(Convert.ToInt32(id), auteur);
                }

                Dictionary<string, object> cur = messages[i];
                mes.Add(new Message(Convert.ToInt16(cur["id"]), (string)cur["titel"], (string)cur["inhoud"], auteur));
            }
            return mes;

        }
        //GET MESSAGES FROM CAT
        public List<Message> getMessagesFromCat(Event eventnaam, string catagory)
        {
            List<Message> mes = new List<Message>();

            List<Dictionary<string, object>> messages = getQuery("SELECT m.id as id, Titel, Inhoud, g.id as gebruiker, username, voornaam, tussenvoegsel, achternaam FROM Message m JOIN Gebruiker g ON m.Gebruiker = g.id JOIN MESINCAT mes ON mes.MESSAGE = m.id WHERE reaction IS NULL AND zichtbaar = 1 AND mes.category = '" + catagory + "' ORDER BY id DESC");
            if (messages == null)
            {
                return null;
            }

            int count = messages.Count;

            if (count > 100)
                count = 100;

            Dictionary<int, User> users = new Dictionary<int, User>();

            for (int i = 0; i < count; i++)
            {
                Dictionary<string, object> data = messages[i];
                User auteur;
                int id = Convert.ToInt32(data["gebruiker"]);
                if (users.ContainsKey(id))
                    auteur = users[id];
                else
                {
                    auteur = new User(Convert.ToInt32(data["gebruiker"]), (string)data["username"], (string)data["voornaam"], (string)data["tussenvoegsel"], (string)data["achternaam"]);
                    users.Add(Convert.ToInt32(id), auteur);
                }

                Dictionary<string, object> cur = messages[i];
                mes.Add(new Message(Convert.ToInt16(cur["id"]), (string)cur["titel"], (string)cur["inhoud"], auteur));
            }
            return mes;

        }
        //Get reactions on a messages
        public List<Message> getMessages(Event eventnaam, Message bericht)
        {
            List<Message> mes = new List<Message>();

            List<Dictionary<string, object>> messages = getQuery("SELECT m.id as id, Inhoud, reaction, g.id as gebruiker, username, voornaam, tussenvoegsel, achternaam FROM Message m JOIN Gebruiker g ON m.Gebruiker = g.id WHERE reaction = " + bericht.Id + " AND zichtbaar = 1 ORDER BY id DESC");
            if (messages == null)
            {
                return null;
            }

            int count = messages.Count;

            if (count > 100)
                count = 100;

            Dictionary<int, User> users = new Dictionary<int, User>();

            for (int i = 0; i < count; i++)
            {
                Dictionary<string, object> data = messages[i];
                User auteur;
                int id = Convert.ToInt32(data["gebruiker"]);
                if (users.ContainsKey(id))
                    auteur = users[id];
                else
                {
                    auteur = new User(Convert.ToInt32(data["gebruiker"]), (string)data["username"], (string)data["voornaam"], (string)data["tussenvoegsel"], (string)data["achternaam"]);
                    users.Add(Convert.ToInt32(id), auteur);
                }

                Dictionary<string, object> cur = messages[i];
                mes.Add(new Message(Convert.ToInt16(cur["id"]), (string)cur["inhoud"], auteur, Convert.ToInt32(cur["reaction"])));
            }
            return mes;

        }
        public bool sendLike(Message message, User user, int id)
        {
            try
            {
                doQuery("INSERT INTO Leuk(ID, GEBRUIKER, MESSAGE, DATUM) VALUES(" + id + ", " + user.Id + ", " + message.Id + ", TIMESTAMP'1999-12-01 10:00:00')");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool sendDislike(Message message, User user)
        {
            try
            {
                doQuery("DELETE FROM LEUK WHERE GEBRUIKER = " + user.Id + " AND MESSAGE = " + message.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool checkLike(Message message, User user)//Check if a person likes a post
        {
            List<Dictionary<string, object>> data = getQuery("SELECT Id FROM LEUK WHERE gebruiker = " + user.Id + " AND message = " + message.Id);
            if (data.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int countLike(Message message)//Check if a person likes a post
        {
            List<Dictionary<string, object>> data = getQuery("SELECT count(Id) AS amount FROM LEUK WHERE message = " + message.Id);
            return 1; //Convert.ToInt32(data["amount"]);
        }
        public Event getEvent(User user)
        {
            //GET EVENT FROM USER + DATA FROM THE EVENT
            List<Dictionary<string, object>> events = getQuery("SELECT * FROM Event WHERE id IN (SELECT event FROM Gebruiker WHERE id = " + user.Id + ")");
            //CHECK IF IT CONTAINS DATA
            if (events == null)
            {
                return null;
            }
            //GET FIRST RESULT
                Dictionary<string, object> data = events[0];
            //CREATE THE EVENT
                Event e = new Event(Convert.ToInt16(data["id"]), (string)data["naam"], (string)data["beschrijving"], Convert.ToDateTime(data["startdatum"]), Convert.ToDateTime(data["einddatum"]), (float)data["kosten"]);

            return e;
        }
        public List<string> getCategories()
        {
            List<string> catres = new List<string>();

            List<Dictionary<string, object>> categories = getQuery("SELECT c.Naam FROM CATEGORY c LEFT JOIN MESINCAT m ON c.Naam = m.Category GROUP BY c.Naam ORDER BY COUNT(m.Message) DESC");
            if (categories == null)
            {
                return null;
            }
            for (int i = 0; i < categories.Count; i++)
            {
                Dictionary<string, object> data = categories[i];
                catres.Add((string)data["naam"]);
            }
            return catres;
        }
        public bool newCategorie(string cat)
        {
            return doQuery("INSERT INTO Category(naam) VALUES('" + cat + "')") > 0;
        }
        public bool newCatinMsg(Message message, string cat)
        {
            return doQuery("INSERT INTO Mesincat(message, category) VALUES('" + message.Id + "', '" + cat + "')") > 0;
        }
    }
}
