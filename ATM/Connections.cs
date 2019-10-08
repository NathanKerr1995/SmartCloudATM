using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ATM
{
    public class Connections
    {
        public static SqlConnection con = null;

        //hosted connection
        public void sql_connection()
        {
            con = new SqlConnection(@"Data Source=smartcloud.cuawljtgnw6p.eu-west-2.rds.amazonaws.com,1433;Initial Catalog=ATM;User ID=NathanKerr;Password=SmartCloud10;MultipleActiveResultSets=True");
        }

        //local database
        //public void sql_connection()
        //{
        //    con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ATM.mdf;Integrated Security=True;MultipleActiveResultSets=True");
        //}

    }
}