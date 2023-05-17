using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OOPSnake
{
    class HeadPart : BodyPart 
    {
        // add the missing attributes, methods and constructor


        public override void Draw(Graphics g)
        {
            _edge = new Rectangle(X, Y, _size, _size);
            g.FillRectangle(Brushes.Red, _edge);
        }

        public HeadPart()
        {
            
        }
        public HeadPart(int size,int x, int y)
        {
            X=x;
            Y=y;
           _size=size;
           _edge = new Rectangle(X, Y, _size, _size);
         }
    }
}
