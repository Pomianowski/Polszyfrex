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
    /// Interaction logic for Symmetric.xaml
    /// </summary>
    public partial class Symmetric : Page
    {
        private Code.Encryption.Symmetric _symmetric;

        public Symmetric()
        {
            InitializeComponent();
            this._symmetric = new Code.Encryption.Symmetric();
        }
        private void Button_Copy(object sender, RoutedEventArgs e)
        {
            System.Windows.Clipboard.SetText(fieldResult.Text);
        }

        private void Button_Generate(object sender, RoutedEventArgs e)
        {
            fieldKey.Text = this._symmetric.GenerateKey(32);
        }

        private void Button_Encrypt(object sender, RoutedEventArgs e)
        {
            fieldResult.Text = this._symmetric.Encrypt(fieldMessage.Text);
        }

        private void Button_Decrypt(object sender, RoutedEventArgs e)
        {
            fieldResult.Text = this._symmetric.Decrypt(fieldMessage.Text);
        }
    }
}
