using System;
using System.Collections.Generic;
using System.Linq;
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

            return message + this._publicKey;
        }

        public string Decrypt(string message)
        {
            this.CheckKeys();

            return message;
        }
    }
}
