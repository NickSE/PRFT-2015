using System;
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
            btnLink.Click += btnLink_Click;    
        }

        void btnLink_Click(object sender, EventArgs e)
        {
            if (tbBarcode.Text == "")
            {
                //barcode niet gevonden
            }
            else
            {
                try
                {
                    int ID = Convert.ToInt32(tbID.Text);
                    List<Dictionary<string, object>> data = adb.getAccount(ID);
                    Dictionary<string, object> cur = data[0];
                    string naam = (string)cur["voornaam"] + (string)cur["achternaam"];
                    string email = (string)cur["email"];
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    var random = new Random();
                    var result = new string(
                                 Enumerable.Repeat(chars, 8)
                                 .Select(s => s[random.Next(s.Length)])
                                 .ToArray());
                    string actievatiehash = (string)result;
                    adb.createAccount(naam, email, actievatiehash);
                    if (adb.GetCode(Convert.ToString(tbBarcode.Text)))
                    {
                        bool resultaat = adb.activateCode(Convert.ToInt32(tbID.Text), tbBarcode.Text);
                        if (resultaat)
                        {
                            //linken gelukt!
                        }
                        else
                        {
                            //linken niet gelukt :c
                        }
                    }
                    else
                    {
                        //barcode is al gelinkt! (Deactiveren)
                        adb.deActivateCode(tbBarcode.Text);
                    }
                }
                catch
                {
                    //ongeldige data!
                }
            }
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

        protected void btnAanwezig_Click(object sender, EventArgs e)
        {
            adb.getAllEntries();/*Aanwezige ophalen en in de listbox zetten lbPresent*/
        }
    }
}