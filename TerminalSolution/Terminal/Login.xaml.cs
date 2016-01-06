using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Terminal
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["Terminal.Properties.Settings.ValidateCredentials"].ConnectionString;
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlInt32 permissions;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (SqlException exc)
                {
                    MessageBox.Show(exc.Message);
                }

                //TerminalMSSQLDataSet dataSet = new TerminalMSSQLDataSet();
                //TerminalMSSQLDataSetTableAdapters.ACCOUNTSTableAdapter accountTableAdapter =
                //    new TerminalMSSQLDataSetTableAdapters.ACCOUNTSTableAdapter();
                //accountTableAdapter.Fill(dataSet.ACCOUNTS);

                //string query = "SELECT * FROM INFORMATION_SCHEMA.TABLES";
                //string query2 = "SELECT * FROM dbo.CONTACT_DATA";

                string query1 = "dbo.VALIDATE_CREDENTIALS_FUNCTION";
                SqlCommand command = new SqlCommand(query1, conn);

                command.CommandType = CommandType.StoredProcedure;

                var returnParam = new SqlParameter
                {
                    ParameterName = "@permission",
                    Direction = ParameterDirection.ReturnValue
                };
                //Console.WriteLine(CalculateMD5Hash("hash01"));
                var login = loginTBox.Text;
                var hash = CalculateMD5Hash(passwordTBox.Password);
                command.Parameters.Add("@login", SqlDbType.VarChar);
                command.Parameters.Add("@hash", SqlDbType.VarChar);
                command.Parameters.Add(returnParam);

                command.Parameters["@login"].Value = login;
                command.Parameters["@hash"].Value = hash;

                command.ExecuteNonQuery();
                permissions = (SqlInt32)returnParam.SqlValue;
                //var reader = command.ExecuteReader();
                //while (reader.Read())
                //{
                //    Console.WriteLine("Values:  {0}, {1}, {2}, {3}",
                //        reader[0], reader[1], reader[2], reader[3]);
                //}
                //MessageBox.Show(result.ToString());
            }


            if (!permissions.IsNull)
            {
                Window window;
                switch (permissions.Value)
                {
                    default:
                        break;
                    case 1:
                        window = new ManagerWindow();
                        window.Show();
                        break;
                    case 2:
                        window = new StaffWindow();
                        window.Show();
                        break;
                    case 3:
                        window = new AgentWindow();
                        window.Show();
                        break;
                }
                Close();
            }
            else
            {
                MessageBox.Show(this, "Zły login lub hasło", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
