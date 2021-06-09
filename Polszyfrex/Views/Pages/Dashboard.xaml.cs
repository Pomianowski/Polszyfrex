using System.Windows;
using System.Windows.Controls;

namespace Polszyfrex.Views.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine((sender as Button).Tag);
#endif
            (App.Current.MainWindow as Views.Container).rootNavigation.Navigate((sender as Button).Tag as string);
            
        }
    }
}
