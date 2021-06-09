using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Polszyfrex.Code.Encryption
{
    public sealed class Asymmetric: Code.Cipher
    {

        private string _publicKey;

        private string _privateKey;

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

        public string GenerateKey(int length, string pattern = "ULNS")
        {
            //U - upper case
            //L - lower case
            //N - numbers
            //S - special

            if (string.IsNullOrEmpty(pattern))
                pattern = "ULNS";

            pattern = pattern.ToUpper();

            string characters = "";
            if (pattern.Contains("U"))
                characters += "GHIJKLMNOPQRSTUVWXYZABCDEF";

            if (pattern.Contains("L"))
                characters += "abcdefghijklmnopqrstuvwxyz";

            if (pattern.Contains("N"))
                characters += "0123456789";

            if (pattern.Contains("S"))
                characters += "!@#$%^&*()_+-={}[];:,.<>?|~";

            Random random = new Random();
            return new string(Enumerable.Repeat(characters, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string RSAEncrypt(string message)
        {


            if (this._privateKey.Length < 16 || this._privateKey.Length > 32 || this._privateKey.Length % 4 != 0)
                return "The key must be between 16 and 32 characters long and divisible by 4.";

            byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(message);

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    rsa.FromXmlString(this._publicKey);
                    var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                catch
                {
                    return "The operation was unsuccessful.";
                }
            }
        }

        private string RSADecrypt(string message)
        {
            Span<byte> base64buffer = new Span<byte>(new byte[message.Length]);
            if (!Convert.TryFromBase64String(message, base64buffer, out int bytesParsed))
                return "The text to decode is not a valid BASE64 string.";

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {                 
                    rsa.FromXmlString(this._privateKey);

                    byte[] resultBytes = Convert.FromBase64String(message);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                catch
                {
                    return "The operation was unsuccessful.";
                }
            }
        }

        private void CheckKeys()
        {
            if (string.IsNullOrEmpty(this._publicKey))
                this._publicKey = this.GenerateKey(16);

            if (string.IsNullOrEmpty(this._privateKey))
                this._privateKey = this.GenerateKey(16);
        }


        public string Encrypt(string message)
        {
            this.CheckKeys();
            return this.RSAEncrypt(message);
        }

        public string Decrypt(string message)
        {
            this.CheckKeys();
            return this.RSADecrypt(message);
        }
    }
}
