using System.Windows;
using System.Windows.Controls;

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
            this._asymmetric.PublicKey = fieldPublic.Text;
            this._asymmetric.PrivateKey = fieldPrivate.Text;

            fieldResult.Text = this._asymmetric.Encrypt(fieldMessage.Text);
        }

        private void Button_Decrypt(object sender, RoutedEventArgs e)
        {
            this._asymmetric.PublicKey = fieldPublic.Text;
            this._asymmetric.PrivateKey = fieldPrivate.Text;

            fieldResult.Text = this._asymmetric.Decrypt(fieldMessage.Text);
        }
    }
}
