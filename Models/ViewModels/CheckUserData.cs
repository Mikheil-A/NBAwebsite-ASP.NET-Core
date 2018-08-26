using System;
using System.Data.SqlClient;

namespace NBAwebsite.Models.ViewModels
{
    public class CheckUserData : Login
    {
        SqlConnection con = new SqlConnection(GetConnectionString.conString());
        string testInfo = "";

        public string lastRegisteredUsename, lastRegisteredDate;
        void getLastRegisteredUser()
        {
            string cmdText = "select userName, userRegisterDate from tblUser where userRegisterDate = (select max(userRegisterDate) from tblUser)";
            using (SqlCommand cmd = new SqlCommand(cmdText, con))
            {
                //It was complaining that the connection was open and it should be closed first
                if(con.State.ToString() == "Open") { con.Close(); }
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                r.Read();
                lastRegisteredUsename = r["userName"].ToString();
                lastRegisteredDate = r["userRegisterDate"].ToString();
            }//The connection will be closed here
        }

        void provideParameters(SqlCommand com)
        {
            SqlParameter p1 = new SqlParameter("email", System.Data.SqlDbType.VarChar);
            p1.SqlValue = Email;
            p1.Size = 256;
            com.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("password", System.Data.SqlDbType.VarChar);
            p2.SqlValue = Password;
            p2.Size = 50;
            com.Parameters.Add(p2);
        }

        //Variables for logged in user data (sessions)
        public string loggedInUsername, loggedInUserEmail, loggedinUserRegisterDate;
        public bool exists()
        {
            bool b = false;
            try
            {
                //string cmdText = "select userName, userEmail, userPassword from tblUser where userEmail = @email and userPassword = @password";
                string cmdText = "select * from tblUser where userEmail = @email and userPassword = @password";
                SqlCommand cmd = new SqlCommand(cmdText, con);
                con.Open();
                provideParameters(cmd);

                SqlDataReader reader = cmd.ExecuteReader();

                //The while statement will execute only ones
                //since 'reader' instance consists of a table with one row
                //while (reader.Read())
                //{
                //    if (reader["userEmail"].ToString() == Email && reader["userPassword"].ToString() == Password)
                //    {
                //        b = true;
                //    }
                //}

                //Or it can be checked simply like that:
                if (reader.HasRows)
                {
                    b = true;
                    
                    reader.Read();
                    loggedInUsername = reader["userName"].ToString();
                    loggedInUserEmail = reader["userEmail"].ToString();
                    loggedinUserRegisterDate = reader["userRegisterDate"].ToString();

                    getLastRegisteredUser();
                }
            }
            catch (Exception ex)
            {
                testInfo = ex.Message;
            }
            finally
            {
                con.Close();
            }

            return b;
        }
    }
}
