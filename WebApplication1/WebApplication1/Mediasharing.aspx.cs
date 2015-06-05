using System;
using System.Collections.Generic;
using System.Linq;
//using Oracle.DataAccess.Type;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Mediasharing : System.Web.UI.Page
    {
        Database mediadb = new Database();
        //hier moet een list komen van messages List<Messages> berichten = new List<Messages>
        protected void Page_Load(object sender, EventArgs e)
        {
            btnPost.ServerClick += btnPost_ServerClick;
            //hier moet dan de GetQuery komen om de posts uit de database te halen.
            media.InnerText = "Hier komen dan de resultaten van de GetQuery";
        }

        void btnPost_ServerClick(object sender, EventArgs e)
        {
            string text = Request["Comment"];
            //add text ook aan List<Messages>
            media.InnerText = text;
            //DoQuery ("insert into bericht(bijdrage_id, titel, inhoud) values(" + id + "," + titel + "," + text "))
        }
    }
}