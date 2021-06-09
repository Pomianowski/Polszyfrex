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
    /// Interaction logic for Atbash.xaml
    /// </summary>
    public partial class Atbash : Page
    {
        private Code.Encryption.Atbash _atbash;

        public Atbash()
        {
            InitializeComponent();
            this._atbash = new Code.Encryption.Atbash();
        }

        private void Button_Copy(object sender, RoutedEventArgs e)
        {
            System.Windows.Clipboard.SetText(fieldResult.Text);
        }

        private void Button_Encrypt(object sender, RoutedEventArgs e)
        {
            fieldResult.Text = this._atbash.Encrypt(fieldMessage.Text);
        }

        private void Button_Decrypt(object sender, RoutedEventArgs e)
        {
            fieldResult.Text = this._atbash.Decrypt(fieldMessage.Text);
        }
    }
}
