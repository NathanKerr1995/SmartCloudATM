using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATM
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Checks if the atm has any notes in the database (tblCurrency)
            if (!IsPostBack)
            {
                Connections cnnCheckMoney = new Connections();
                cnnCheckMoney.sql_connection();
                Connections.con.Open();

                using (SqlDataReader sdrCheckMoney = new SqlCommand("SELECT SUM(CurrencyNoteTotal) AS CountCurrencyTotal FROM tblCurrency WHERE Status = '1'", Connections.con).ExecuteReader())
                {
                    if (sdrCheckMoney.HasRows)
                    {
                        while (sdrCheckMoney.Read())
                        {
                            if (Convert.ToInt32(sdrCheckMoney["CountCurrencyTotal"]) == 0)
                            {
                                Response.Redirect("OutOfOrder.aspx");
                            }
                        }
                    }
                }

                Connections.con.Close();
            }
        }
    }
}