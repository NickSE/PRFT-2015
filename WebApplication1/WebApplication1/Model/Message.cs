using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Model
{
    public class Message : Contribution
    {

        public string Content { get; set; }

        public Message(string Content)
        {
            this.Content = Content;
        }
    }
}