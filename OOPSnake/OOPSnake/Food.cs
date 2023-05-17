using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OOPSnake
{
    public interface IFood
    {
        void Draw(Graphics g);

        void Clear(Graphics g);
         
    }



    public class Food : IFood
    {
        // add the missing attributes and methods and constructor
        public int Value { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public Rectangle Edge { get; set; }

        public Food(int xpos, int ypos)
        {
            X = xpos;
            Y = ypos;

            Random rng = new Random();

            Value = rng.Next(1, 6);

            Edge = new Rectangle(X, Y, 10, 10);
        }


        public virtual void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Green, X, Y, 10, 10);
        }

        public virtual void Clear(Graphics g)
        {
            g.FillEllipse(Brushes.White, X, Y, 10, 10);
        }
    }
}
