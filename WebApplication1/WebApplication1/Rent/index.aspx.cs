using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.DB;
using WebApplication1.Model;


namespace WebApplication1.Rent
{
    public partial class index : System.Web.UI.Page
    {
        ADatabase adb = new ADatabase();
        protected void Page_Load(object sender, EventArgs e)
        {   
            //laad catagorieën
            loadCategories();            
            lbHuur.SelectedIndexChanged += lbHuur_SelectedIndexChanged;
            btnZoek.Click += btnZoek_Click;
            btnHuur.Click += btnHuur_Click;
        }

        void btnHuur_Click(object sender, EventArgs e)
        {
            try
            {
                //als er geen barcode is ingevuld (dus via listbox)
                if (tbBarcode.Text == "")
                {
                    List<Dictionary<string, object>> data = adb.huurItem(lbHuur.SelectedValue);
                    Dictionary<string, object> cur = data[0];
                    string item = (string)cur["serie"];
                    string merk = (string)cur["merk"];
                    string prijs = Convert.ToString(cur["prijs"]) + " euro";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Weet u zeker dat u het product: " + item + " van het merk: " + merk + " wilt huren voor: " + prijs + "?')", true);
                }
                //als er wel een barcode is ingevuld
                else if (tbBarcode.Text != "")
                {
                    List<Dictionary<string, object>> dataHuur = adb.huurItemBarcode(tbBarcode.Text);
                    Dictionary<string, object> curr = dataHuur[0];
                    string itemB = (string)curr["serie"];
                    string merkB = (string)curr["merk"];
                    string prijsB = Convert.ToString(curr["prijs"]) + " euro";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Weet u zeker dat u het product: " + itemB + " van het merk: " + merkB + " wilt huren voor: " + prijsB + "?')", true);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ongeldige informatie!')", true);
            }
        }

        void lbHuur_SelectedIndexChanged(object sender, EventArgs e)
        {
            //zet item in een string
            string item = lbHuur.SelectedItem.Text;
            if (adb.getSubCategories(item) != null)
            {
                lbHuur.Items.Clear();
                //haal subcategorie op
                foreach (Category c in adb.getSubCategories(item))
                {
                    lbHuur.Items.Add(new ListItem(c.Name));
                }
            }
            else
            {
                //do nothing
            }
             
        }

        void lbHuur_DoubleClick(object sender, EventArgs e)
        {

        }

        private void loadCategories()
        {
            foreach (Category c in adb.getCategories())
            {
                lbHuur.Items.Add(new ListItem(c.Name));
            }
        }

        void btnZoek_Click(object sender, EventArgs e)
        {
            string searched = tbZoek.Text;
        }
    }
}