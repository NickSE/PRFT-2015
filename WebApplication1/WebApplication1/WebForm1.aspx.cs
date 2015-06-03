using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnInlog.ServerClick += btnInlog_ServerClick; 
            
        }

        void btnInlog_ServerClick(object sender, EventArgs e)
        {           
            //nieuwe db
            Database db = new Database();
            //wachtwoord en gebruikersnaam naar string zetten
            string gebruikersnaam = Request["username"];
            string wachtwoord = Convert.ToString(password.Value);
            //inlog check
            if (db.logIn(gebruikersnaam, wachtwoord))
            {
                JeMoeder.InnerHtml = "Je bent ingelogd!";
            }
            else
            {
                JeMoeder.InnerHtml = "Je bent niet aangelogd!";
            }
        }
    }
}
