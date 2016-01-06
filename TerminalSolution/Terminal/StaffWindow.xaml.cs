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
    /// Interaction logic for StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        public StaffWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

           /*Terminal.TerminalDBDataSet terminalDBDataSet = ((Terminal.TerminalDBDataSet)(this.FindResource("terminalDBDataSet")));
            // Load data into the table Table. You can modify this code as needed.
            Terminal.TerminalDBDataSetTableAdapters.TableTableAdapter terminalDBDataSetTableTableAdapter = new Terminal.TerminalDBDataSetTableAdapters.TableTableAdapter();
            terminalDBDataSetTableTableAdapter.Fill(terminalDBDataSet.Table);
            System.Windows.Data.CollectionViewSource tableViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tableViewSource")));
            tableViewSource.View.MoveCurrentToFirst();*/
        }
    }
}
