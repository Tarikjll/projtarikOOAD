using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClCompany;               // SessionContext
using WpfCompany;        // straks die pages

namespace WpfCompany
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            // 2) Start met de login-pagina
            MainFrame.Navigate(new LoginPage());
        }

        private void BtnYearReports_Click(object sender, RoutedEventArgs e)
        {
            Company? current = SessionContext.CurrentCompany;
            if (current != null)
            {
                MainFrame.Navigate(new YearReportPage(current));
            }
            else
            {
                // eventueel: toon een melding of forceer login opnieuw
                MainFrame.Navigate(new LoginPage());
            }
        }

        private void BtnStats_Click(object sender, RoutedEventArgs e)
        {
            Company? current = SessionContext.CurrentCompany;
            if (current != null)
            {
                MainFrame.Navigate(new StatsPage(current));
            }
            else
            {
                MainFrame.Navigate(new LoginPage());
            }
        }
    }
}
