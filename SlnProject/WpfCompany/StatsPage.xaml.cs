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
using System.Collections.Generic;
using ClCompany;

namespace WpfCompany
{
    public class StatItem
    {
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
    }

    public partial class StatsPage : Page
    {
        public StatsPage( Company company)
        {
            InitializeComponent();

            // TODO: vervang dit door echte berekeningen
            List<StatItem> dummy = new List<StatItem>
            {
                new StatItem { Name = "Gem. FTE", Value = 12.3 },
                new StatItem { Name = "Gem. Kost/antwoord", Value = 45.6 }
            };

            dgStats.ItemsSource = dummy;
        }
    }
}
