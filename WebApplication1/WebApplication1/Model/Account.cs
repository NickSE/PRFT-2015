using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Model
{
    public class Account
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string activatiehash { get; set; }
        public string active { get; set; }
        public Account(int id, string username, string email, string activatiehash, string active)
        {
            this.id = id;
            this.username = username;
            this.email = email;
            this.activatiehash = activatiehash;
            this.active = active;
        }
    }
}