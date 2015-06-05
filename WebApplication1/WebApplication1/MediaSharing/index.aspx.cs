using System;
using System.Collections.Generic;
using System.Linq;
//using Oracle.DataAccess.Type;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.DB;
using WebApplication1.Model;

namespace WebApplication1.Scherm
{
    public partial class Mediasharing : System.Web.UI.Page
    {
        MSDatabase db = new MSDatabase();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Add event listeners
            sendNewMessage.Click += sendNewMessage_Click;

            string type;
            // Load messages
            foreach (Contribution i in db.getContributions())
            {
                if (i is Message) type = "message";
                else if(i is UserFile) type = "file";
                else type = "category";

                // Start artikel
                article_list.InnerHtml += "<article>" + "\n";

                // Display author
                article_list.InnerHtml += "<div class=\"author\">";
                article_list.InnerHtml += "<span>" + i.Author + "</span>";
                article_list.InnerHtml += "</div>" + "\n";

                article_list.InnerHtml += "<div class=\"content "+ type +"\">";
                // Content
                if (i is Message)
                {
                    article_list.InnerHtml += ((Message)i).Content;
                } else
                if (i is UserFile)
                {
                    /* Bekijk wat voor bestand dit is;
                    ** Image: gebruik <img src"" />
                    ** Mp4: Gebruik <video> <source>
                    ** Mp3: Gebruik <audio> <source>
                     */
                } else
                if (i is Category)
                {
                    article_list.InnerHtml += i.Author + " heeft " + ((Category)i).Name + " toegevoegd";
                }

                article_list.InnerHtml += "</div>" + "\n";

                // Geef artikel een Like, Reageer en report functie
                article_list.InnerHtml += "<div class=\"do\">";
                article_list.InnerHtml += "<button ID=\"doLike\" class=\"btn btn-success\" data-contribution=\"" + i.id + "\" runat=\"server\">Like</button>";
                article_list.InnerHtml += "<button ID=\"doReact\" class=\"btn btn-primary\" data-contribution=\"" + i.id + "\" runat=\"server\">Reageer</button>";
                article_list.InnerHtml += "<button ID=\"doReport\" class=\"btn btn-danger\" data-contribution=\"" + i.id + "\" runat=\"server\">Ongewenst</button>";
                article_list.InnerHtml += "</div>" + "\n";

                // Geef artikel aantal likes
                article_list.InnerHtml += "<div class\"likes\">";
                article_list.InnerHtml += "<span><b>" + i.Likes + "</b> like(s)</span>";
                article_list.InnerHtml += "</div>" + "\n";

                // Laadt reacties
                // ...

                // Sluit artikel
                article_list.InnerHtml += "</article>" + "\n";
            }
        }

        void sendNewMessage_Click(object sender, EventArgs e)
        {
            // Send new message to DB
            // MSDatabase.addMessage()
        }

    }
}