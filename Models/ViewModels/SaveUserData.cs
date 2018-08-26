using System;
using System.Data.SqlClient;
using System.Data;

namespace NBAwebsite.Models.ViewModels
{
    /**
     * This class saves values of register properties and
     * insertrs them into the tblUsers table in the database
     */

    public class SaveUserData : Register
    {
        SqlConnection con = new SqlConnection(GetConnectionString.conString());
        string registerMessage = "";

        void provideParameters(SqlCommand cmd)
        {
            SqlParameter par = new SqlParameter("parUsername", SqlDbType.VarChar);
            par.SqlValue = Username;
            cmd.Parameters.Add(par);

            par = new SqlParameter("parEmail", SqlDbType.VarChar);
            par.SqlValue = Email;
            cmd.Parameters.Add(par);

            par = new SqlParameter("parPassword", SqlDbType.VarChar);
            par.SqlValue = Password;
            cmd.Parameters.Add(par);

            par = new SqlParameter("parDate", SqlDbType.DateTime);
            par.SqlValue = DateTime.Now;
            cmd.Parameters.Add(par);
        }

        /* This method checks whether an username of an email which an user provided
         * alrady exists in a database or not.
         * It returns 1 if there's an attempt of inserting of duplicate username, 2 of email
         * and 0 otherwise
         */
        int isDuplicateUsernameOrEmail()
        {
            string checkForUsername = "select max(userName) from tblUser where userName = @parUsername";
            SqlCommand cmd = new SqlCommand(checkForUsername, con);
            provideParameters(cmd);
            string userN = cmd.ExecuteScalar().ToString();

            string checkForEmail = "select max(userEmail) from tblUser where userEmail = @parEmail";
            cmd = new SqlCommand(checkForEmail, con);
            provideParameters(cmd);
            string userEm = cmd.ExecuteScalar().ToString();

            if (userN == Username) { return 1; }
            else if (userEm == Email) { return 2; }
            return 0;
        }

        public string insertIntoTBL()
        {
            try
            {
                /* All the constraints are checked using the database except this one
                 * So, I have to check it before attempting to insert something into the database
                 */
                if (Password == ConfirmPassword)
                {
                    SqlCommand cmd = new SqlCommand("exec insertIntoTblUser @parUsername, @parEmail, @parPassword, @parDate", con);
                    provideParameters(cmd);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    registerMessage = "Account created successfully";
                }
                else
                {
                    registerMessage = "Confirmation Password doesn't match the Password";
                }
            }
            catch(SqlException ex)
            {
                if (Username == null || Email == null || Password == null || ConfirmPassword == null)
                {
                    return registerMessage = "All the input fields must be filled out";
                }

                if (isDuplicateUsernameOrEmail() == 1)
                {
                    return registerMessage = "The username already exists in the database. " +
                        "Please provide a different one";
                }
                else if(isDuplicateUsernameOrEmail() == 2)
                {
                    return registerMessage = "The email already exists in the database. " +
                        "Please provide a different one";
                }

                if(Password.Length < 4)
                {
                    return registerMessage = "The field Password must have a minimum length of 4";
                }

                registerMessage = ex.Message;
            }
            catch (Exception ex)
            {
                registerMessage = ex.GetType().ToString() + " -- " + ex.Message;
            }
            finally
            {
                con.Close();
            }

            return registerMessage;
        }
    }
}
