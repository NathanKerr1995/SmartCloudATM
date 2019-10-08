using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    class Login
    {
        public Int32 checkApi(Int32 pin)
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

                string postData = "{\"currentBalance\":\"testbody\",\"pin\":\"" + pin + "\"}";

                using (var streamWriter = new StreamWriter(requestObject.GetRequestStream()))
                {
                    streamWriter.Write(postData);
                    streamWriter.Flush();
                    streamWriter.Close();

                    var httpResponse = (HttpWebResponse)requestObject.GetResponse();

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result2 = streamReader.ReadToEnd();
                        return 1;
                    }
                }
                
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
