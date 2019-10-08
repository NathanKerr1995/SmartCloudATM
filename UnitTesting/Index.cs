using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    class Index
    {

        public bool checkOutOfOrder()
        {
            try
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
                                
                            }
                        }
                    }
                }

                Connections.con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool checkOutOfOrderTotal(Int32 Total)
        {
            if (Total == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
