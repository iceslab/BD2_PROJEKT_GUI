using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
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
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        private enum ManagerTabViewType
        {
            CLIENTSVIEW,
            AIRCRAFSTVIEW,
            INFRASTRUCTUREVIEW,
            MAINTANANCEVIEW,
            RESERVATIONSVIEW
        };

        public ManagerWindow()
        {
            InitializeComponent();
            changeTab(ManagerTabViewType.CLIENTSVIEW);
        }

        private void FillDataGrid(ManagerDataSetTableAdapters.CLIENTSTableAdapter adapter)
        {
            var table = adapter.GetJoinedData();
            DGTabView.ItemsSource = table.DefaultView;
        }
        private void FillDataGrid(ManagerDataSetTableAdapters.AIRCRAFTSTableAdapter adapter)
        {
            var table = adapter.GetData();
            DGTabView.ItemsSource = table.DefaultView;
        }
        private void FillDataGrid(ManagerDataSetTableAdapters.INFRASTRUCTURETableAdapter adapter)
        {
            var table = adapter.GetData();
            DGTabView.ItemsSource = table.DefaultView;
        }
        private void FillDataGrid(ManagerDataSetTableAdapters.MAINTENANCETableAdapter adapter)
        {
            var table = adapter.GetData();
            DGTabView.ItemsSource = table.DefaultView;
        }
        private void FillDataGrid(ManagerDataSetTableAdapters.RESERVATIONSTableAdapter adapter)
        {
            var table = adapter.GetData();
            DGTabView.ItemsSource = table.DefaultView;
        }

        private void changeTab(ManagerTabViewType type)
        {
            switch(type)
            {
                case ManagerTabViewType.CLIENTSVIEW:
                    TIData.Header = "Dane klientów";
                    FillDataGrid(new ManagerDataSetTableAdapters.CLIENTSTableAdapter());
                    break;
                case ManagerTabViewType.AIRCRAFSTVIEW:
                    TIData.Header = "Samoloty przegląd";
                    FillDataGrid(new ManagerDataSetTableAdapters.AIRCRAFTSTableAdapter());
                    break;
                case ManagerTabViewType.INFRASTRUCTUREVIEW:
                    TIData.Header = "Infrastruktura przegląd";
                    FillDataGrid(new ManagerDataSetTableAdapters.INFRASTRUCTURETableAdapter());
                    break;
                case ManagerTabViewType.MAINTANANCEVIEW:
                    TIData.Header = "Planowane remonty";
                    FillDataGrid(new ManagerDataSetTableAdapters.MAINTENANCETableAdapter());
                    break;
                case ManagerTabViewType.RESERVATIONSVIEW:
                    TIData.Header = "Aktualne rezerwacje";
                    FillDataGrid(new ManagerDataSetTableAdapters.RESERVATIONSTableAdapter());
                    break;

            }
        }

        private void clientsButton_Click(object sender, RoutedEventArgs e)
        {
            changeTab(ManagerTabViewType.CLIENTSVIEW);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            changeTab(ManagerTabViewType.AIRCRAFSTVIEW);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            changeTab(ManagerTabViewType.INFRASTRUCTUREVIEW);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            changeTab(ManagerTabViewType.MAINTANANCEVIEW);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            changeTab(ManagerTabViewType.RESERVATIONSVIEW);
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            Close();
        }

    }
}
