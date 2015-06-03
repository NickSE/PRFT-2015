using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Mediasharing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Database mediadb = new Database();
            //hier moet dan de GetQuery komen om de posts uit de database te halen.
            media.InnerText = "Hier komen dan de resultaten van de GetQuery";
        }
    }
}