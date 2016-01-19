using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace Terminal
{


    public partial class NewAccountWindow : Window
    {
        private static readonly String connectionString = ConfigurationManager.ConnectionStrings["Terminal.Properties.Settings.Manager"].ConnectionString;
        private SqlConnection connection;

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
            TBAgentCity.Text = TBClientCity.Text;
            TBAgentEmail.Text = TBClientEmail.Text;
            TBAgentPhoneNumber.Text = TBClientPhoneNumber.Text;
            TBAgentPostCode.Text = TBClientPostCode.Text;
            TBAgentStreet.Text = TBClientStreet.Text;

            TBAgentStreet.IsEnabled =
                TBAgentPostCode.IsEnabled =
                TBAgentPhoneNumber.IsEnabled =
                TBAgentEmail.IsEnabled =
                TBAgentCity.IsEnabled = ! (bool)CBAgentClientData.IsChecked;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if(TBAgentPassword.Text != TBAgentPassword2.Text)
            {
                MessageBox.Show(this, "Podane hasła różnią się!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            EmployeeDataSetTableAdapters.CONTACT_DATATableAdapter adapter = new EmployeeDataSetTableAdapters.CONTACT_DATATableAdapter();
            
            //adapter.Inert()???
            
            //EmployeeDataSet.CONTACT_DATADataTable table = adapter.GetData();
            //EmployeeDataSet.CONTACT_DATARow row = (EmployeeDataSet.CONTACT_DATARow)table.NewRow();

            //row.NAME = TBClientCity.Text;
            //row.ADDRESS1 = TBClientStreet.Text;
            //row.CITY = TBClientCity.Text;
            //row.POSTAL_CODE = TBClientPostCode.Text;
            //row.EMAIL = TBClientEmail.Text;
            //row.PHONE_NUMBER = TBClientPhoneNumber.Text;



            //table.AddCONTACT_DATARow(row);
            //adapter.Update(table);

            connection = new SqlConnection(connectionString);
            EmployeeDataSetTableAdapters.CONTACT_DATATableAdapter x = 
                new EmployeeDataSetTableAdapters.CONTACT_DATATableAdapter();
            
            x.InsertWithSequence(   TBClientName.Text, 
                                    TBClientStreet.Text, "", 
                                    TBClientCity.Text, 
                                    TBAgentPostCode.Text, 
                                    TBClientEmail.Text, 
                                    TBAgentPhoneNumber.Text );
            System.Console.WriteLine("Insert poszedł!");
        }
    }
}
