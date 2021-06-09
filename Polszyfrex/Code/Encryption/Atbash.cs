using System;

namespace Polszyfrex.Code.Encryption
{

    sealed public class Atbash : Code.Cipher
    {

        private string ShiftMessage(string message)
        {
            //A = /65
            //Z = /90
            //a = /97
            //z = /122
            int shit_A = Convert.ToByte('A') - 1;
            int shit_Z = Convert.ToByte('Z') + 1;

            int shit_a = Convert.ToByte('a') - 1;
            int shit_z = Convert.ToByte('z') + 1;

            char[] charMap = message.ToCharArray();

            for (int i = 0; i < charMap.Length; i++)
            {
                if (charMap[i] >= 'A' && charMap[i] <= 'Z')
                {
                    charMap[i] = (char)(shit_A + (shit_Z - charMap[i]));
                }
                else if(charMap[i] >= 'a' && charMap[i] <= 'z')
                {
                    charMap[i] = (char)(shit_a + (shit_z - charMap[i]));
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
}
