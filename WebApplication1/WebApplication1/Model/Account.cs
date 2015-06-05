using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Model
{
    public class Account
    {
        private int id;
        private string username;
        public string Username { get { return username; } }

        public Account(int id)
        {
            this.id = id;

            // Get username from db
            //username = ADatabase.getUsername(id);
        }

        public Account(int id, string username, string email, string activatiehash)
        {
            this.id = id;
            this.username = username;

            // Create account in db
            //ADatabase.newAccount(id, username, email, activatiehash);
        }
    }
}