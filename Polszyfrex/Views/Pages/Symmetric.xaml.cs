using System.Windows;
using System.Windows.Controls;

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

        private bool Verify()
        {
            if(string.IsNullOrEmpty(fieldKey.Text))
            {
                fieldResult.Text = "Key cannot be empty";
                return false;
            }
            return true;
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
            if (!this.Verify())
                return;

            this._symmetric.Key = fieldKey.Text;
            fieldResult.Text = this._symmetric.Encrypt(fieldMessage.Text);
        }

        private void Button_Decrypt(object sender, RoutedEventArgs e)
        {
            if (!this.Verify())
                return;

            this._symmetric.Key = fieldKey.Text;
            fieldResult.Text = this._symmetric.Decrypt(fieldMessage.Text);
        }
    }
}
