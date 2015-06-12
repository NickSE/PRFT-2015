using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using Oracle.DataAccess.Type;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.DB;
using WebApplication1.Model;

namespace WebApplication1.Scherm
{
    public partial class Mediasharing : Page
    {
        MSDatabase db = new MSDatabase();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
                Response.Redirect("../");

            user.InnerText = Session["login"].ToString();

            // Add event listeners
           // sendNewMessage.Click += sendNewMessage_Click;
            sendMessage.ServerClick += sendMessage_Click;
            sendFile.ServerClick += sendFile_Click;
            sendCategory.ServerClick += sendCategory_Click;

            loadCategories(fileCat, false);
            loadCategories(parentCat, true);
            loadCategories(categories, true);
            loadArticles();
        }

        private void loadCategories(System.Web.UI.HtmlControls.HtmlSelect select, Boolean nieuw)
        {
            select.Items.Clear();

            if(nieuw)
                select.Items.Add(new ListItem("Nieuw...", "-1"));

            foreach (Category c in db.getCategories())
            {
                select.Items.Add(new ListItem(c.Name, c.id.ToString()));
            }
        }

        private void loadArticles() {
            article_list.InnerHtml = "";
            string type;
            // Load messages
            foreach (Contribution i in db.getContributions())
            {
                if (i is Message) type = "message";
                else if (i is UserFile) type = "file";
                else type = "category";

                // Start artikel
                article_list.InnerHtml += "<article>" + "\n";

                // Display author
                article_list.InnerHtml += "<div class=\"author\">";
                article_list.InnerHtml += "<h2>" + i.Author + "</h2>";
                article_list.InnerHtml += "<h5>" + i.Date + "</h5>";
                article_list.InnerHtml += "</div>" + "\n";

                article_list.InnerHtml += "<div class=\"content " + type + "\">";
                // Content
                if (i is Message)
                {
                    article_list.InnerHtml += "<h6><b>" + ((Message)i).Title + "</b></h6>";
                    article_list.InnerHtml += "<p>" + ((Message)i).Content + "</p>";
                }
                else
                    if (i is UserFile)
                    {
                        switch (((UserFile)i).getType())
                        {
                            case "jpg":
                            case "png":
                            case "gif":
                                article_list.InnerHtml += "<img src=\"" + ((UserFile)i).Path + "\" class=\"img-responsive\"/>";
                                break;
                            case "mp3":
                            case "ogg":
                            case "wav":
                                article_list.InnerHtml += "<audio controls src=\"" + ((UserFile)i).Path + "\"> ";
                                article_list.InnerHtml += "Je browser ondersteunt geen audio element.";
                                article_list.InnerHtml += "</audio>";
                                break;
                            case "mp4":
                                article_list.InnerHtml += "<video controls src=\"" + ((UserFile)i).Path + "\"> ";
                                article_list.InnerHtml += "Je browser ondersteunt geen video element.";
                                article_list.InnerHtml += "</video>";
                                break;
                            default:
                                article_list.InnerHtml += "Bestandstype \"" + ((UserFile)i).getType() + "\" kan niet worden geladen.";
                                break;
                        }
                        article_list.InnerHtml += "<br /><a href=\"" + ((UserFile)i).Path + "\" target=\"_blanc\"> Klik hier om het bestand te downloaden </a>";
                    }
                    else
                        if (i is Category)
                        {
                            article_list.InnerHtml += "<b>" + i.Author + "</b> heeft <b>" + ((Category)i).Name + "</b> toegevoegd";
                            if (((Category)i).Parent != null)
                                article_list.InnerHtml += " aan <b>" + ((Category)i).Parent.Name + "</b>";
                        }

                article_list.InnerHtml += "</div>" + "\n";

                // Geef artikel een Like, Reageer en report functie
                article_list.InnerHtml += "<div class=\"do text-right\">";
                article_list.InnerHtml += "<button ID=\"doLike\" class=\"btn btn-link\" onclick=\"Like(" + i.id + ")\">Like</button>";
                article_list.InnerHtml += "<button ID=\"doReact\" class=\"btn btn-link\" onclick=\"openReact(" + i.id + ")\">Reageer</button>";
                article_list.InnerHtml += "<button ID=\"doReport\" class=\"btn btn-link\" onclick=\"Report(" + i.id + ")\">Ongewenst</button>";
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

        private void sendMessage_Click(object sender, EventArgs e)
        {
            string title = titel.Value;
            string content = inhoud.Value;
            db.sendMessage(title, content, Session["login"].ToString());
            loadArticles();
        }
        
        private void sendFile_Click(object sender, EventArgs e)
        {
            HttpPostedFile file = Request.Files["filepath"];

            if(file != null && file.ContentLength > 0)
            {
                string fname = Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath(Path.Combine("~/MediaSharing/uploads/", fname)));
            }
        }

        private void sendCategory_Click(object sender, EventArgs e)
        {
            string naam = cat.Value;
            string parent = Request["parentCat"];
            db.sendCategory(naam, Convert.ToInt32(parent), Session["login"].ToString());
        }

        [System.Web.Services.WebMethod]
        public static bool Like(int id)
        {
            MSDatabase db = new MSDatabase();
            db.addLike(id, HttpContext.Current.Session["login"].ToString());

            return true;
        }

        [System.Web.Services.WebMethod]
        public static bool Report(int id)
        {
            return false;
        }

        [System.Web.Services.WebMethod]
        public static bool React(int id)
        {
            return false;
        }

    }
}