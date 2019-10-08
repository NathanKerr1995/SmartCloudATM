using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Data;
using System.Speech.Synthesis;

namespace ATM
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["intPinIncorrect"] = 0;

                txtNumPad.Focus();
            }
        }

        protected void btnNumPadEnter_Click(object sender, EventArgs e)
        {
            //Check pin entered is correct
            try
            {
                //POST
                string strURLTest = String.Format("https://frontend-challenge.screencloud-michael.now.sh/api/pin/");
                WebRequest requestObject = WebRequest.Create(strURLTest);
                requestObject.Method = "POST";
                requestObject.ContentType = "application/json";
                requestObject.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                requestObject.UseDefaultCredentials = true;

                string postData = "{\"currentBalance\":\"testbody\",\"pin\":\"" + txtNumPad.Text + "\"}";

                using (var streamWriter = new StreamWriter(requestObject.GetRequestStream()))
                {
                    streamWriter.Write(postData);
                    streamWriter.Flush();
                    streamWriter.Close();

                    var httpResponse = (HttpWebResponse)requestObject.GetResponse();

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result2 = streamReader.ReadToEnd();
                        Session["UsersPepper"] = txtNumPad.Text;
                        Response.Redirect("MainScreen.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                lblResponse.Text = "Pin Entered incorrectly";
                lblResponse.CssClass = "text-red font-weight-bold";
                txtNumPad.Text = string.Empty;
                Session["intPinIncorrect"] = Convert.ToInt32(Session["intPinIncorrect"]) + 1;

                //pin incorrect count
                if (Convert.ToInt32(Session["intPinIncorrect"]) >= 5)
                {
                    Response.Redirect("Index.aspx");
                }
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
            synth.Speak("This page is for entering your pin. Please enter your pin and press enter.");
            synth.Dispose();
        }

    }
}