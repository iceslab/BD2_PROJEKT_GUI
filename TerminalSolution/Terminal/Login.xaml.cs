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
            loginTBox.Focus();
        }

        private void LogInButtonClicked(object sender, RoutedEventArgs e)
        {
            ValidateCredentialsDataSetTableAdapters.QueriesTableAdapter tableAdapter =
                new ValidateCredentialsDataSetTableAdapters.QueriesTableAdapter();
            var login = loginTBox.Text;
            var hash = CalculateMD5Hash(passwordTBox.Password);
            passwordTBox.Password = null;
            int? permissions = tableAdapter.VALIDATE_CREDENTIALS_FUNCTION(login, hash);

            if (permissions != null)
            {
                Window window;
                switch (permissions)
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

        public static string CalculateMD5Hash(string input)
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

        private void SingUpButtonClicked(object sender, RoutedEventArgs e)
        {
            Window window = new NewAccountWindow();
            window.Show();
        }
    }
}
