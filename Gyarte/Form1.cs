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
    public partial class Engine : Form
    {
        static CustomButton[] buttons;
        static List<KeyEventArgs> pressedKeys = new List<KeyEventArgs>();
        static Timer t = new Timer(); 
        static Settings settings;
        public static double relativeHeight, relativeWidth, gameHeight, gameWidth, clientHeight, clientWidth;
        public static StringFormat middleText = new StringFormat();
        static bool setup;
        public enum States {
            mainMenu,
            lvlBrowser,
            playing,
            pause
        }
        public static States currentState;
        public static Level[] levels;
        public static int currentLevel;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!pressedKeys.Contains(e))
            {
                pressedKeys.Add(e);
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (pressedKeys.Contains(e))
            {
                pressedKeys.Remove(e);
            }
        }
        private void KeyUse() {
            string[] pressedStrings = new string[pressedKeys.Count];
            for (int i = 0; i < pressedKeys.Count; i++) pressedStrings[i] = pressedKeys[i].KeyCode.ToString();
            if (pressedStrings.Contains(settings.backKey)) Application.Exit();
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].Update()) return;
            }
        }

        public static Rectangle Relator(Rectangle input)
        {
            return new Rectangle(Convert.ToInt32(input.X / relativeWidth), Convert.ToInt32(input.Y / relativeHeight),
                Convert.ToInt32(input.Width / relativeWidth), Convert.ToInt32(input.Height / relativeHeight));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }
        private void LoadFiles()
        {
            string JSONSettings = File.ReadAllText("Settings.json", Encoding.UTF7);
            settings = JsonConvert.DeserializeObject<Settings>(JSONSettings);
            string JSONButtons = File.ReadAllText("Buttons.json", Encoding.UTF7);
            buttons = JsonConvert.DeserializeObject<CustomButton[]>(JSONButtons);

            levels = new Level[1];
            string[] coms = new string[] { "asdf" };
            levels[0] = new Level("Level 1", "This is the first level, it contains a ceasar cipher", new CaesarCipher(5), "You just beat the first level!", coms);
        }

        public Engine()
        {
            DoubleBuffered = true;
            setup = true;

            LoadFiles();

            gameHeight = 90;
            gameWidth = 160;

            currentState = States.mainMenu;
            InitializeComponent();
            
            middleText.Alignment = StringAlignment.Center;
            middleText.LineAlignment = StringAlignment.Center;

            t.Interval = 16;
            t.Tick += T_Tick;
            t.Start();

            this.Paint += new PaintEventHandler(Painter);
        }
        private void T_Tick(object sender, EventArgs e)
        {
            KeyUse();
            if (setup) {
                clientHeight = ClientRectangle.Height;
                clientWidth = ClientRectangle.Width;
                relativeHeight = gameHeight / clientHeight;
                relativeWidth = gameWidth / clientWidth;
                this.Font = new Font("Arial", 35 / (float)relativeHeight, FontStyle.Bold);
                setup = false;
            }
            Refresh();
        }
        public void Painter(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (currentState == States.mainMenu)
            {
                g.FillRectangle(Brushes.LightBlue, ClientRectangle);
            }
            else if (currentState == States.lvlBrowser)
            {
                g.FillRectangle(Brushes.LemonChiffon, ClientRectangle);
            }
            else if (currentState == States.playing || currentState == States.pause)
            {
                levels[currentLevel].Draw(g);
            }
            else if (currentState == States.pause)
            {
                g.FillRectangle(Brushes.LemonChiffon, ClientRectangle);
            }
            if (!setup) {
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].Draw(g);
                }
            }
        }
    }
}