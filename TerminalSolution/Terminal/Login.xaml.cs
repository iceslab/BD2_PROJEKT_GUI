using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlServerCe;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace Terminal
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        String connectionString;
        public Login()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["Terminal.Properties.Settings.ValidateCredentials"].ConnectionString;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var wyndows = new List<Window>();
            wyndows.Add(new StaffWindow());
            wyndows.Add(new AgentWindow());
            wyndows.Add(new ManagerWindow());


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
                    ParameterName = "@return",
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
                var ret = returnParam.SqlValue;
                //var reader = command.ExecuteReader();
                //while (reader.Read())
                //{
                //    Console.WriteLine("Values:  {0}, {1}, {2}, {3}",
                //        reader[0], reader[1], reader[2], reader[3]);
                //}
                //MessageBox.Show(result.ToString());
            }

            foreach (Window win in wyndows)
            {
                win.Show();
            }
            this.Close();
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
