//Welcome to my program, this program creates Serpinski's Triangle.
//Feel free to look through it and message me if you find things that can be improved.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Need to use this for the timer
using System.Diagnostics;

namespace Infinite_Triforce
{
    public partial class infiniteFractals : Form
    {
        public infiniteFractals()
        {
            InitializeComponent();
        }

        #region Main Code
        Bitmap fractalsBitmap = new Bitmap(500, 500);

        private void FractalPictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(fractalsBitmap, 0, 0);
        }

        private void CreateFractal_Click(object sender, EventArgs e)
        {
            //Initial values of points and other variables
            Point A = new Point(Convert.ToInt32(pointAXCoordinateTextBox.Text), Convert.ToInt32(pointAYCoordinateTextBox.Text));
            Point B = new Point(Convert.ToInt32(pointBXCoordinateTextBox.Text), Convert.ToInt32(pointBYCoordinateTextBox.Text));
            Point C = new Point(Convert.ToInt32(pointCXCoordinateTextBox.Text), Convert.ToInt32(pointCYCoordinateTextBox.Text));
            Point[] mainTrianglePoints = { A, B, C };
            int Depth = 1;
            Stopwatch stopwatch = new Stopwatch();

            //Makes initial triangle and clears any previous triangles made
            Graphics mainTriangle = Graphics.FromImage(fractalsBitmap);
            mainTriangle.Clear(Color.Turquoise); //Deletes previous triangles and sets the background colour
            mainTriangle.FillPolygon(Brushes.Gold, mainTrianglePoints); // Creates the initial triangle
            fractalPictureBox.Refresh();
            mainTriangle.Dispose();

            //Creates the empty triangle and starts the timer
            stopwatch.Start();
            MakeFractal(A, B, C, Depth);
            stopwatch.Stop();
            timeLabel.Text = "Time Elapsed: " + stopwatch.ElapsedMilliseconds.ToString() + "ms";
        }

        private void MakeFractal(Point A, Point B, Point C, int Depth)
        {
            //Controls the depth of blank triangles made
            if (Depth > Convert.ToInt32(depthTextBox.Text))
            {
                
            }
            else
            {
                //Finds new points of the blank triangles
                Depth++;
                Point D = new Point((A.X + B.X) / 2, (A.Y + B.Y) / 2);
                Point E = new Point((B.X + C.X) / 2, (B.Y + C.Y) / 2);
                Point F = new Point((C.X + A.X) / 2, (C.Y + A.Y) / 2);
                Point[] whiteTrianglePoints = { D, E, F };

                //Draws the empty triangle
                Graphics whiteTriangleFractal = Graphics.FromImage(fractalsBitmap);
                whiteTriangleFractal.FillPolygon(Brushes.Turquoise, whiteTrianglePoints);
                fractalPictureBox.Refresh();
                whiteTriangleFractal.Dispose();

                //Recursion
                MakeFractal(A, D, F, Depth);
                MakeFractal(F, E, C, Depth);
                MakeFractal(D, B, E, Depth);
            }
        }
        #endregion

        #region Additional Features (i.e. Key Filtering, Resetting Values)
        //Stop the user from entering invalid text in the text boxes
        private void DepthTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            var eKeyValue = e.KeyValue;
            //Allowed Keys: Delete, Backspace, Left Arror, Right Arror, Number Row, and Numberp Pad
            if (eKeyValue == 8 || eKeyValue == 46 || eKeyValue == 37 || eKeyValue == 39 || (eKeyValue >= 48 && eKeyValue <= 57) || (eKeyValue >= 96 && eKeyValue <= 105))
            {
                e.SuppressKeyPress = false;
            }
        }

        //Incase the user wants to reset the values to their default value
        private void SetDefaultButton_Click(object sender, EventArgs e)
        {
            depthTextBox.Text = "0";
            pointAXCoordinateTextBox.Text = "250";
            pointAYCoordinateTextBox.Text = "100";
            pointBXCoordinateTextBox.Text = "375";
            pointBYCoordinateTextBox.Text = "350";
            pointCXCoordinateTextBox.Text = "125";
            pointCYCoordinateTextBox.Text = "350";
        }
        #endregion
    }
}
