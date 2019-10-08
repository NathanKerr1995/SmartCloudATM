using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATM
{
    public partial class MainScreen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //check if user is logged in with session

                #region Get user balance with API
                try
                {
                    //POST
                    string strURLTest = String.Format("https://frontend-challenge.screencloud-michael.now.sh/api/pin/");
                    WebRequest requestObject = WebRequest.Create(strURLTest);
                    requestObject.Method = "POST";
                    requestObject.ContentType = "application/json";
                    requestObject.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    requestObject.UseDefaultCredentials = true;

                    string postData = "{\"currentBalance\":\"testbody\",\"pin\":\"" + Convert.ToInt32(Session["UsersPepper"]) + "\"}";

                    using (var streamWriter = new StreamWriter(requestObject.GetRequestStream()))
                    {
                        streamWriter.Write(postData);
                        streamWriter.Flush();
                        streamWriter.Close();

                        var httpResponse = (HttpWebResponse)requestObject.GetResponse();

                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result2 = streamReader.ReadToEnd();

                            //get only the numbers for users balance
                            //string strUsersBalance = string.Empty;
                            //strUsersBalance = Regex.Match(result2, @"\d+").Value;
                            //lblBalance.Text = "Balance: £" + strUsersBalance;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("Index.aspx");
                }
                #endregion

                #region Get user balance with Db
                Connections cnnUserBalance = new Connections();
                cnnUserBalance.sql_connection();
                Connections.con.Open();

                SqlCommand cmdUserBalance = new SqlCommand("SELECT * FROM tblUserDetails WHERE Pin = @UsersPepper AND Status = '1'", Connections.con);
                cmdUserBalance.Parameters.AddWithValue("@UsersPepper", Convert.ToInt32(Session["UsersPepper"]));

                using (SqlDataReader sdrUserBalance = cmdUserBalance.ExecuteReader())
                {
                    if (sdrUserBalance.HasRows)
                    {
                        while (sdrUserBalance.Read())
                        {
                            lblBalance.Text = "Balance: £" + Convert.ToInt32(sdrUserBalance["Userbalance"]);
                        }
                    }
                }
                Connections.con.Close();
                #endregion

                //check if button for note amount is visible
                //intially disable 50 and 100 until further checks
                hfReceipt100.Value = "0";
                hfReceipt50.Value = "0";

                Connections cnnCheckNotes = new Connections();
                cnnCheckNotes.sql_connection();
                Connections.con.Open();

                //checking if 100 button can be enabled
                #region check total ATM 
                SqlCommand comATMTotal = new SqlCommand();
                comATMTotal.Connection = Connections.con;
                comATMTotal.CommandText = "SELECT SUM(CurrencyAmount * CurrencyNoteTotal) FROM tblCurrency WHERE Status = 1";
                Int32 intTotal = (Int32)comATMTotal.ExecuteScalar();

                if (intTotal >= 100)
                {
                    hfReceipt100.Value = "1";
                }
                #endregion

                //checking if 20 button can be enabled
                #region check 20 total
                using (SqlDataReader sdrCheckNotes20 = new SqlCommand("SELECT * FROM tblCurrency WHERE CurrencyAmount = 20 AND CurrencyNoteTotal != 0 AND Status = 1", Connections.con).ExecuteReader())
                {
                    if (sdrCheckNotes20.HasRows)
                    {
                        while (sdrCheckNotes20.Read())
                        {
                            hfReceipt20.Value = "1";

                            if (intTotal >= 50 && Convert.ToInt32(sdrCheckNotes20["CurrencyNoteTotal"]) >= 3)
                            {
                                hfReceipt50.Value = "1";
                            }
                        }
                    }
                    else
                    {
                        hfReceipt20.Value = "0";
                    }
                }
                #endregion

                //checking if 10 button can be enabled
                Int32 int10Total = 0;

                #region check 10 total
                using (SqlDataReader sdrCheckNotes10 = new SqlCommand("SELECT * FROM tblCurrency WHERE CurrencyAmount = 10 AND CurrencyNoteTotal != 0 AND Status = 1", Connections.con).ExecuteReader())
                {
                    if (sdrCheckNotes10.HasRows)
                    {
                        while (sdrCheckNotes10.Read())
                        {
                            int10Total = Convert.ToInt32(sdrCheckNotes10["CurrencyNoteTotal"]);
                            hfReceipt10.Value = "1";

                            if (intTotal >= 50 && Convert.ToInt32(sdrCheckNotes10["CurrencyNoteTotal"]) >= 1)
                            {
                                hfReceipt50.Value = "1";
                            }

                            if (Convert.ToInt32(sdrCheckNotes10["CurrencyNoteTotal"]) >= 2)
                            {
                                hfReceipt20.Value = "1";
                            }
                        }
                    }
                    else
                    {
                        hfReceipt20.Value = "0";
                    }
                }
                #endregion

                //checking if 5 button can be enabled
                #region check 5 total
                using (SqlDataReader sdrCheckNotes5 = new SqlCommand("SELECT * FROM tblCurrency WHERE CurrencyAmount = 5 AND CurrencyNoteTotal != 0 AND Status = 1", Connections.con).ExecuteReader())
                {
                    if (sdrCheckNotes5.HasRows)
                    {
                        while (sdrCheckNotes5.Read())
                        {
                            hfReceipt5.Value = "1";

                            if (intTotal >= 50 && Convert.ToInt32(sdrCheckNotes5["CurrencyNoteTotal"]) >= 2)
                            {
                                hfReceipt50.Value = "1";
                            }

                            if (Convert.ToInt32(sdrCheckNotes5["CurrencyNoteTotal"]) >= 4 || (Convert.ToInt32(sdrCheckNotes5["CurrencyNoteTotal"]) >= 2 && int10Total >= 1))
                            {
                                hfReceipt20.Value = "1";
                            }

                            if (Convert.ToInt32(sdrCheckNotes5["CurrencyNoteTotal"]) >= 2)
                            {
                                hfReceipt10.Value = "1";
                            }
                        }
                    }
                    else
                    {
                        hfReceipt5.Value = "0";
                    }
                }
                #endregion

                Connections.con.Close();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void lnbtSpeak_Click(object sender, EventArgs e)
        {
            // Initialize a new instance of the SpeechSynthesizer.
            SpeechSynthesizer synth = new SpeechSynthesizer();

            // Configure the audio output.   
            synth.SetOutputToDefaultAudioDevice();

            // Speak a string.  
            synth.Speak("This page allows you to check your balance and withdraw cash. The check balance button is located on the left while the withdraw button is located in the middle.");
            synth.Dispose();
        }

        protected void btnEmailBalance_Click(object sender, EventArgs e)
        {
            //get users details based on pin
            Connections cnnUser = new Connections();
            cnnUser.sql_connection();
            Connections.con.Open();

            SqlCommand cmdUser = new SqlCommand("SELECT * FROM tblUserDetails WHERE Pin = @UsersPepper AND Status = '1'", Connections.con);
            cmdUser.Parameters.AddWithValue("@UsersPepper", Convert.ToInt32(Session["UsersPepper"]));

            using (SqlDataReader sdrUser = cmdUser.ExecuteReader())
            {
                if (sdrUser.HasRows)
                {
                    while (sdrUser.Read())
                    {
                        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                        mail.To.Add(Convert.ToString(sdrUser["UserEmail"]));
                        mail.From = new MailAddress("kerrwebdesign@gmail.com", "SmartCloud ATM", System.Text.Encoding.UTF8);
                        mail.Subject = "Account Balance";
                        mail.SubjectEncoding = System.Text.Encoding.UTF8;
                        mail.Body = "<h4>Hello " + Convert.ToString(sdrUser["UserForename"]) + " " + Convert.ToString(sdrUser["UserSurname"]) + ",</h4>"
                                + "<b>This is an automated email from a ATM request to view you account balance. Please do not reply to this email.</b> <br /><br />"
                                + Convert.ToString(sdrUser["UserForename"]) + ", here is your account balance: £" + Convert.ToString(sdrUser["UserBalance"]) + "<br /><br />"
                                + "Kind Regards,"
                                + "<br />" +
                                "Smart Cloud ATM";
                        mail.BodyEncoding = System.Text.Encoding.UTF8;
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.High;
                        SmtpClient client = new SmtpClient();
                        client.Credentials = new System.Net.NetworkCredential("animalshelterqubtest@gmail.com", "AnimalShelter10");
                        client.Port = 587;
                        client.Host = "smtp.gmail.com";
                        client.EnableSsl = true;
                        try
                        {
                            //successfully send email
                            client.Send(mail);
                            lblModalTitle.Text = "Account balance sent";
                            lblModalMessage.Text = "An message with your account balance has been to your email.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                        catch
                        {
                            //error page
                            lblModalTitle.Text = "Email error";
                            lblModalMessage.Text = "Sorry but we are unable to send emails at this time.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                    }
                }
            }
            Connections.con.Close();
        }

        protected void btnYesRecipt_Click(object sender, EventArgs e)
        {
            //withdraw with receipt
            Withdraw(1);
        }

        protected void btnNoRecipt_Click(object sender, EventArgs e)
        {
            //withdraw without receipt
            Withdraw(0);
        }

        void Withdraw(int EmailFlag)
        {
            //get users balance from database rather than api
            Connections cnnCheckBalance = new Connections();
            cnnCheckBalance.sql_connection();
            Connections.con.Open();

            SqlCommand cmdCheckBalance = new SqlCommand("SELECT * FROM tblUserDetails WHERE Pin = @UsersPepper AND Status = '1'", Connections.con);
            cmdCheckBalance.Parameters.AddWithValue("@UsersPepper", Convert.ToInt32(Session["UsersPepper"]));

            using (SqlDataReader sdrCheckBalance = cmdCheckBalance.ExecuteReader())
            {
                if (sdrCheckBalance.HasRows)
                {
                    while (sdrCheckBalance.Read())
                    {
                        Int32 intBalance = Convert.ToInt32(sdrCheckBalance["UserBalance"]);
                        Int32 intWithdraw = Convert.ToInt32(hfOtherAmount.Value);
                        Int32 intWithdrawMax = intBalance + 100;
                        Int32 intCount = 0;
                        Int32 intNewWithdraw = intWithdraw;

                        if (intWithdrawMax >= intWithdraw)
                        {
                            //check what notes are available 
                            using (SqlDataReader sdrCheckNotes = new SqlCommand("SELECT TOP 1 * FROM tblCurrency WHERE Status = 1 And CurrencyNoteTotal != 0 Order by CurrencyAmount ASC", Connections.con).ExecuteReader())
                            {
                                if (sdrCheckNotes.HasRows)
                                {
                                    while (sdrCheckNotes.Read())
                                    {
                                        //checks the amount can divide to make the withdraw number
                                        if (intWithdraw % Convert.ToInt32(sdrCheckNotes["CurrencyAmount"]) == 0)
                                        {
                                            //check atm total is more than withdraw
                                            SqlCommand comATMTotal = new SqlCommand();
                                            comATMTotal.Connection = Connections.con;
                                            comATMTotal.CommandText = "SELECT SUM(CurrencyAmount * CurrencyNoteTotal) FROM tblCurrency WHERE Status = 1";
                                            Int32 intATMTotal = (Int32)comATMTotal.ExecuteScalar();

                                            if (intATMTotal >= intWithdraw)
                                            {
                                                while (intCount != intWithdraw)
                                                {
                                                    //start building the count to reach the withdraw
                                                    using (SqlDataReader sdrCount = new SqlCommand("SELECT * FROM tblCurrency WHERE [Status] = 1 And CurrencyNoteTotal != 0 ORDER BY CurrencyNoteTotal DESC, CurrencyAmount DESC", Connections.con).ExecuteReader())
                                                    {
                                                        if (sdrCount.HasRows)
                                                        {
                                                            Int32 intNoteCountTotal = 0;

                                                            while (sdrCount.Read())
                                                            {
                                                                //check the amount can be divided by remaining withdraw
                                                                if (intNewWithdraw % Convert.ToInt32(sdrCount["CurrencyAmount"]) == 0)
                                                                {
                                                                    intNoteCountTotal++;

                                                                    //update db and take away intNotCountTotal from the CurrencyNoteTotal
                                                                    using (SqlCommand cmdRemoveIntCount = new SqlCommand("UPDATE tblCurrency SET CurrencyNoteTotal = CurrencyNoteTotal - 1 WHERE CurrencyAmount = @CurrencyAmount", Connections.con))
                                                                    {
                                                                        cmdRemoveIntCount.Parameters.AddWithValue("@CurrencyAmount", Convert.ToInt32(sdrCount["CurrencyAmount"]));
                                                                        cmdRemoveIntCount.ExecuteNonQuery();
                                                                    }

                                                                    intCount = intCount + (Convert.ToInt32(sdrCount["CurrencyAmount"]) * 1);
                                                                    intNewWithdraw = intNewWithdraw - (Convert.ToInt32(sdrCount["CurrencyAmount"]) * 1);

                                                                    if (intCount == intWithdraw)
                                                                    {
                                                                        //update users balance in db not api
                                                                        using (SqlCommand cmdRemoveUserBalance = new SqlCommand("UPDATE tblUserDetails SET UserBalance = UserBalance - @intCount WHERE Pin = @Pin AND Status = '1'", Connections.con))
                                                                        {
                                                                            cmdRemoveUserBalance.Parameters.AddWithValue("@intCount", intCount);
                                                                            cmdRemoveUserBalance.Parameters.AddWithValue("@Pin", Convert.ToInt32(Session["UsersPepper"]));
                                                                            cmdRemoveUserBalance.ExecuteNonQuery();
                                                                        }

                                                                        //insert into audit table
                                                                        using (SqlCommand cmdAddAudit = new SqlCommand("INSERT INTO tblAudit (UserId, Date, WithdrawAmount) Values (@UserId, @Date, @WithdrawAmount)", Connections.con))
                                                                        {
                                                                            cmdAddAudit.Parameters.AddWithValue("@UserId", Convert.ToInt32(sdrCheckBalance["Id"]));
                                                                            cmdAddAudit.Parameters.AddWithValue("@Date", DateTime.Now);
                                                                            cmdAddAudit.Parameters.AddWithValue("@WithdrawAmount", intCount);
                                                                            cmdAddAudit.ExecuteNonQuery();
                                                                        }

                                                                        #region send email receipt
                                                                        if (EmailFlag == 1)
                                                                        {
                                                                            //send email receipt
                                                                            Connections cnnUser = new Connections();
                                                                            cnnUser.sql_connection();
                                                                            Connections.con.Open();


                                                                            SqlCommand cmdUser = new SqlCommand("SELECT * FROM tblUserDetails WHERE Pin = @UsersPepper AND Status = '1'", Connections.con);
                                                                            cmdUser.Parameters.AddWithValue("@UsersPepper", Convert.ToInt32(Session["UsersPepper"]));

                                                                            using (SqlDataReader sdrUser = cmdUser.ExecuteReader())
                                                                            {
                                                                                if (sdrUser.HasRows)
                                                                                {
                                                                                    while (sdrUser.Read())
                                                                                    {
                                                                                        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                                                        mail.To.Add(Convert.ToString(sdrUser["UserEmail"]));
                                                                                        mail.From = new MailAddress("kerrwebdesign@gmail.com", "SmartCloud ATM", System.Text.Encoding.UTF8);
                                                                                        mail.Subject = "Withdraw Receipt";
                                                                                        mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                                                        mail.Body = "<h4>Hello " + Convert.ToString(sdrUser["UserForename"]) + " " + Convert.ToString(sdrUser["UserSurname"]) + ",</h4>"
                                                                                                + "<b>This is an automated email from a ATM receipt request from a recent Withdrawal. Please do not reply to this email.</b> <br /><br />"
                                                                                                + Convert.ToString(sdrUser["UserForename"]) + ", here is your Withdrawal amount: £" + intCount + ". On the " + Convert.ToString(DateTime.Now.ToShortDateString()) + " at " + Convert.ToString(DateTime.Now.ToShortTimeString()) + "<br /><br />"
                                                                                                + "Kind Regards,"
                                                                                                + "<br />" +
                                                                                                "Smart Cloud ATM";
                                                                                        mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                                                        mail.IsBodyHtml = true;
                                                                                        mail.Priority = MailPriority.High;
                                                                                        SmtpClient client = new SmtpClient();
                                                                                        client.Credentials = new System.Net.NetworkCredential("animalshelterqubtest@gmail.com", "AnimalShelter10");
                                                                                        client.Port = 587;
                                                                                        client.Host = "smtp.gmail.com";
                                                                                        client.EnableSsl = true;
                                                                                        try
                                                                                        {
                                                                                            //successfully send email
                                                                                            client.Send(mail);
                                                                                        }
                                                                                        catch
                                                                                        {
                                                                                            //error page
                                                                                            lblModalTitle.Text = "Email error";
                                                                                            lblModalMessage.Text = "Sorry but we are unable to send emails at this time.";
                                                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                            Connections.con.Close();
                                                                        }
                                                                        #endregion

                                                                        //Ask user to take their money
                                                                        lblModalTimeoutTitle.Text = "Please take your money";
                                                                        lblModalTimeoutMessage.Text = "Thank you for using SmartCloud ATM. Don't forget your card and money.";
                                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTimeout();", true);
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //ATM doesn't have enough money
                                                lblModalTitle.Text = "Amount too large";
                                                lblModalMessage.Text = "This amount is too large to withdraw from this ATM.";
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                                            }
                                        }
                                        else
                                        {
                                            System.Diagnostics.Debug.WriteLine("should break : " + intCount);
                                            //ATM can't withdraw total number, bad number i.e 203
                                            lblModalTitle.Text = "ATM can't withdraw £" + intWithdraw;
                                            lblModalMessage.Text = "This ATM is unable to withdraw that amount.";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Amount too much error
                            lblModalTitle.Text = "Withdraw amount too much";
                            lblModalMessage.Text = "Sorry but we can only withdraw the amount of your balance plus £100.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                    }
                }
                else
                {
                    //users balance cannot be brought back using pin
                }
            }
            Connections.con.Close();
        }
    }
}