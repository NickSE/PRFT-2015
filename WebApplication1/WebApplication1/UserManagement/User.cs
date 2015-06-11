using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.UserManagement
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string insertion { get; set; }
        public string lastname { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public string city { get; set; }
        public string banknr { get; set; }
        public User(int id, string name, string insertion, string lastname, string street, string number, string city, string banknr)
        {
            this.id = id;
            this.name = name;
            this.insertion = insertion;
            this.lastname = lastname;
            this.street = street;
            this.number = number;
            this.city = city;
            this.banknr = banknr;
        }
        public override string ToString()
        {
            return (id + " - " + name + " " + insertion + " " + lastname);
        }
    }
}