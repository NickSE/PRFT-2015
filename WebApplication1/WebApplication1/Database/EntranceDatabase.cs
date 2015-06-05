using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace ICT4Events_Group1
{
    class EntranceDatabase : Database
    {
        //fields
        //propperties
        //constructor
        //methodes
        public bool CheckReservarion(Reservation reservation)
        {
            return true;
        }
        public List<Dictionary<string, object>> GenerateList()
        {
            List<Dictionary<string, object>> data = getQuery("SELECT voornaam, tussenvoegsel, achternaam FROM gebruiker WHERE rfid IS NOT null");
            
            return data;
        }
        public List<Dictionary<string, object>> GetContact(int id)
        {
            
            
              List< Dictionary<string, object>> data = getQuery("SELECT voornaam, tussenvoegsel, achternaam, woonplaats, foto, paspoort, betaald FROM gebruiker WHERE id = " + id);

                return data;
           
            
          
        }

        
        public bool PaymentCheck(string code)
        {
            
            return false;
            
        }
        public int activateCode(int rfid)
        {
            try
            {
                List<Dictionary<string, object>> data = getQuery("SELECT avtivate FROM gebruikers WHERE rfid = '" + rfid+ "'");
                if (Convert.ToString(data) == "0")
                {
                    doQuery("UPDATE gebruiker SET activate = 1 WHERE rfid = '" + rfid + "'");
                    return (2);
                }
                else
                {
                    return (1);
                }


            }
            catch
            {
                return (0);
            }
        }


               
        public int diActivateCode(string rfid)
        {
            try
            {
                 List<Dictionary<string, object>> data = getQuery("SELECT rfid FROM gebruiker WHERE rfid = '" + rfid+ "'");
                 if (data.Count != 0)
               {
                   doQuery("UPDATE gebruiker SET rfid = '' WHERE rfid = '" + rfid + "'");
                   return (2);
               }
               else
               {
                   return (1);
               }
            
                  
                }
                catch
                {
                    return (0);
                }


      
            
        }
        public bool GetCode(string code)
        {
           List<Dictionary<string, object>> data = getQuery("SELECT id FROM gebruiker WHERE rfid = '"+ code+ "'");

            if ( data.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Getid(string id)
        {
            List<Dictionary<string, object>> data = getQuery("SELECT rfid FROM gebruiker WHERE id = '" + id + "'");

            if (data == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       public bool activateCode(int id, string rfid)
        {
            try
            {
                doQuery("INSERT INTO rfid VALUES('"+ rfid +"')");
                doQuery("UPDATE gebruiker SET rfid = '" + rfid + "'  WHERE id = " + id);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
