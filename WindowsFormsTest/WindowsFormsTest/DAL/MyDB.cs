using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsTest.DAL
{
    class MyDB
    {

        public static SqlCommand GetCmd_SP(SqlConnection MyConnection, string Qs, out string er)
        {
            er = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand()
                {
                    Connection = MyConnection,
                    CommandText = Qs,
                    CommandTimeout = 0
                };
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@RV", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add("@Error_Msg", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;
                if (MyConnection.State != ConnectionState.Open) MyConnection.Open();
                return cmd;
            }
            catch (Exception ex)
            {
                er = "DAL.EpmloyeeDB.GetCmd_SP:" + ex.Message;
                return null;
            }
        }
    }
}
