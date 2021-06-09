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

namespace Polszyfrex.Views.Pages
{
    /// <summary>
    /// Interaction logic for Asymmetric.xaml
    /// </summary>
    public partial class Asymmetric : Page
    {
        private Code.Encryption.Asymmetric _asymmetric;

        public Asymmetric()
        {
            InitializeComponent();
            this._asymmetric = new Code.Encryption.Asymmetric();
        }

        private void Button_Copy(object sender, RoutedEventArgs e)
        {
            System.Windows.Clipboard.SetText(fieldResult.Text);
        }

        private void Button_Generate(object sender, RoutedEventArgs e)
        {
            fieldPublic.Text = this._asymmetric.GenerateKey(32);
            fieldPrivate.Text = this._asymmetric.GenerateKey(32);
        }

        private void Button_Encrypt(object sender, RoutedEventArgs e)
        {
            fieldResult.Text = this._asymmetric.Encrypt(fieldMessage.Text);
        }

        private void Button_Decrypt(object sender, RoutedEventArgs e)
        {
            fieldResult.Text = this._asymmetric.Decrypt(fieldMessage.Text);
        }
    }
}
