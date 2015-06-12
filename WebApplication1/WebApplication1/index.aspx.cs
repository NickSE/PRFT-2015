using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.DB;

namespace WebApplication1.Scherm
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Database db = new Database();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            btnInlog.ServerClick += btnInlog_ServerClick; 
            
        }

        void btnInlog_ServerClick(object sender, EventArgs e)
        {           
            //nieuwe db
            
            //gebruikersnaam naar string zetten
            string gebruikersnaam = Request["username"];
            //inlog check
            if (db.logIn(gebruikersnaam))
            {
                Session["login"] = gebruikersnaam;
                Response.Redirect("/MediaSharing");
            }
            else
            {
                Session.Clear();
                error.InnerText = "Niet ingelogd. Gebruikersnaam niet gevonden.";
                username.Value = gebruikersnaam; 
            }
        }
    }
}
