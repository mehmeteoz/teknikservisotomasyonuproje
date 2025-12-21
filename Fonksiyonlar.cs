using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TeknikServisOtomasyonuProje
{
    internal class Fonksiyonlar
    {
        // Validates an email address
        public bool CheckEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email; // ensures exact match
            }
            catch
            {
                return false;
            }
        }

        // Validates a 10-digit phone number that does not start with 0
        public bool CheckPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^[1-9][0-9]{9}$");
        }

        public List<User> GetUserInfo(int userID, SqlConnection sqlConnection)
        {
            List<User> users = new List<User>();

            try
            {
                sqlConnection.Open();

                string query = "SELECT * FROM Users WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@UserID", userID);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Role = reader["Role"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["PasswordHash"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        CreatedAt = reader["CreatedAt"].ToString()
                    };

                    users.Add(user);
                }

                reader.Close();
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }

            return users;
        }
    }
}
