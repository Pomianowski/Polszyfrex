using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Steganography.xaml
    /// </summary>
    public partial class Steganography : Page
    {
        public Steganography()
        {
            InitializeComponent();
        }

        private void Button_Select(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Polszyfrex - Steganography",
                Filter = "PNG (*.png)|*.7z;*.zip;*.rar|All Files (*.*)|*.*",
                CheckPathExists = true,
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        };

            if (openFileDialog.ShowDialog() == true)
            {
                fieldImage.Text = fieldImagePath.Text = openFileDialog.FileName;
#if DEBUG
                System.Diagnostics.Debug.WriteLine(openFileDialog.FileName);
#endif
            }
        }

        private void Button_Encrypt(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Decrypt(object sender, RoutedEventArgs e)
        {

        }
    }
}
