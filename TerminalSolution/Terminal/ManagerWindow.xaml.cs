﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        private static readonly String connectionString = ConfigurationManager.ConnectionStrings["Terminal.Properties.Settings.Manager"].ConnectionString;
        private SqlConnection connection;


        public ManagerWindow()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            try
            {
                connection.Open();
                String query = "select * from [dbo].CONTACT_DATA";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable("Dane klientów");

                adapter.Fill(table);
                clientsDataGrid.ItemsSource = table.DefaultView;
                adapter.Update(table);

                connection.Close();
            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.Message);
            }

        }
    }
}
