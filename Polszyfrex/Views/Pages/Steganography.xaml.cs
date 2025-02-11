﻿using Microsoft.Win32;
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
        private Code.Encryption.Steganography _steganography;
        private BitmapImage _workFile;
        private BitmapImage _encryptedFile;

        public Steganography()
        {
            InitializeComponent();
            fieldImagePath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private void PreviewFromImage(string path)
        {
            this._workFile = new BitmapImage();

            this._workFile.BeginInit();
            this._workFile.UriSource = new Uri(path);
            this._workFile.EndInit();
            fieldImageBefore.Source = this._workFile;
        }

        private void Button_Select(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Polszyfrex - Steganography",
                Filter = "Image (*.png;*.jpg)|*.png;*.jpg;*.jpeg;*.bmp|All Files (*.*)|*.*",
                CheckPathExists = true,
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        };

            if (openFileDialog.ShowDialog() == true)
            {
                fieldImage.Text = fieldImagePath.Text = openFileDialog.FileName;
                this.PreviewFromImage(openFileDialog.FileName);
#if DEBUG
                System.Diagnostics.Debug.WriteLine(openFileDialog.FileName);
#endif
            }
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            if (this._encryptedFile == null)
                return;

            using (FileStream stream = new FileStream(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "polszyfrex.png").ToString(), FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(this._encryptedFile));
                encoder.Save(stream);
            }
        }

        private void Button_Encrypt(object sender, RoutedEventArgs e)
        {
            if (this._workFile == null)
                return;

            this._steganography = new Code.Encryption.Steganography();
            this._encryptedFile = this._steganography.EncryptText(this._workFile, fieldMessage.Text);
            
            fieldImageAfter.Source = this._encryptedFile;
        }

        private void Button_Decrypt(object sender, RoutedEventArgs e)
        {
            if (this._workFile == null)
                return;

            this._steganography = new Code.Encryption.Steganography();
            fieldMessage.Text = this._steganography.DecryptText(this._workFile);
        }
    }
}
