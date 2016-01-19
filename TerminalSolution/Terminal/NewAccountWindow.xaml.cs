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

namespace Terminal
{
    /// <summary>
    /// Interaction logic for NewAccountWindow.xaml
    /// </summary>
    public partial class NewAccountWindow : Window
    {
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
            
            EmployeeDataSet.CONTACT_DATADataTable table = adapter.GetData();
            EmployeeDataSet.CONTACT_DATARow row = (EmployeeDataSet.CONTACT_DATARow)table.NewRow();

            row.NAME = TBClientCity.Text;
            row.ADDRESS1 = TBClientStreet.Text;
            row.CITY = TBClientCity.Text;
            row.POSTAL_CODE = TBClientPostCode.Text;
            row.EMAIL = TBClientEmail.Text;
            row.PHONE_NUMBER = TBClientPhoneNumber.Text;

            table.AddCONTACT_DATARow(row);
            adapter.Update(table);

        }
    }
}
