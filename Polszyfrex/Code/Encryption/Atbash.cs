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
            int shift_A = Convert.ToByte('A') - 1;
            int shift_Z = Convert.ToByte('Z') + 1;

            int shift_a = Convert.ToByte('a') - 1;
            int shift_z = Convert.ToByte('z') + 1;

            int shift_zero = Convert.ToByte('0') - 1;
            int shift_nine = Convert.ToByte('9') + 1;

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
                else if (charMap[i] >= '0' && charMap[i] <= '9')
                {
                    charMap[i] = (char)(shift_zero + (shift_nine - charMap[i]));
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
