using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Model
{
    public class Message : Contribution
    {

        public string Content { get; set; }

        public string Title { get; set; }

        public Contribution Reaction { get; set; }

        public Message(Contribution con, string content, string title, Contribution reaction) : base(con)
        {
            this.Content = content;
            this.Title = title;
            this.Reaction = reaction;
        }
    }
}