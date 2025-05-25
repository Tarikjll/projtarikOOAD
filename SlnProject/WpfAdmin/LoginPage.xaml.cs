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
using ClCompany;               // AuthenticationService, AuthResult, SessionContext
using WpfAdmin;         // voor AdminMainPage
using WpfCompany;             // voor CompanyDashboardPage
    // of de namespace waarin CompanyDashboardPage staat

namespace WpfAdmin
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string password = pbPassword.Password;

            AuthResult result = AuthenticationService.Login(user, password);

            if (!result.IsSuccess)
            {
                MessageBox.Show(
                    "Onjuiste gebruikersnaam of wachtwoord.",
                    "Login mislukt",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            // Opslaan in session
            SessionContext.IsAdmin = result.IsAdmin;
            SessionContext.CurrentCompany = result.Company;

            // Navigeer naar de juiste dashboard
            if (result.IsAdmin)
            {
                // Admin-view in WpfAdmin
                this.NavigationService.Navigate(new AdminMainPage());
            }
            else
            {
                // Company-view uit WpfCompany
                this.NavigationService.Navigate(
                    new CompanyDashboardPage(result.Company!));
            }
        }
    }
}
