using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATM
{
    public partial class Reset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Connections cnnReset = new Connections();
            cnnReset.sql_connection();
            Connections.con.Open();

            using (SqlCommand cmdResetUser = new SqlCommand("UPDATE tblUserDetails SET UserBalance = 220 WHERE Pin = 1111 AND Status = '1'", Connections.con))
            {
                cmdResetUser.ExecuteNonQuery();
            }

            using (SqlCommand cmdResetCurrency5 = new SqlCommand("UPDATE tblCurrency SET CurrencyNoteTotal = 4 WHERE CurrencyAmount = 5 AND Status = '1'", Connections.con))
            {
                cmdResetCurrency5.ExecuteNonQuery();
            }

            using (SqlCommand cmdResetCurrency10 = new SqlCommand("UPDATE tblCurrency SET CurrencyNoteTotal = 15 WHERE CurrencyAmount = 10 AND Status = '1'", Connections.con))
            {
                cmdResetCurrency10.ExecuteNonQuery();
            }
            using (SqlCommand cmdResetCurrency20 = new SqlCommand("UPDATE tblCurrency SET CurrencyNoteTotal = 7 WHERE CurrencyAmount = 20 AND Status = '1'", Connections.con))
            {
                cmdResetCurrency20.ExecuteNonQuery();
            }

            Connections.con.Close();
        }
    }
}