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
using ClCompany;            // SessionContext

namespace WpfCompany
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = txtUsername.Text.Trim();
            string password = pbPassword.Password;

            AuthResult result = AuthenticationService.Login(login, password);
            if (!result.IsSuccess || result.IsAdmin)
            {
                MessageBox.Show(
                    "Onjuiste login of geen company-account.",
                    "Login mislukt",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
             
                    return;
            }

            // Sla rol en company op in sessie
            SessionContext.IsAdmin = false;
            SessionContext.CurrentCompany = result.Company!;

     
      
            // na succesvolle login:
            this.NavigationService.Navigate(
             new CompanyDashboardPage(result.Company!));


        }
    }
}
