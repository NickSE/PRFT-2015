using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Model;
using WebApplication1.UserManagement;

namespace WebApplication1.DB
{
    public class UMDatabase : Database
    {
        public List<User> GetAllUsers()
        {
            List<User> ret = new List<User>();
            List<Dictionary<string, object>> AllUsers = getQuery("SELECT * FROM persoon;");
            foreach (Dictionary<string, object> user in AllUsers)
            {
                ret.Add(new User(Convert.ToInt32(user["id"]), (string)user["voornaam"], (string)user["tussenvoegsel"], (string)user["achternaam"], (string)user["straat"], (string)user["huisnr"], (string)user["woonplaats"], (string)user["banknr"]));
            }

            return ret;
        }
        public bool AddUser(User user)
        {
            try
            {
                string query;
                query = "INSERT INTO persoon(id, voornaam, tussenvoegsel, achternaam, straat, huisnr, woonplaats, banknr) VALUES(";
                query += user.id + ", " + user.name + ", " + user.insertion + ", " + user.lastname + ", " + user.street + ", " + user.number + ", " + user.city + ", " + user.banknr + ");";
                doQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool deleteEvent(User user)
        {
            try
            {
                doQuery("DELETE FROM persoon WHERE id = " + user.id + ";");
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}