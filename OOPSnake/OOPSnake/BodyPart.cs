using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OOPSnake
{
    class BodyPart
    {
        private int _X;
        

        public int X
        {
            get { return _X; }
            set { _X = value;
            _edge = new Rectangle(_X, _Y, _size, _size);
            }
        }

        private int _Y;


        public int Y
        {
            get { return _Y; }
            set { _Y = value;
            _edge = new Rectangle(_X, _Y, _size, _size);
            }
        }
        protected int _size;

        protected Rectangle _edge;

        public Rectangle Edge
        {
            get { return _edge; }
            set { _edge = value; }
        }
       

        public virtual void Draw (Graphics g)
        {
            _edge = new Rectangle(_X, _Y, _size, _size);
            g.FillRectangle(Brushes.Blue, _edge);
        }

        public void Clear(Graphics g)
        {
            _edge = new Rectangle(_X, _Y, _size, _size);
            g.FillRectangle(Brushes.White, _edge);
        }

        public BodyPart(int Size,int X, int Y)
        {
            _X=X;
            _Y=Y;
           _size=Size;
           _edge = new Rectangle(_X, _Y, _size, _size);
         }
        public BodyPart()
        {
        }
    }
}
