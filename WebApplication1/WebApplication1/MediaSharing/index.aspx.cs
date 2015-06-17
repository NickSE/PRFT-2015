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
                select.Items.Add(new ListItem("Geen...", "-1"));

            foreach (Category c in db.getCategories())
            {
                select.Items.Add(new ListItem(c.Name, c.id.ToString()));
            }
        }

        private void loadArticles() {
            article_list.InnerHtml = "";
            string type;
            // Load messages
            int account_id = db.getSessionId(Session["login"].ToString());
            foreach (Contribution i in db.getContributions(account_id, ""))
            {
                if (i is Message) type = "message";
                else if (i is UserFile) type = "file";
                else type = "category";

                // Start artikel
                article_list.InnerHtml += "<article id=\"contribution_"+i.id+"\">" + "\n";

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
                if(i.Liked)
                    article_list.InnerHtml += "<button class=\"like btn btn-link\" id=\"like_" + i.id + "\" onclick=\"Unlike(" + i.id + ")\">Unlike</button>";
                else
                    article_list.InnerHtml += "<button class=\"like btn btn-link\" id=\"like_" + i.id + "\" onclick=\"Like(" + i.id + ")\">Like</button>";
                article_list.InnerHtml += "<button class=\"react btn btn-link\" id=\"react_" + i.id + "\" onclick=\"openReact(" + i.id + ")\">Reageer</button>";
                if(i.Reported)
                    article_list.InnerHtml += "<button class=\"report btn btn-link\" id=\"report_" + i.id + "\" disabled=\"disabled\">Ongewenst</button>";
                else
                    article_list.InnerHtml += "<button class=\"report btn btn-link\" id=\"report_" + i.id + "\" onclick=\"Report(" + i.id + ")\">Ongewenst</button>";
                article_list.InnerHtml += "</div>" + "\n";

                // Geef artikel aantal likes
                article_list.InnerHtml += "<div class=\"likes\">";
                article_list.InnerHtml += "<b>" + i.Likes + "</b> like(s)";
                article_list.InnerHtml += "</div>" + "\n";

                // Laadt reacties
                article_list.InnerHtml += "<div id=\"commentfield_" + i.id + "\" class=\"hidden\">";
                    article_list.InnerHtml += "<div id=\"comments\">";
                    foreach (Message j in db.getReaction(i))
                    {
                        article_list.InnerHtml += "<div id=\"comment_" + j.id + "\">";
                        article_list.InnerHtml += "<div class=\"author\">" + j.Author + "</div>";
                        article_list.InnerHtml += "<div class=\"content\">" + j.Content + "</div>";
                        article_list.InnerHtml += "<div class=\"time\">" + j.Date + "</div>";
                        article_list.InnerHtml += "</div>";
                    }
                    article_list.InnerHtml += "</div>";
                    // Reageer
                    article_list.InnerHtml += "<div id=\"commenting_" + i.id + "\">";
                        article_list.InnerHtml += "<div class=\"form-group\">";
                        article_list.InnerHtml += "<label for=\"react\"> Reageer </label>";
                        article_list.InnerHtml += "<input type=\"text\" name=\"react\" id=\"comment_content_"+i.id+"\" class=\"form-control\" />";
                        article_list.InnerHtml += "</div>";
                        article_list.InnerHtml += "<input class=\"btn btn-primary btn-lg btn-block\" type=\"submit\" value=\"Verstuur\" onclick=\"React(" + i.id + ")\" />";
                    article_list.InnerHtml += "</div>";
                article_list.InnerHtml += "</div>";

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
                db.sendFile(fname, file.ContentLength, Convert.ToInt32(Request["fileCat"]), Session["login"].ToString());
                loadArticles();
            }
        }

        private void sendCategory_Click(object sender, EventArgs e)
        {
            string naam = cat.Value;
            string parent = Request["parentCat"];
            db.sendCategory(naam, Convert.ToInt32(parent), Session["login"].ToString());
            loadArticles();
        }

        [System.Web.Services.WebMethod]
        public static bool Like(int id)
        {
            MSDatabase db = new MSDatabase();
            return db.doAction(id, HttpContext.Current.Session["login"].ToString(), "LIKE");
        }

        [System.Web.Services.WebMethod]
        public static bool Unlike(int id)
        {
            MSDatabase db = new MSDatabase();
            return db.doAction(id, HttpContext.Current.Session["login"].ToString(), "UNLIKE");
        }

        [System.Web.Services.WebMethod]
        public static bool Report(int id)
        {
            MSDatabase db = new MSDatabase();
            return db.doAction(id, HttpContext.Current.Session["login"].ToString(), "REPORT");
        }

        [System.Web.Services.WebMethod]
        public static void React(int id, string message)
        {
            MSDatabase db = new MSDatabase();
            db.sendReaction(id, message, HttpContext.Current.Session["login"].ToString());
        }

        [System.Web.Services.WebMethod]
        public static string Sort(string id)
        {
            string html = "";
            MSDatabase db = new MSDatabase();
            string type;
            // Load messages
            int account_id = db.getSessionId(HttpContext.Current.Session["login"].ToString());
            foreach (Contribution i in db.getContributions(account_id, id))
            {
                if (i is Message) type = "message";
                else if (i is UserFile) type = "file";
                else type = "category";

                // Start artikel
                html += "<article id=\"contribution_" + i.id + "\">" + "\n";

                // Display author
                html += "<div class=\"author\">";
                html += "<h2>" + i.Author + "</h2>";
                html += "<h5>" + i.Date + "</h5>";
                html += "</div>" + "\n";

                html += "<div class=\"content " + type + "\">";
                // Content
                if (i is Message)
                {
                    html += "<h6><b>" + ((Message)i).Title + "</b></h6>";
                    html += "<p>" + ((Message)i).Content + "</p>";
                }
                else
                    if (i is UserFile)
                    {
                        switch (((UserFile)i).getType())
                        {
                            case "jpg":
                            case "png":
                            case "gif":
                                html += "<img src=\"" + ((UserFile)i).Path + "\" class=\"img-responsive\"/>";
                                break;
                            case "mp3":
                            case "ogg":
                            case "wav":
                                html += "<audio controls src=\"" + ((UserFile)i).Path + "\"> ";
                                html += "Je browser ondersteunt geen audio element.";
                                html += "</audio>";
                                break;
                            case "mp4":
                                html += "<video controls src=\"" + ((UserFile)i).Path + "\"> ";
                                html += "Je browser ondersteunt geen video element.";
                                html += "</video>";
                                break;
                            default:
                                html += "Bestandstype \"" + ((UserFile)i).getType() + "\" kan niet worden geladen.";
                                break;
                        }
                        html += "<br /><a href=\"" + ((UserFile)i).Path + "\" target=\"_blanc\"> Klik hier om het bestand te downloaden </a>";
                    }
                    else
                        if (i is Category)
                        {
                            html += "<b>" + i.Author + "</b> heeft <b>" + ((Category)i).Name + "</b> toegevoegd";
                            if (((Category)i).Parent != null)
                                html += " aan <b>" + ((Category)i).Parent.Name + "</b>";
                        }

                html += "</div>" + "\n";

                // Geef artikel een Like, Reageer en report functie
                html += "<div class=\"do text-right\">";
                if (i.Liked)
                    html += "<button class=\"like btn btn-link\" id=\"like_" + i.id + "\" onclick=\"Unlike(" + i.id + ")\">Unlike</button>";
                else
                    html += "<button class=\"like btn btn-link\" id=\"like_" + i.id + "\" onclick=\"Like(" + i.id + ")\">Like</button>";
                html += "<button class=\"react btn btn-link\" id=\"react_" + i.id + "\" onclick=\"openReact(" + i.id + ")\">Reageer</button>";
                html += "<button class=\"report btn btn-link\" id=\"report_" + i.id + "\" onclick=\"Report(" + i.id + ")\">Ongewenst</button>";
                html += "</div>" + "\n";

                // Geef artikel aantal likes
                html += "<div class=\"likes\">";
                html += "<b>" + i.Likes + "</b> like(s)";
                html += "</div>" + "\n";

                // Laadt reacties
                // ...

                // Sluit artikel
                html += "</article>" + "\n";
            }
            return html;
        }

    }
}