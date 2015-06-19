using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.DB;


namespace WebApplication1.EventManagement
{
    public partial class index : System.Web.UI.Page
    {
        Database db = new Database();
        EMDatabase edb = new EMDatabase();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                RefreshEvents();
                RefreshLocaties();
                Refreshplek();
                RefreshAllDropdown();
                Refreshspecification();
            }
        }

        protected void btnCreateLocation_Click(object sender, EventArgs e)
        {
            try
            {
                string naam = tbNameLocation.Text;
                string straat = tbStreetLocation.Text;
                string nr = tbNrLocation.Text;
                string postcode = tbPostcodeLocation.Text;
                string plaats = tbCityLocation.Text;

                Locatie newlocatie = new Locatie(db.getLatestId("locatie"), naam, straat, nr, postcode, plaats);
                if (edb.AddLocation(newlocatie))
                {
                    RefreshAllDropdown();
                        RefreshLocaties();
                }
                else
                { //niet ok
                }
            }
            catch { 
            // werkt niet
            }



        }

        protected void btnCreateEvent_Click(object sender, EventArgs e)
        {
            try
            {
                int location_id = Convert.ToInt32(dbLocationEvent.SelectedItem.Value);
                string name = tbNameEvent.Text;
                DateTime start = dtpDateStartEvent.SelectedDate;
                DateTime end = dtpDateEndEvent.SelectedDate;
                int max = Convert.ToInt32(tbMaxEvent.Text);

                Events newEvent = new Events(db.getLatestId("event"), name, start, end, max, location_id);
                if(edb.AddEvent(newEvent))
                {
                    RefreshEvents();
                }
                else
                {
                    //wrkt niet
                }
            }
            catch
            {
                //shit didnt work
            }
        }

        protected void btnCreatePlek_Click(object sender, EventArgs e)
        {
            try
            {
                string nr = tbNrPlek.Text;
                int capa = Convert.ToInt32(tbCapacityPlek.Text);
                int location_id = Convert.ToInt32(dbLocationPlek.SelectedValue);
                int id = db.getLatestId("plek");
                Plek newplek = new Plek(id, nr, capa, location_id);
                edb.AddPlek(newplek);
                // add specification --------------------------------------------------------------------------------------
                foreach (ListItem spec in lbSpecificationPlek.Items)
                {
                    string waarde = spec.Value;
                    string naam = spec.Text;
                    edb.AddSpecification(naam, waarde, id);
                }

                lbSpecificationPlek.Items.Clear();
                Refreshplek();
            }
            catch
            {
                //i cry evry time
            }
            
        }

        protected void btnAddSpecificationPlek_Click(object sender, EventArgs e)
        {
            
            lbSpecificationPlek.Items.Add(new ListItem(dbSpecificationPlek.SelectedItem.Text, tbValuePlek.Text));
        }

        protected void btnRemoveSpecificationPlek_Click(object sender, EventArgs e)
        {
            lbSpecificationPlek.Items.RemoveAt(lbSpecificationPlek.SelectedIndex);
        }

        protected void btnRemoveLocation_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lbLocation.SelectedValue);
            edb.deleteLocation(id);
            RefreshLocaties();
            RefreshAllDropdown();
            
        }

        protected void btnRemoveEvent_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lbEvent.SelectedValue);
            edb.deleteEvent(id);
            RefreshEvents();
        }

        protected void btnRemovePlek_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lbPlek.SelectedValue);
            edb.deleteplek(id);
            Refreshplek();
        }

        private void RefreshEvents()
        {
            lbEvent.Items.Clear();
            foreach (Events events in (edb.GetAllEvents()))
            {
               // lbEvent.Items.Add(events.ToString());
                lbEvent.Items.Add(new ListItem(events.ToString(), Convert.ToString(events.id)));
            }
        }
        private void RefreshLocaties()
        {
            lbLocation.Items.Clear();
            foreach (Locatie locatie in (edb.GetAllLocations()))
            {
             //   lbEvent.Items.Add(locatie.ToString());
                lbLocation.Items.Add(new ListItem(locatie.ToString(), Convert.ToString(locatie.id)));
                
            }
        }

        private void Refreshplek()
        {
            lbPlek.Items.Clear();
            foreach (Plek plek in (edb.GetAllPleks()))
            {
                // lbEvent.Items.Add(plek.ToString());
                lbPlek.Items.Add(new ListItem(plek.ToString(), Convert.ToString(plek.id)));

            }
        }
        private void Refreshspecification()
        {
            lbSpecificationPlek.Items.Clear();
            foreach (Dictionary<string, object> spec in (edb.GetAllSpecs()))
            {
                dbSpecificationPlek.Items.Add(new ListItem(Convert.ToString(spec["naam"]), Convert.ToString(spec["id"])));
            }
        }
        private void RefreshAllDropdown()
        {
            dbLocationEvent.Items.Clear();
            dbLocationPlek.Items.Clear();
            foreach (Locatie locatie in (edb.GetAllLocations()))
            {
                dbLocationEvent.Items.Add(new ListItem(locatie.ToString(), Convert.ToString(locatie.id)));
                dbLocationPlek.Items.Add(new ListItem(locatie.ToString(), Convert.ToString(locatie.id)));
            }

        }
    }
}