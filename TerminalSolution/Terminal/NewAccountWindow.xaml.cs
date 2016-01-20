using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace Terminal
{


    public partial class NewAccountWindow : Window
    {
        private static readonly String connectionString = ConfigurationManager.ConnectionStrings["Terminal.Properties.Settings.Manager"].ConnectionString;

        public NewAccountWindow()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CBAgentClientData_Changed(object sender, RoutedEventArgs e)
        {
            if (CBAgentClientData.IsChecked == true)
            {
                TBAgentCity.Text = TBClientCity.Text;
                TBAgentEmail.Text = TBClientEmail.Text;
                TBAgentPhoneNumber.Text = TBClientPhoneNumber.Text;
                TBAgentPostCode.Text = TBClientPostCode.Text;
                TBAgentStreet.Text = TBClientStreet.Text;
            }
            else
            {
                TBAgentCity.Text =
                TBAgentEmail.Text =
                TBAgentPhoneNumber.Text =
                TBAgentPostCode.Text =
                TBAgentStreet.Text = "";
            }

            TBAgentStreet.IsEnabled =
                TBAgentPostCode.IsEnabled =
                TBAgentPhoneNumber.IsEnabled =
                TBAgentEmail.IsEnabled =
                TBAgentCity.IsEnabled = !(bool)CBAgentClientData.IsChecked;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (PBAgentPassword.Password != PBAgentPassword2.Password)
            {
                MessageBox.Show(this, "Podane hasła różnią się!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (RegisterValidation())
            {
                EmployeeDataSetTableAdapters.CONTACT_DATATableAdapter contactTA =
                new EmployeeDataSetTableAdapters.CONTACT_DATATableAdapter();
                EmployeeDataSetTableAdapters.CLIENTSTableAdapter clientsTA =
                    new EmployeeDataSetTableAdapters.CLIENTSTableAdapter();
                EmployeeDataSetTableAdapters.AGENTSTableAdapter agentsTA =
                    new EmployeeDataSetTableAdapters.AGENTSTableAdapter();
                EmployeeDataSetTableAdapters.ACCOUNTSTableAdapter accountsTA =
                    new EmployeeDataSetTableAdapters.ACCOUNTSTableAdapter();

                // Dodaj dane kontaktowe klienta
                var clientContactDataId = (int)contactTA.InsertWithSequence(
                                        TBClientName.Text,
                                        TBClientStreet.Text, null,
                                        TBClientCity.Text,
                                        TBAgentPostCode.Text,
                                        TBClientEmail.Text,
                                        TBAgentPhoneNumber.Text);
                // Dodaj klienta
                var clientId = (int)clientsTA.InsertWithSequence(clientContactDataId);

                // Dodaj dane kontaktowe agenta
                var agentContactDataId = (int)contactTA.InsertWithSequence(
                                        TBAgentName.Text,
                                        TBAgentStreet.Text, null,
                                        TBAgentCity.Text,
                                        TBAgentPostCode.Text,
                                        TBAgentEmail.Text,
                                        TBAgentPhoneNumber.Text);
                // Dodaj agenta
                var agentId = (int)agentsTA.InsertWithSequence(agentContactDataId);
                var accountId = (int)accountsTA.InsertWithSequence(
                    TBAgentLogin.Text,
                    Login.CalculateMD5Hash(PBAgentPassword.Password),
                    3, 1, agentId);
                MessageBox.Show(this, "Zarejestrowano konto. Czekaj na aktywację.", "Rejestracja", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
            {
                MessageBox.Show(this, "Wypełnij wszystkie pola na obu kartach!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool RegisterValidation()
        {

            if (String.IsNullOrEmpty(TBClientName.Text))
                return false;
            if (String.IsNullOrEmpty(TBClientStreet.Text))
                return false;
            if (String.IsNullOrEmpty(TBClientCity.Text))
                return false;
            if (String.IsNullOrEmpty(TBClientPostCode.Text))
                return false;
            if (String.IsNullOrEmpty(TBClientPhoneNumber.Text))
                return false;
            if (String.IsNullOrEmpty(TBClientEmail.Text))
                return false;

            if (String.IsNullOrEmpty(TBAgentName.Text))
                return false;
            if (String.IsNullOrEmpty(TBAgentLogin.Text))
                return false;
            if (String.IsNullOrEmpty(PBAgentPassword.Password))
                return false;
            if (String.IsNullOrEmpty(PBAgentPassword2.Password))
                return false;
            if (String.IsNullOrEmpty(TBAgentStreet.Text))
                return false;
            if (String.IsNullOrEmpty(TBAgentCity.Text))
                return false;
            if (String.IsNullOrEmpty(TBAgentPostCode.Text))
                return false;
            if (String.IsNullOrEmpty(TBAgentPhoneNumber.Text))
                return false;
            if (String.IsNullOrEmpty(TBAgentEmail.Text))
                return false;
            return true;
        }
    }
}
