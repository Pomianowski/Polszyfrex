using MaterialWPF.UI;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
                new NavItem { Icon = MaterialIcon.DefenderApp, Name = "Caesar Cipher", Tag = "Caesar", Type = typeof(Pages.Caesar)},
                new NavItem { Icon = MaterialIcon.DefenderApp, Name = "Atbash Cipher", Tag = "Atbash", Type = typeof(Pages.Atbash)},
                new NavItem { Icon = MaterialIcon.DefenderApp, Name = "Symmetric Cipher", Tag = "Symmetric", Type = typeof(Pages.Symmetric)},
                new NavItem { Icon = MaterialIcon.DefenderApp, Name = "Asymmetric Cipher", Tag = "Asymmetric", Type = typeof(Pages.Asymmetric)},
                new NavItem { Icon = MaterialIcon.DefenderApp, Name = "Steganography", Tag = "Steganography", Type = typeof(Pages.Steganography)}
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
