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
            List<Dictionary<string, object>> AllUsers = getQuery("SELECT * FROM persoon ORDER BY id");
            foreach (Dictionary<string, object> user in AllUsers)
            {
                ret.Add(new User(Convert.ToInt32(user["id"]), Convert.ToString(user["voornaam"]), Convert.ToString(user["tussenvoegsel"]), Convert.ToString(user["achternaam"]), Convert.ToString(user["straat"]), Convert.ToString(user["huisnr"]), Convert.ToString(user["woonplaats"]), Convert.ToString(user["banknr"]), Convert.ToString(user["email"])));
            }

            return ret;
        }
        public User GetUser(int id)
        {
            List<Dictionary<string, object>> AllUsers = getQuery("SELECT * FROM persoon WHERE id = " + id + " ORDER BY id");
            foreach (Dictionary<string, object> user in AllUsers)
            {
                User ret = new User(Convert.ToInt32(user["id"]), Convert.ToString(user["voornaam"]), Convert.ToString(user["tussenvoegsel"]), Convert.ToString(user["achternaam"]), Convert.ToString(user["straat"]), Convert.ToString(user["huisnr"]), Convert.ToString(user["woonplaats"]), Convert.ToString(user["banknr"]), Convert.ToString(user["email"]));
                return ret;
            }

            return null;
        }
        public bool AddUser(User user)
        {
            try
            {
                string query;
                query = "INSERT INTO PERSOON VALUES(";
                query += user.id + ", '" + user.name + "', '" + user.insertion + "', '" + user.lastname + "', '" + user.street + "', '" + user.number + "', '" + user.city + "', '" + user.banknr + "', '" + user.email + "')";
                doQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteUser(int id)
        {
            try
            {
                doQuery("DELETE FROM persoon WHERE id = " + id + " ");
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}