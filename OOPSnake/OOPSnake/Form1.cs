using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OOPSnake
{
    public partial class Form1 : Form
    {
       //form attributes
        private Snake S = new Snake(20, 10);
        private List<Food> FoodOnScreen=new List<Food>();
        private Boolean GameOver = false;
        private const int FORMWIDTH = 800;
        private const int FORMHEIGHT = 800;
        private const int FOODFREQUENCY = 5; //5% chance of food appearing at every tick
        Graphics g;
        public Form1()
        {
            //form constructor
            InitializeComponent();
            Width = FORMWIDTH;
            Height = FORMHEIGHT;
            Text = "Score:" + S.Length;
            BackColor = Color.White;
            this.DoubleBuffered = true;
            g = this.CreateGraphics();
        }


        private void GameTimer_Tick(object sender, EventArgs e)
        {
            
            //game loop
            
            if (!GameOver)
            {
                // make changes to game
                UpdateGame();
                // redraw the game screen
                DrawGame();
            }
          
        }

        private void UpdateGame()
        {
            Random r = new Random();

            //Add more food?
            if (r.Next(1, 100) < FOODFREQUENCY)
            {
                Food F = new Food(r.Next(1, FORMWIDTH - 20), r.Next(1, FORMHEIGHT - 20));
                FoodOnScreen.Add(F);
            }

            //check to see if food has been eaten
            S.Grow(ref FoodOnScreen, g);


            // decide if the snake has hit its own body
            if (!S.Crashed())
            {
                //Nope - move the snake
                S.Move(FORMWIDTH, FORMHEIGHT);
            }
            else
            {
                // Yep - display game over message and stop game
                GameOver = true;
                GameTimer.Enabled = false;
                MessageBox.Show("Game over");
            }
        }

        private void DrawGame()
        {
            
            //draw snake
            S.Draw(g);

            //draw food
            foreach (Food f in FoodOnScreen)
            {
                f.Draw(g);
            }

            //Draw score
            Text = "Score:" + S.Length;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Left:
             
                        S.Xvelocity = -S.Velocity;
                        S.Yvelocity = 0;
                  
                    break;
                case Keys.Right:
             
                        S.Xvelocity = S.Velocity;
                        S.Yvelocity = 0;
                 
                    break;
                case Keys.Up:
    
                        S.Yvelocity = -S.Velocity;
                        S.Xvelocity = 0;
              
                    break;
                case Keys.Down:

                        S.Yvelocity = S.Velocity;
                        S.Xvelocity = 0;
                 
                    break;
            }
        }
    }
}
