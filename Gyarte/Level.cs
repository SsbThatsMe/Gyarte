using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyarte
{
    public class Level
    {
        public string name, description, code, text;
        public IEncryption encryption;
        public string[] avaliableCommands;

        public Level(string name, string description, IEncryption encryption, string code, string[] avaliableCommands)
        {
            this.name = name;
            this.description = description;
            this.encryption = encryption;
            this.code = code;
            this.avaliableCommands = avaliableCommands;
        }

        public void Initialize()
        {
            text = encryption.Encrypt("abcdefg");
        }

        public void Draw(Graphics g)
        {
            Font font = new Font("Arial", 20, FontStyle.Regular);
            g.DrawString(text, font, Brushes.Black, 10, 10);
        }
    }
}
