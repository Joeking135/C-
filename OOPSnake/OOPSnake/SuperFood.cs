using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPSnake
{
    public class SuperFood : Food
    {
        public SuperFood(int xpos, int ypos) : base(xpos, ypos)
        {
            X = xpos;
            Y = ypos;


            Value = 10;

            Edge = new Rectangle(X, Y, 10, 10);
        }


    }
}
