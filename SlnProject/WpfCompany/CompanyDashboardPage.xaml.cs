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
using System.Windows;
using System.Windows.Controls;
using ClCompany;                // voor Company
using WpfCompany;        // pas aan naar jouw namespace voor YearReportPage & StatsPage

namespace WpfCompany
{
    public partial class CompanyDashboardPage : Page
    {
        private readonly Company _company;

        public CompanyDashboardPage(Company company)
        {
            InitializeComponent();
            _company = company;

            // laad standaard de jaarrapporten
            ContentFrame.Navigate(new YearReportPage(_company));
        }

        private void BtnYearReports_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new YearReportPage(_company));
        }

        private void BtnStats_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new StatsPage(_company));
        }
    }
}
