# Polszyfrex
Program for passing laboratories in the field of computer systems security, studies

```c#
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
```

```c#
sealed public class Atbash : Code.Cipher
{
    private string ShiftMessage(string message)
    {
        //A = /65
        //Z = /90
        //a = /97
        //z = /122
        int shift_A = Convert.ToByte('A') - 1;
        int shift_Z = Convert.ToByte('Z') + 1;

        int shift_a = Convert.ToByte('a') - 1;
        int shift_z = Convert.ToByte('z') + 1;

        char[] charMap = message.ToCharArray();

        for (int i = 0; i < charMap.Length; i++)
        {
            if (charMap[i] >= 'A' && charMap[i] <= 'Z')
            {
                charMap[i] = (char)(shift_A + (shift_Z - charMap[i]));
            }
            else if(charMap[i] >= 'a' && charMap[i] <= 'z')
            {
                charMap[i] = (char)(shift_a + (shift_z - charMap[i]));
            }
        }

        return new String(charMap);
    }

    public string Encrypt(string message)
    {
        return this.ShiftMessage(message);
    }

    public string Decrypt(string message)
    {
        return this.ShiftMessage(message);
    }
}
```