﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.DB;
using WebApplication1.Model;

namespace WebApplication1.Entrance
{
    public partial class index : System.Web.UI.Page
    {

        ADatabase adb = new ADatabase();
        protected void Page_Load(object sender, EventArgs e)
        {
            btnZoek.Click += btnZoek_Click;
        }

        void btnZoek_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(tbID.Text);
            List<Dictionary<string, object>> data = adb.getAccount(ID);
            Dictionary<string, object> cur = data[0];
            string naam;
            string adres = (string)cur["straat"] + " " + (string)cur["huisnr"] + ", " + (string)cur["woonplaats"];
            string betaald;            
            naam = (string)cur["voornaam"] + " " + (string)cur["tussenvoegsel"] + " " + (string)cur["achternaam"];
            if (Convert.ToInt32(cur["betaald"]) == 1)
            {
                betaald = "Ja";
            }
            else
            {
                betaald = "Nee";
            }
                    
           
            
            Naam.InnerHtml = "Naam:" + naam;
            Adres.InnerHtml = "Adres:" + adres;
            Betaald.InnerHtml = "Betaald:" + betaald;
            /*profiel.InnerHtml += "<div class=\"profiel\">";
            profiel.InnerHtml += "<label for=\"pnlProfiel\">Profiel</label>";
            profiel.InnerHtml += "<asp:Panel ID=\"pnlProfiel\" runat=\"server\">";
            //profiel.InnerHtml += "<p>" + "Naam:" + naam + "</p>";
            profiel.InnerHtml += "</asp:Panel>";
            profiel.InnerHtml += "</div>" + "\n";*/
        }
    }
}