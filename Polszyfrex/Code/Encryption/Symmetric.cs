using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Polszyfrex.Code.Encryption
{
    public sealed class Symmetric : Code.Cipher
    {
        private string _key;

        public string Key
        {
            get => this._key;
            set => this._key = value;
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

        private string AesEncrypt(string message)
        {
            if (this._key.Length < 16 || this._key.Length > 32 || this._key.Length % 8 != 0)
                return "The key must be between 16 and 32 characters long and divisible by 8.";

            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(this._key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(message);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        private string AesDecrypt(string message)
        {
            if (this._key.Length < 16 || this._key.Length > 32 || this._key.Length % 8 != 0)
                return "The key must be between 16 and 32 characters long and divisible by 8.";

            Span<byte> base64buffer = new Span<byte>(new byte[message.Length]);
            if(!Convert.TryFromBase64String(message, base64buffer, out int bytesParsed))
                return "The text to decode is not a valid BASE64 string.";

            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(message);

            using (Aes aes = Aes.Create())
            {
                try
                {
                    aes.Key = Encoding.UTF8.GetBytes(this._key);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    return "Decoding failed, block size / padding is invalid";
                }
            }
        }

        private void CheckKeys()
        {
            if (string.IsNullOrEmpty(this._key))
                this._key = this.GenerateKey(32);
        }

        public string Encrypt(string message)
        {
            this.CheckKeys();
            return this.AesEncrypt(message);
        }

        public string Decrypt(string message)
        {
            this.CheckKeys();
            return this.AesDecrypt(message);
        }
    }
}
