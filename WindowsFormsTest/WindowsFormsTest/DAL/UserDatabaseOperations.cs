using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsTest.BLL;
using WindowsFormsTest.BO;

namespace WindowsFormsTest.DAL
{
    class UserDatabaseOperations
    {

        public static bool GetUserData2(ref UserModel UserData, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (UserData == null)
            {
                errorMessage = "UserData == null";
                return false;
            }
            if (UserData.User_Email.Trim().Equals(string.Empty))
            {
                errorMessage = "UserData.User_Email == str.empy";
                return false;
            }
            try
            {

                using (SqlConnection MyConnection = new SqlConnection(Super.connectionString))
                {
                    string Qs = "dbo.[GET_USER_DATA2]";
                    SqlCommand cmd = MyDB.GetCmd_SP(MyConnection, Qs, out errorMessage);

                    cmd.Parameters.AddWithValue("@User_Email", UserData.User_Email);
                    cmd.Parameters.AddWithValue("@User_Password", UserData.User_Password);

                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@User_IS_Admin", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@User_Name", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@User_Phone", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    MyConnection.Close();

                    if ((int)cmd.Parameters["@RV"].Value != 1)
                    {
                        errorMessage = cmd.Parameters["@Error_Msg"].Value.ToString();
                        return false;
                    }

                    UserData.User_ID = (int)cmd.Parameters["@User_ID"].Value;
                    UserData.User_IS_Admin = (bool)cmd.Parameters["@User_IS_Admin"].Value;
                    UserData.User_Name = cmd.Parameters["@User_Name"].Value.ToString();
                    UserData.User_Phone = cmd.Parameters["@User_Phone"].Value.ToString();
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "UserDatabaseOperations.GetUserData2.Exception:" + ex.Message;
                return false;
            }
        }
    }
}
