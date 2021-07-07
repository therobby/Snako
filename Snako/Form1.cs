using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snako
{
    public partial class Form1 : Form
    {
        private SolidBrush fruit;
        private SolidBrush snakoTailo;
        private SolidBrush snakoHedo;
        private Snakoo snak;
        private Fruto fruto;
        private int score;

        public Form1()
        {
            InitializeComponent();
            fruit = new SolidBrush(Color.Red);
            snakoTailo = new SolidBrush(Color.Blue);
            snakoHedo = new SolidBrush(Color.BlueViolet);
            Snak = new Snakoo(new Point(400, 160), 10, this);
            fruto = new Fruto(this);
            score = 0;
        }

        ~Form1()
        {
            fruit.Dispose();
            snakoHedo.Dispose();
            snakoTailo.Dispose();
        }

        internal Snakoo Snak { get => snak; private set => snak = value; }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Tailo t in Snak.Tail)
            {
                if (Snak.Tail.IndexOf(t) == 0)
                    e.Graphics.FillEllipse(snakoHedo, t.Tail);
                else
                    e.Graphics.FillEllipse(snakoTailo, t.Tail);
            }
            e.Graphics.FillEllipse(fruit, fruto.Rectangle);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Keyboard.KeysPressed() > 0)
            {
                if (Keyboard.IsKeyDown(Keys.Escape))
                    Application.Exit();
                if (Keyboard.IsKeyDown(Keys.W) || Keyboard.IsKeyDown(Keys.S) || Keyboard.IsKeyDown(Keys.A) || Keyboard.IsKeyDown(Keys.D))
                {
                    if (Keyboard.IsKeyDown(Keys.W))
                    {
                        Snak.NewDirection(Direction.Up);
                    }
                    else if (Keyboard.IsKeyDown(Keys.S))
                    {
                        Snak.NewDirection(Direction.Down);
                    }
                    else if (Keyboard.IsKeyDown(Keys.A))
                    {
                        Snak.NewDirection(Direction.Left);
                    }
                    else if (Keyboard.IsKeyDown(Keys.D))
                    {
                        Snak.NewDirection(Direction.Right);
                    }
                }
            }

            Snak.Move();
            if (!Snak.Alive)
            {
                Tick.Stop();
                MessageBox.Show("Score: " + score, "Game over!", MessageBoxButtons.OK);
                Application.Exit();
            }
            if (fruto.Eaten(snak))
            {
                score++;
                Snak.Add();
            }
            Refresh();
        }




        public static class Keyboard
        {
            private static readonly HashSet<Keys> keys = new HashSet<Keys>();

            public static void OnKeyDown(object sender, KeyEventArgs e)
            {
                if (keys.Contains(e.KeyCode) == false)
                {
                    keys.Add(e.KeyCode);
                }
            }

            public static void OnKeyUp(object sender, KeyEventArgs e)
            {
                if (keys.Contains(e.KeyCode))
                {
                    keys.Remove(e.KeyCode);
                }
            }

            public static bool IsKeyDown(Keys key)
            {
                return keys.Contains(key);
            }

            public static int KeysPressed() => keys.Count();
        }
    }
}
