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

using System;
using System.Collections.Generic;
using System.Linq;
using ClCompany;

namespace WpfCompany
{
    public partial class YearReportPage : Page
    {
        private readonly Company _company;
        private List<YearReport> _allReports;

        public YearReportPage(Company company)
        {
            InitializeComponent();

            _company = company;
            _allReports = YearReport.GetByCompanyId(_company.Id);

            // vul de jaar-combo met unieke jaren
            List<int> jaren = _allReports
                .Select(r => r.Year)
                .Distinct()
                .OrderByDescending(y => y)
                .ToList();

            jaren.Insert(0, 0);  // 0 = “alle”
            cbYears.ItemsSource = jaren;
            cbYears.SelectedIndex = 0;

            // toon alle
            dgReports.ItemsSource = _allReports;
        }

        private void CbYears_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int gekozen = (int)cbYears.SelectedItem;
            if (gekozen == 0)
            {
                dgReports.ItemsSource = _allReports;
            }
            else
            {
                List<YearReport> gefilterd = _allReports
                    .Where(r => r.Year == gekozen)
                    .ToList();
                dgReports.ItemsSource = gefilterd;
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            // herlaad van de DB
            _allReports = YearReport.GetByCompanyId(_company.Id);
            CbYears_SelectionChanged(null!, null!);
        }
    }
}
