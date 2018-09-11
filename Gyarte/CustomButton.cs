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
    class CustomButton
    {
        public Rectangle rect;
        public string text, type, value;
        public string[] activeStates;

        [JsonConstructor]
        public CustomButton(Rectangle rect, string[] activeStates, string text, string type, string value)
        {
            this.rect = rect;
            this.text = text;
            this.type = type;
            this.activeStates = activeStates;
            this.value = value;
        }

        public bool Press()
        {
            if (Cursor.Position.X * Engine.relativeWidth > rect.X && Cursor.Position.X * Engine.relativeWidth< rect.X + rect.Width && Cursor.Position.Y * Engine.relativeHeight >= rect.Y &&
                    Cursor.Position.Y * Engine.relativeHeight <= rect.Y + rect.Height) return true;
            else return false;
        }

        public bool Update()
        {
            if (activeStates.Contains(Engine.currentState.ToString()) && Press())
            {
                if (type == "stateChanger")
                {
                    if (value == "mainMenu") Engine.currentState = Engine.States.mainMenu;
                    else if (value == "lvlBrowser") Engine.currentState = Engine.States.lvlBrowser;
                    else if (value == "playing") Engine.currentState = Engine.States.playing;
                    else if (value == "pause") Engine.currentState = Engine.States.pause;
                    else if (value == "closeGame") Application.Exit();
                }
                else if (type == "lvlSelect")
                {
                    if (int.TryParse(value, out Engine.currentLevel)) {
                        Engine.currentState = Engine.States.playing;
                        Engine.levels[Engine.currentLevel].Initialize();
                    }
                }
                return true;
            }
            else return false;
        }

        public void Draw(Graphics g)
        {
            if (activeStates.Contains(Engine.currentState.ToString()))
            {
                Font font = new Font("Arial", 20, FontStyle.Regular);
                if (Press()) g.FillRectangle(Brushes.LightGray, Engine.Relator(rect));
                else g.FillRectangle(Brushes.Gray, Engine.Relator(rect));
                RectangleF relatoredRect = Engine.Relator(rect);
                g.DrawString(text, font, Brushes.Black, relatoredRect, Engine.middleText);
                g.DrawRectangle(Pens.Black, Engine.Relator(rect));
            }
        }
    }
}
