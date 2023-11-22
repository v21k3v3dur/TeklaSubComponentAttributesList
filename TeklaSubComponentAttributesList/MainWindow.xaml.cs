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

namespace TeklaSubComponentAttributesList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowDataModel model;
        public MainWindow()
        {
            InitializeComponent();
            model = new MainWindowDataModel();

            ConnectionLabelTextBlock.Text = model.ConnectionStatus;
        }

        private void componentCatalog_SelectClicked(object sender, EventArgs e)
        {
            this.componentCatalog.SelectedName=model.SubCompName;
        }

        private void componentCatalog_SelectionDone(object sender, EventArgs e)
        {
            model.SubCompName=componentCatalog.SelectedName;
            model.SubCompNumber=componentCatalog.SelectedNumber;
            componentNameBox.Text=model.SubCompName;
            componentNumberBox.Text = model.SubCompNumber.ToString();
        }
    }
}
