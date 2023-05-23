using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsters
{
    abstract class Character
    {
        private int _LastX;
        private int _LastY;

        protected int _PosX;

        public int PosX
        {
            get { return _PosX; }
        }
        
        protected int _PosY;

        public int PosY
        {
            get { return _PosY; }
        }

        protected ConsoleColor Colour;
        protected Char Image;

        public void display()
        {
            Console.SetCursorPosition(_LastX, _LastY);
            Console.Write(" ");
            Console.SetCursorPosition(_PosX,_PosY);
            Console.ForegroundColor=Colour;
            Console.Write(Image);
            Console.ForegroundColor=ConsoleColor.White;
        }

        public Character(int X, int Y, ConsoleColor C, Char I)
        {
            _PosX = X;
            _PosY = Y;
            Colour = C;
            Image = I;
        }

        public void move(direction D, int screenW, int screenH)
        {
            _LastX = PosX;
            _LastY = PosY;
            switch (D)
            {
                case direction.north:
                    if (_PosY > 1)
                    {
                        _PosY=_PosY-1;
                    }
                    break;

                case direction.south:

                    if (_PosY < screenH-1)
                    {
                        _PosY=_PosY+1;
                    }
                    break;

                case direction.east:
                    if (_PosX < screenW-1)
                    {
                        _PosX=_PosX+1;
                    }
                    break;

                case direction.west:
                    if (_PosX > 1)
                    {
                        _PosX=_PosX-1;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
