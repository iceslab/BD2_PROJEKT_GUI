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
        private enum AgentTabViewType
        {
            ACCOUNTVIEW,
            ACCOUNTEDIT,
            AIRCRAFSTVIEW,
            AIRCRAFSTEDIT,
            RESERVATIONSADD,
            RESERVATIONSVIEW,
            RESERVATIONSREMOVE
        };

        private string login;
        private string[] labelNames = { "Imię i nazwisko", "Ulica", "Miasto", "Kod pocztowy", "Nr telefonu", "E-mail" };
        private Grid GAccount, GTabData, GTabStats;
        private DataGrid DGData, DGStats;
        private TabControl TCView;
        private TabItem TIData, TIStats;
        private TextBox[] textBoxes;
        private Label[] labels;

        private void InitializeControls()
        {
            InitializeAccountGrid();
            InitializeTabControlView();
        }

        private void InitializeAccountGrid()
        {
            GAccount = new Grid();
            GAccount.SetValue(Grid.RowProperty, 1);
            GAccount.SetValue(Grid.ColumnProperty, 1);
            GAccount.SetValue(Grid.ColumnSpanProperty, 2);
            ColumnDefinition col1 = new ColumnDefinition(), col2 = new ColumnDefinition();
            col1.Width = GridLength.Auto;
            col2.Width = GridLength.Auto;
            GAccount.ColumnDefinitions.Add(col1);
            GAccount.ColumnDefinitions.Add(col2);

            labels = new Label[labelNames.Length];
            textBoxes = new TextBox[labelNames.Length];

            for (int i = 0; i < labelNames.Length; i++)
            {
                var row = new RowDefinition();
                row.Height = GridLength.Auto;
                GAccount.RowDefinitions.Add(row);

                labels[i] = new Label();
                labels[i].Content = labelNames[i];
                labels[i].HorizontalAlignment = HorizontalAlignment.Right;
                labels[i].VerticalAlignment = VerticalAlignment.Top;
                labels[i].SetValue(Grid.RowProperty, i);
                GAccount.Children.Add(labels[i]);

                textBoxes[i] = new TextBox();
                textBoxes[i].HorizontalAlignment = HorizontalAlignment.Left;
                textBoxes[i].TextWrapping = TextWrapping.Wrap;
                textBoxes[i].Height = 23.0;
                textBoxes[i].Width = 120.0;
                textBoxes[i].SetValue(Grid.ColumnProperty, 1);
                textBoxes[i].SetValue(Grid.RowProperty, i);
                GAccount.Children.Add(textBoxes[i]);
            }
        }

        private void InitializeTabControlView()
        {
            TCView = new TabControl();
            TCView.SetValue(Grid.ColumnSpanProperty, 2);
            TCView.SetValue(Grid.ColumnProperty, 1);
            TCView.SetValue(Grid.RowProperty, 1);

            TIData = new TabItem();
            TIData.Header = "Dane klientów";
            TIData.Height = 22;
            TIData.VerticalAlignment = VerticalAlignment.Top;

            GTabData = new Grid();

            DGData = new DataGrid();
            DGData.AutoGenerateColumns = true;
            DGData.HorizontalAlignment = HorizontalAlignment.Left;
            DGData.VerticalAlignment = VerticalAlignment.Top;
            DGData.Width = 507;
            DGData.Height = 324;

            GTabData.Children.Add(DGData);
            TIData.Content = GTabData;

            TIStats = new TabItem();
            TIStats.Header = "Statystyki";
            GTabStats = new Grid();

            DGStats = new DataGrid();
            DGStats.AutoGenerateColumns = true;
            DGStats.HorizontalAlignment = HorizontalAlignment.Left;
            DGStats.VerticalAlignment = VerticalAlignment.Top;
            DGStats.Width = 507;
            DGStats.Height = 324;

            GTabStats.Children.Add(DGStats);
            TIStats.Content = GTabStats;

            TCView.Items.Add(TIData);
            TCView.Items.Add(TIStats);
        }

        private void SetupAccountGrid(bool enableControls)
        {
            if (mainGrid.Children.Contains(TCView))
                mainGrid.Children.Remove(TCView);
            if (!mainGrid.Children.Contains(GAccount))
                mainGrid.Children.Add(GAccount);

            foreach(var tb in textBoxes)
            {
                tb.IsEnabled = enableControls;
            }
        }

        private void SetupTabControlView(bool enableControls)
        {
            if (mainGrid.Children.Contains(GAccount))
                mainGrid.Children.Remove(GAccount);
            if (!mainGrid.Children.Contains(TCView))
                mainGrid.Children.Add(TCView);

            DGData.IsEnabled = enableControls;
            DGStats.IsEnabled = enableControls;
        }

        public AgentWindow(string _login)
        {
            InitializeComponent();
            InitializeControls();
            login = _login;
            changeTab(AgentTabViewType.ACCOUNTVIEW);
        }

        private void FillData(AgentDataSetTableAdapters.CONTACT_DATATableAdapter adapter)
        {
            var row = adapter.GetDataByLogin(login)[0];

            textBoxes[0].Text = row.NAME;
            textBoxes[1].Text = row.ADDRESS1;
            textBoxes[2].Text = row.CITY;
            textBoxes[3].Text = row.POSTAL_CODE;
            textBoxes[4].Text = row.PHONE_NUMBER;
            textBoxes[5].Text = row.EMAIL;
        }

        private void FillDataGrid(AgentDataSetTableAdapters.AIRCRAFTSTableAdapter adapter)
        {
            var table = adapter.GetData();
            DGData.ItemsSource = table.DefaultView;
        }
        private void FillDataGrid(AgentDataSetTableAdapters.RESERVATIONSTableAdapter adapter)
        {
            var table = adapter.GetData();
            DGData.ItemsSource = table.DefaultView;
        }

        private void changeTab(AgentTabViewType type)
        {

            switch (type)
            {
                case AgentTabViewType.ACCOUNTEDIT:
                    SetupAccountGrid(true);
                    TIData.Header = "Twoje dane edycja";
                    FillData(new AgentDataSetTableAdapters.CONTACT_DATATableAdapter());
                    break;
                case AgentTabViewType.ACCOUNTVIEW:
                    SetupAccountGrid(false);
                    TIData.Header = "Twoje dane przegląd";
                    FillData(new AgentDataSetTableAdapters.CONTACT_DATATableAdapter());
                    break;
                case AgentTabViewType.AIRCRAFSTEDIT:
                    SetupTabControlView(true);
                    TIData.Header = "Samoloty edycja";
                    FillDataGrid(new AgentDataSetTableAdapters.AIRCRAFTSTableAdapter());
                    break;
                case AgentTabViewType.AIRCRAFSTVIEW:
                    SetupTabControlView(false);
                    TIData.Header = "Samoloty przegląd";
                    FillDataGrid(new AgentDataSetTableAdapters.AIRCRAFTSTableAdapter());
                    break;
                case AgentTabViewType.RESERVATIONSADD:
                    SetupTabControlView(true);
                    TIData.Header = "Rezerwacje dodaj";
                    FillDataGrid(new AgentDataSetTableAdapters.RESERVATIONSTableAdapter());
                    break;
                case AgentTabViewType.RESERVATIONSREMOVE:
                    SetupTabControlView(true);
                    TIData.Header = "Rezerwacje usuń";
                    FillDataGrid(new AgentDataSetTableAdapters.RESERVATIONSTableAdapter());
                    break;
                case AgentTabViewType.RESERVATIONSVIEW:
                    SetupTabControlView(false);
                    TIData.Header = "Rezerwacje przegląd";
                    FillDataGrid(new AgentDataSetTableAdapters.RESERVATIONSTableAdapter());
                    break;

            }
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            Close();
        }

        private void accountViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeTab(AgentTabViewType.ACCOUNTVIEW);
        }

        private void accountEditButton_Click(object sender, RoutedEventArgs e)
        {
            changeTab(AgentTabViewType.ACCOUNTEDIT);
        }

        private void aircraftsViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeTab(AgentTabViewType.AIRCRAFSTVIEW);
        }

        private void aircraftsEditButton_Click(object sender, RoutedEventArgs e)
        {
            changeTab(AgentTabViewType.AIRCRAFSTEDIT);
        }

        private void reservationsAddButton_Click(object sender, RoutedEventArgs e)
        {
            changeTab(AgentTabViewType.RESERVATIONSADD);
        }

        private void reservationsViewButton_Click(object sender, RoutedEventArgs e)
        {
            changeTab(AgentTabViewType.RESERVATIONSVIEW);
        }

        private void reservationsDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            changeTab(AgentTabViewType.RESERVATIONSREMOVE);
        }
    }
}
