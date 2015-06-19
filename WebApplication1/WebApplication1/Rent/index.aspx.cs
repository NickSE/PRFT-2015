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
            loadCategories();            
            lbHuur.SelectedIndexChanged += lbHuur_SelectedIndexChanged;
            btnZoek.Click += btnZoek_Click;
        }

        void lbHuur_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = lbHuur.SelectedItem.Text;
            lbHuur.Items.Clear();
            foreach (Category c in adb.getSubCategories(item))
            {
                lbHuur.Items.Add(new ListItem(c.Name));
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