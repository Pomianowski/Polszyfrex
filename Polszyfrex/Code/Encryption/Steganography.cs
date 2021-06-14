using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Polszyfrex.Code.Encryption
{
    public sealed class Steganography
    {
        private BitmapSource _imageSource;
        private WriteableBitmap _writeableBitmap;

        //Stride is pixel size in bytes. 
        private int _strideSize;

        public BitmapImage EncryptText(BitmapImage source, string message)
        {
            this._imageSource = source as BitmapSource;

            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine(this.ReadPixel(0, 0));
            System.Diagnostics.Debug.WriteLine(this.ReadPixel(1, 1));
            System.Diagnostics.Debug.WriteLine(this.ReadPixel(2, 2));
            System.Diagnostics.Debug.WriteLine(this.ReadPixel(10, 10));
            System.Diagnostics.Debug.WriteLine(this.ReadPixel(100, 100));
            System.Diagnostics.Debug.WriteLine(this.ReadPixel(500, 500));


            this.WritePixel(0, 0, Color.FromRgb(255,0,0));
            this.WritePixel(1, 1, Color.FromRgb(0, 255, 0));
            this.WritePixel(2, 2, Color.FromRgb(0, 0, 255));
            
            
            
            this._imageSource = this._writeableBitmap.ToBitmapSource();
            
            
            System.Diagnostics.Debug.WriteLine(this.ReadPixel(0, 0));
            System.Diagnostics.Debug.WriteLine(this.ReadPixel(1, 1));
            System.Diagnostics.Debug.WriteLine(this.ReadPixel(2, 2));

            return this._imageSource as BitmapImage;
        }

        public BitmapImage DecryptText(BitmapImage source, string message)
        {
            return new BitmapImage();
        }

        private void WritePixel(int x, int y, Color color)
        {
            byte[] ColorData = { color.B, color.G, color.R, 0 }; // B G R

            Int32Rect rect = new Int32Rect(
                    x,
                    y,
                    1,
                    1);

            //public void WritePixels (System.Windows.Int32Rect sourceRect, IntPtr buffer, int bufferSize, int stride);
            this._writeableBitmap.WritePixels(rect, ColorData, 4, 0);

            

            //this._writeableBitmap.WritePixels(new Int32Rect(0, 0, sourceWidth, sourceHeight), data, this._strideSize, 0);
            //this._writeableBitmap.WritePixels(new Int32Rect(0, 0, sourceWidth, sourceHeight), pixels, width * 4, x, y);
        }

        private Color ReadPixel(int x, int y)
        {
            this.VerifyWriteable();

            Color c = Colors.White;
            if (this._imageSource != null)
            {
                try
                {
                    CroppedBitmap cb = new CroppedBitmap(this._imageSource, new Int32Rect(x, y, 1, 1));
                    byte[] pixels = new byte[4];

                    cb.CopyPixels(pixels, 4, 0);
                    c = Color.FromRgb(pixels[2], pixels[1], pixels[0]);
                }
                catch (Exception) { }
            }
            return c;
        }

        private void VerifyWriteable()
        {
            if (this._imageSource.Format != PixelFormats.Bgra32)
                this._imageSource = new FormatConvertedBitmap(this._imageSource, PixelFormats.Bgra32, null, 0);

            if (this._writeableBitmap == null)
            {
                this._writeableBitmap = new WriteableBitmap(this._imageSource);
                this._strideSize = this._writeableBitmap.BackBufferStride;
            }
                
        }

    }
}
