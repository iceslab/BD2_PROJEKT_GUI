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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Terminal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AgentWindow : Window
    {
        private string login;

        public AgentWindow()
        {
            InitializeComponent();
            login = "";
            FillData();
        }
        public AgentWindow(string _login)
        {
            InitializeComponent();
            login = _login;
            FillData();
        }

        private void FillData()
        {
            AgentDataSetTableAdapters.CONTACT_DATATableAdapter accountsTA =
                new AgentDataSetTableAdapters.CONTACT_DATATableAdapter();
            var row = accountsTA.GetDataByLogin(login)[0];
            TBAgentName.Text = row.NAME;
            TBAgentStreet.Text = row.ADDRESS1;
            TBAgentCity.Text = row.CITY;
            TBAgentPostCode.Text = row.POSTAL_CODE;
            TBAgentPhoneNumber.Text = row.PHONE_NUMBER;
            TBAgentEmail.Text = row.EMAIL;
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
