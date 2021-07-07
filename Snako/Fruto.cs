using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snako
{
    class Fruto
    {
        private Rectangle rectangle;
        private Form1 Form;

        private int timerMax = 10;
        private int timer = 0;

        public Fruto(Form1 form)
        {
            Form = form;
            Rectangle = new Rectangle(new Random().Next(0, Form.Bounds.Width - 20), new Random().Next(0, Form.Bounds.Height - 48), 10, 10);
        }

        private void NewLocation()
        {
            rectangle.Location = new Point(new Random().Next(0, Form.Bounds.Width - 20), new Random().Next(0, Form.Bounds.Height - 48));
            foreach (Tailo t in Form.Snak.Tail)
            {
                if (t.Tail.IntersectsWith(Rectangle))
                {
                    NewLocation();
                    return;
                }
            }
        }

        public void execTimer()
        {
            timer++;
            if(timer >= timerMax)
            {
                timer = 0;
                NewLocation();
            }
        }

        public bool Eaten(Snakoo snakoo)
        {
            if (Rectangle.IntersectsWith(snakoo.Tail[0].Tail))
            {
                NewLocation();
                return true;
            }
            return false;
        }
        public Rectangle Rectangle { get => rectangle; private set => rectangle = value; }

    }
}
