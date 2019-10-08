using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    class Connections
    {
        public static SqlConnection con = null;

        public void sql_connection()
        {
            con = new SqlConnection(@"Data Source=smartcloud.cuawljtgnw6p.eu-west-2.rds.amazonaws.com,1433;Initial Catalog=ATM;User ID=NathanKerr;Password=SmartCloud10;MultipleActiveResultSets=True");
        }

        //public void sql_connection()
        //{
        //    con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ATM.mdf;Integrated Security=True;MultipleActiveResultSets=True");
        //}

    }
}
