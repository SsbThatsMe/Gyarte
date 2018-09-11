using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyarte
{

    public interface IEncryption
    {
        string Encrypt(string input);
    }

    public class CaesarCipher : IEncryption {
        private int key;

        public CaesarCipher(int key)
        {
            this.key = key;
        }

        public string Encrypt(string input)
        {
            string output = "";
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            for (int i = 0; i < input.Length; i++)
            {
                int position = (characters.IndexOf(input[i]) + key) % characters.Length;
                output += characters[position];
            }
            return output;
        }
    }

    class Encryptions
    {

    }
}
