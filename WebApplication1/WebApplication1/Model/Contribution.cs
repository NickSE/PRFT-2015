using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Model
{
    public class Contribution
    {
        private int id;

        public int Likes { get; set; }

        public string Author { get; set; }

        public Contribution()
        {
            Likes = 0;
            Author = "TEMP";
        }
    }
}