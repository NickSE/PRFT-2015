using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Model
{
    public class Contribution
    {
        public int id;

        public int Likes { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }

        public Contribution(Contribution con)
        {
            this.id = con.id;
            this.Author = con.Author;
            this.Date = con.Date;
            this.Likes = con.Likes;
        }

        public Contribution(int id, DateTime date, int likes, string author)
        {
            this.id = id;
            this.Date = date;
            this.Likes = likes;
            this.Author = author;
        }
    }
}