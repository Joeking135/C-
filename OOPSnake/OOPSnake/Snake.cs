using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OOPSnake
{
    class Snake
    {
        private HeadPart Head;
        private List<BodyPart> body = new List<BodyPart>();
        private int _width;
        private int _length;
        private BodyPart Tail;

        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }

        private int _xvelocity;

        public int Xvelocity
        {
          get { return _xvelocity; }
          set { _xvelocity = value; }
        }
                private int _yvelocity;

        public int Yvelocity
        {
          get { return _yvelocity; }
          set { _yvelocity = value; }
        }

        private int _velocity;

        public int Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Snake(int width, int length)
        {
            _width = width;
            _length = length;
            _velocity = width;
            _xvelocity =width;
            _yvelocity=0;
            Head=new HeadPart(_width,200+width,100);
            int i;
            for (i = 0; i < _length; i++)
            {
                BodyPart c = new BodyPart(_width,200-(i*width),100);
                body.Add(c);
            }
            Tail = new BodyPart(_width, 200 - (i * width), 100);
        }

        public void Draw(Graphics g)
        {
            Head.Draw(g);
            foreach (BodyPart c in body)
	        {
                c.Draw(g);
            }
            Tail.Clear(g);
	    }
        public void Grow(ref List<Food> FoodonScreen, Graphics g) //foodlist passed
        {
            int i=0 ;//need to Food ID
            foreach (Food Food in FoodonScreen.OfType<Food>())
            {
                if (Head.Edge.IntersectsWith(Food.Edge))
                {
                    //eaten food - create one new body part and add to snake.
                    for (int f = 0; f < Food.Value; f++)
                    {
                        BodyPart p = new BodyPart(_width, body[_length - 1].X - _xvelocity, body[_length - 1].Y - _yvelocity);
                        body.Add(p);
                        _length++;
                    }
                    //remove this food item from the screen.
                    Food.Clear(g);
                    FoodonScreen.RemoveAt(i);
                    break;
                }
                i++;
            }
        }

        public void Move(int formwidth, int formheight)
        {
            //move body parts along working from back
            Tail.X = body[body.Count - 1].X;
            Tail.Y = body[body.Count - 1].Y;
            for (int i = body.Count-1; i >0 ; i--)
            {
                body[i].X = body[i-1].X;
                body[i].Y = body[i - 1].Y;
            }
            body[0].X = Head.X;
            body[0].Y = Head.Y;
            //move head - with wrap around
            Head.X = (Head.X + _xvelocity) % formwidth;
            Head.Y = (Head.Y + _yvelocity) % formheight;
            if (Head.X <= 0) {Head.X = formwidth; }
            if (Head.Y <= 0) {Head.Y=formheight;}
            
           
        }

        public Boolean Crashed()
        {
            foreach (BodyPart bodypart in body)
            {
                if (Head.Edge.IntersectsWith(bodypart.Edge))
                {
                    return true;
                }
            }
          
            return false;
        }


    }
}
