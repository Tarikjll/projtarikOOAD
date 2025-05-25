using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClCompany;  // Company

namespace WpfAdmin
{
    public partial class AdminMainPage : Page
    {
        // 1) meteen initialiseren om nullability-warnings te verhelpen
        private List<Company> _allCompanies = new List<Company>();

        public AdminMainPage()
        {
            InitializeComponent();

            LoadCompanies();
            cbStatusFilter.SelectedIndex = 0;  // standaard "Alle"
        }

        private void LoadCompanies()
        {
            _allCompanies = Company.GetAll();
            lbCompanies.ItemsSource = _allCompanies;
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadCompanies();
            ApplyFilter();
        }

        private void CbStatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            if (_allCompanies == null) return;

            string filter = (cbStatusFilter.SelectedItem as ComboBoxItem)?.Content as string;
            if (filter == "Alle")
                lbCompanies.ItemsSource = _allCompanies;
            else
                lbCompanies.ItemsSource = _allCompanies
                    .Where(c => c.Status.Equals(filter, StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }

        private void BtnDetails_Click(object sender, RoutedEventArgs e)
        {
            Company sel = (Company)lbCompanies.SelectedItem;
            if (sel == null)
            {
                MessageBox.Show("Selecteer eerst een bedrijf.", "Geen selectie",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            this.NavigationService.Navigate(new CompanyDetailPage(sel));
        }

        // 2) Handler toevoegen voor SelectionChanged op lbCompanies
        private void LbCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Details-knop automatisch in-/uitschakelen
            btnDetails.IsEnabled = lbCompanies.SelectedItem != null;
        }
    }
}