using MaterialWPF.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Polszyfrex.Views
{
    /// <summary>
    /// Interaction logic for Container.xaml
    /// </summary>
    public partial class Container : Window
    {
        public Container()
        {
            InitializeComponent();

            this.InitializeSplash();
            this.InitializeNavigation();
            this.HideSplash();
        }

        private void InitializeSplash()
        {
            rootSplash.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            rootSplash.SubTitle = "Loading encrypted encryption...";
            rootSplash.Logo = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/Assets/Images/polszyfrex-banner.png"));
        }

        private void InitializeNavigation()
        {

            rootNavigation.Items = new ObservableCollection<NavItem>
            {
                new NavItem { Icon = MaterialIcon.GridView, Name = "Dashboard", Tag = "Dashboard", Type = typeof(Pages.Dashboard)},
            };

            rootNavigation.Footer = new ObservableCollection<NavItem>
            {
                new NavItem { Icon = MaterialIcon.LockFeedback, Name = "About", Tag = "About", Type = typeof(Pages.About) }
            };

            rootNavigation.Frame = rootFrame;
            rootNavigation.Navigate("Dashboard");
        }

        private async void HideSplash()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(2000);

                App.Current.Dispatcher.Invoke(() =>
                {
                    rootGrid.Children.Remove(rootGrid.FindName("rootSplash") as UIElement);
                    rootLogo.Visibility = Visibility.Visible;
                });
            });
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                App.Current.MainWindow.DragMove();
        }
    }
}
