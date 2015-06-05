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
            
           /* btnInlog.ServerClick += btnInlog_ServerClick; */
            
        }

        void btnInlog_ServerClick(object sender, EventArgs e)
        {           
            //nieuwe db
            
            //wachtwoord en gebruikersnaam naar string zetten
            //2 manieren om gegevens op te halen Request en .value
            //string gebruikersnaam = Request["username"];
            //string wachtwoord = Convert.ToString(password.Value);
            //inlog check
            /*if (db.logIn(gebruikersnaam, wachtwoord))
            {
                //JeMoeder.InnerHtml = "Je bent ingelogd!";
            }
            else
            {
                //JeMoeder.InnerHtml = "Je bent niet aangelogd!";
            }*/
        }
    }
}
