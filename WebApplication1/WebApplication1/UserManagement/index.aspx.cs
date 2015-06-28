using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebApplication1.DB;
using WebApplication1.Model;
using System.Net.Mail;

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
                string email = tbEmailUser.Text;

                DateTime startdate = startdateRes.SelectedDate;
                DateTime enddate = enddateRes.SelectedDate;
                string payed;
                if (cbPayedRes.Checked) { payed = "1"; }
                else { payed = "0"; }

                int id = db.getLatestId("persoon");
                //Generate random hash.
                string hash = "Random";
                User newUser = new User(id, name, insertion, lastname, street, number, city, banknr, email);
                Reservation newReservation = new Reservation(db.getLatestId("reservering"), id, startdate, enddate, payed);
                // id, gebruikersnaam, email, hash, actief
                Account newAccount = new Account(db.getLatestId("account"), name + id, email, hash, "1");
                if (udb.AddUser(newUser) && udb.AddRes(newReservation) && udb.AddAccount(newAccount))
                {
                    try
                    {
                        //Sending email for activation code
                        SendMail(email, hash, Convert.ToString(name + id));
                        //Response.Write("E-mail sent!"); FEEDBACK
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Could not send the e-mail - error: " + ex.Message);
                    }
                    finally
                    {
                        //feedback
                        RefreshUserList();
                    }
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
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + selectedUser.name + " " + selectedUser.insertion + " " + selectedUser.lastname + " " + selectedUser.street + " " + selectedUser.number + " " + selectedUser.city + " " + selectedUser.banknr + " " + selectedUser.email +"');", true);
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
        private void SendMail(string email, string hash, string username)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(email); //Reciever
            mailMessage.From = new MailAddress("another@mail-address.com"); //Sender
            mailMessage.Subject = "Account verification " + email;
            mailMessage.Body = "Verification mail for " + email + ". Your activation hash is " + hash + ", use this to acctivate your account. Username is " + username;
            SmtpClient smtpClient = new SmtpClient("smtp.your-isp.com"); //smtp from server
            smtpClient.Send(mailMessage);
        }
    }
}