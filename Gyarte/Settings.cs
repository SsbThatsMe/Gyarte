using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace Gyarte
{
    class Settings
    {
        public string backKey;
        public bool fullscreen;

        public Settings(bool Fullscreen, string BackKey)
        {
            this.backKey = BackKey;
            this.fullscreen = Fullscreen;
        }

        public void Save() {
            string JSONsettings = JsonConvert.SerializeObject(this);
            File.WriteAllText("Settings.json", JSONsettings);
        }
    }
}
