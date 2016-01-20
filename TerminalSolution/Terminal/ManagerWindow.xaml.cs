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
        private enum TabViewType
        {
            CLIENTSVIEW,
            AIRCRAFSTVIEW,
            INFRASTRUCTUREVIEW,
            MAINTANANCEVIEW,
            RESERVATIONSVIEW
        };
        private static readonly String connectionString = ConfigurationManager.ConnectionStrings["Terminal.Properties.Settings.Manager"].ConnectionString;
        

        public ManagerWindow()
        {
            InitializeComponent();
            changeTab(TabViewType.CLIENTSVIEW);
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

        private void changeTab(TabViewType type)
        {
            switch(type)
            {
                case TabViewType.CLIENTSVIEW:
                    TIData.Header = "Dane klientów";
                    FillDataGrid(new ManagerDataSetTableAdapters.CLIENTSTableAdapter());
                    break;
                case TabViewType.AIRCRAFSTVIEW:
                    TIData.Header = "Samoloty przegląd";
                    FillDataGrid(new ManagerDataSetTableAdapters.AIRCRAFTSTableAdapter());
                    break;
                case TabViewType.INFRASTRUCTUREVIEW:
                    TIData.Header = "Infrastruktura przegląd";
                    FillDataGrid(new ManagerDataSetTableAdapters.INFRASTRUCTURETableAdapter());
                    break;
                case TabViewType.MAINTANANCEVIEW:
                    TIData.Header = "Planowane remonty";
                    FillDataGrid(new ManagerDataSetTableAdapters.MAINTENANCETableAdapter());
                    break;
                case TabViewType.RESERVATIONSVIEW:
                    TIData.Header = "Aktualne rezerwacje";
                    FillDataGrid(new ManagerDataSetTableAdapters.RESERVATIONSTableAdapter());
                    break;

            }
        }

        private void clientsButton_Click(object sender, RoutedEventArgs e)
        {
            changeTab(TabViewType.CLIENTSVIEW);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            changeTab(TabViewType.AIRCRAFSTVIEW);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            changeTab(TabViewType.INFRASTRUCTUREVIEW);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            changeTab(TabViewType.MAINTANANCEVIEW);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            changeTab(TabViewType.RESERVATIONSVIEW);
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            Close();
        }

    }
}
