using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polszyfrex.Code.Encryption
{
    public sealed class Symmetric: Code.Cipher
    {

        private string _key;

        public string Key
        {
            get => this._key;
            set => this._key = value;
        }

        public string GenerateKey(int length, string pattern = "ULNS")
        {
            if (string.IsNullOrEmpty(pattern))
                pattern = "ULNS";

            pattern = pattern.ToUpper();

            string characters = "";
            if (pattern.Contains("U"))
            {
                characters += "GHIJKLMNOPQRSTUVWXYZABCDEF";
            }

            if (pattern.Contains("L"))
            {
                characters += "abcdefghijklmnopqrstuvwxyz";
            }

            if (pattern.Contains("N"))
            {
                characters += "0123456789";
            }

            if (pattern.Contains("S"))
            {
                characters += "!@#$%^&*()_+-={}[];:,.<>?|~";
            }

            Random random = new Random();
            return new string(Enumerable.Repeat(characters, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string Encrypt(string message)
        {
            if (string.IsNullOrEmpty(this._key))
                this._key = this.GenerateKey(16);

            return message + this._key;
        }

        public string Decrypt(string message)
        {
            if (string.IsNullOrEmpty(this._key))
                this._key = this.GenerateKey(16);

            return message;
        }
    }
}
