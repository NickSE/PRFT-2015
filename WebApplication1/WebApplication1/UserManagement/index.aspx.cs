using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebApplication1.DB;

namespace WebApplication1.UserManagement
{
    
    public partial class index : System.Web.UI.Page
    {
        Database db = new Database();
        UMDatabase udb = new UMDatabase();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshUserList();
                lbUser.DataBind();
            }
        }
        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            try
            {
                string name = tbNameUser.Text;
                string insertion = tbInsertionUser.Text;
                string lastname = tbLastnameUser.Text;
                string street = tbStreetUser.Text;
                string number = tbNumberUser.Text;
                string city = tbCityUser.Text;
                string banknr = tbBanknrUser.Text;
                User newUser = new User(db.getLatestId("persoon"), name, insertion, lastname, street, number, city, banknr);

                if (udb.AddUser(newUser))
                {
                    //feedback
                    RefreshUserList();
                }
                else
                {

                }
            }
            catch
            {

            }
        }
        protected void btnRemoveUser_Click(object sender, EventArgs e)
        {
            //int id = Convert.ToInt32(lbUser.SelectedValue);

            if (lbUser.SelectedItem.Value != null)
            {
                if(udb.DeleteUser(Convert.ToInt32(lbUser.SelectedItem.Value)))
                {
                    btnRemoveUser.Text = "Jobs done!";
                }
                RefreshUserList();
            }
                
            

        }
        protected void btnInfoUser_Click(object sender, EventArgs e)
        {
            if (lbUser.SelectedItem.Value != null)
            {
                User selectedUser = udb.GetUser(Convert.ToInt32(lbUser.SelectedItem.Value));
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + selectedUser.name + " " + selectedUser.insertion + " " + selectedUser.lastname + " " + selectedUser.street + " " + selectedUser.number + " " + selectedUser.city + " " + selectedUser.banknr + "');", true);
            }
        }
        private void RefreshUserList()
        {
            lbUser.Items.Clear();
            foreach (User user in (udb.GetAllUsers()))
            {
                lbUser.Items.Add(new ListItem(user.ToString(), Convert.ToString(user.id)));
            }
        }
    }
}