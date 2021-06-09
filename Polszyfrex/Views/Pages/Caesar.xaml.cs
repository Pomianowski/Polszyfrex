using System.Windows;
using System.Windows.Controls;

namespace Polszyfrex.Views.Pages
{
    /// <summary>
    /// Interaction logic for Caesar.xaml
    /// </summary>
    public partial class Caesar : Page
    {
        private Code.Encryption.Caesar _caesar;

        public Caesar()
        {
            InitializeComponent();
            this._caesar = new Code.Encryption.Caesar();
        }

        private void Button_Copy(object sender, RoutedEventArgs e)
        {
            System.Windows.Clipboard.SetText(fieldResult.Text);
        }

        private void Button_Encrypt(object sender, RoutedEventArgs e)
        {
            this._caesar.TrySetShift(fieldShift.Text);
            fieldResult.Text = this._caesar.Encrypt(fieldMessage.Text);
        }

        private void Button_Decrypt(object sender, RoutedEventArgs e)
        {
            this._caesar.TrySetShift(fieldShift.Text);
            fieldResult.Text = this._caesar.Decrypt(fieldMessage.Text);
        }
    }
}
