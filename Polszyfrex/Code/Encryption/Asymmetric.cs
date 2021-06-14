using System;
using System.Security.Cryptography;
using System.Text;

namespace Polszyfrex.Code.Encryption
{
    public sealed class Asymmetric: Code.Cipher
    {
        private int _dwKeySize = 2048;

        private string _publicKey;

        private string _privateKey;

        public int KeySize
        {
            get => this._dwKeySize;
            set => this._dwKeySize = value;
        }
        
        public string PublicKey
        {
            get => this._publicKey;
            set => this._publicKey = value;
        }

        public string PrivateKey
        {
            get => this._privateKey;
            set => this._privateKey = value;
        }

        public string[] GenerateKeys()
        {
            RSA rsaCrypto = RSA.Create();
            return new string[] {
                this.Base64Encode(rsaCrypto.ToXmlString(false)), //public
                this.Base64Encode(rsaCrypto.ToXmlString(true)) //private
            };
        }

        private string RSAEncrypt(string message)
        {
            string publicKey = this.Base64Decode(this._publicKey);

            byte[] byteMessage = Encoding.UTF8.GetBytes(message);
            using (RSACryptoServiceProvider rsaCrypto = new RSACryptoServiceProvider(this._dwKeySize))
            {
                try
                {                  
                    rsaCrypto.FromXmlString(publicKey.ToString());

                    return Convert.ToBase64String(rsaCrypto.Encrypt(byteMessage, true));
                }
                catch
                {
                    return "The operation was unsuccessful.";
                }
                finally
                {
                    rsaCrypto.PersistKeyInCsp = false;
                }
            }
        }

        private string RSADecrypt(string message)
        {
            string privateKey = this.Base64Decode(this._privateKey);

            using (RSACryptoServiceProvider rsaCrypto = new RSACryptoServiceProvider(this._dwKeySize))
            {
                try
                {
                    rsaCrypto.FromXmlString(privateKey);
                    byte[] decryptedBytes = rsaCrypto.Decrypt(Convert.FromBase64String(message), true);

                    return Encoding.UTF8.GetString(decryptedBytes).ToString();
                }
                catch
                {
                    return "The operation was unsuccessful.";
                }
                finally
                {
                    rsaCrypto.PersistKeyInCsp = false;
                }
            }
        }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private string Base64Decode(string encodedBase64)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodedBase64);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private bool IsBase64(string encodedBase64)
        {
            Span<byte> base64buffer = new Span<byte>(new byte[encodedBase64.Length]);
            if (!Convert.TryFromBase64String(encodedBase64, base64buffer, out int bytesParsed))
                return false;

            return true;
        }

        private bool CheckKeys()
        {
            if (string.IsNullOrEmpty(this._publicKey))
                return false;

            if (string.IsNullOrEmpty(this._privateKey))
                return false;

            if (!this.IsBase64(this._publicKey))
                return false;

            if (!this.IsBase64(this._privateKey))
                return false;

            return true;
        }

        public string Encrypt(string message)
        {
            if (!this.CheckKeys())
                return "The given keys are invalid.";

            return this.RSAEncrypt(message);
        }

        public string Decrypt(string message)
        {
            if (!this.CheckKeys())
                return "The given keys are invalid.";

            return this.RSADecrypt(message);
        }
    }
}
