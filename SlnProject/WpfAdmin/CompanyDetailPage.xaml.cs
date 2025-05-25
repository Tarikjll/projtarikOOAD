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
using System.Windows;
using System.Windows.Controls;
using ClCompany;

namespace WpfAdmin
{
    public partial class CompanyDetailPage : Page
    {
        private readonly Company _company;

        public CompanyDetailPage(Company company)
        {
            InitializeComponent();
            _company = company;
            this.DataContext = _company;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // _company.Status is al gebonden aan cbStatus
                _company.Update();
                MessageBox.Show("Status opgeslagen.", "OK",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                // Ga terug naar overzicht
                this.NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij opslaan: " + ex.Message,
                                "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}

