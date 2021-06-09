using System;

namespace Polszyfrex.Code.Encryption
{
    public enum Characters
    {
        Letters,
        ASCII,
        UTF8_2,
        UTF8_3,
        UTF8_4
    }

    sealed public class Caesar: Code.Cipher
    {
        private Characters _type;

        private int _mapLength = 0;
        private int _shift = 0;

        public Caesar(Characters characters = Characters.Letters)
        {
            this._type = characters;
            this.SetLength();
        }

        private void SetLength()
        {
            switch (this._type)
            {
                case Characters.Letters:
                    this._mapLength = 26; //Only US letters
                    break;
                case Characters.ASCII:
                    this._mapLength = 128; //US-ASCII
                    break;
                case Characters.UTF8_2:
                    this._mapLength = 2048; //UTF-8 U+0080
                    break;
                case Characters.UTF8_3:
                    this._mapLength = 65536; //UTF-8 U+0800
                    break;
                case Characters.UTF8_4:
                    this._mapLength = 2097152; //UTF-8 U+10000
                    break;
            }
        }

        public int Shift
        {
            get => this._shift;
            set => this._shift = value;
        }

        public void TrySetShift(string? value)
        {
            Int32.TryParse(value, out this._shift);
        }

        private char ShiftLetter(char character)
        {
            char startChar = '!'; //0021 first non-blank character

            if (this._type == Characters.Letters)
            {
                if (!char.IsLetter(character))
                    return character;

                startChar = char.IsUpper(character) ? 'A' : 'a';
            }

            return (char)(((character + this._shift - startChar) % this._mapLength) + startChar);
        }

        public string Encrypt(string message)
        {
            string output = string.Empty;

            foreach (char ch in message)
                output += this.ShiftLetter(ch);

            return output;
        }

        public string Decrypt(string message)
        {
            this._shift = this._mapLength - this._shift;
            
            if (this._shift < 0)
                this._shift *= -1;

            return this.Encrypt(message);
        }
    }
}
