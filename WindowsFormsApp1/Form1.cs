using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Graphics g;
        Color colorA = Color.FromArgb(255, 0, 0);
        Color colorB = Color.FromArgb(0, 255, 0);
        Color colorC = Color.FromArgb(0, 0, 255);
        Color colorR;
        Color midleCA, midleCB, midleCC;
        


        PointF a, b, c, r;
        PointF midleA, midleB, midleC;

        int k1, k2, k3;

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            morphing((float)trackBar1.Value / (float)trackBar1.Maximum);
            drawMidle();
        }

        int R, G, B;

        private void Расчитать_Click(object sender, EventArgs e)
        {
            takeKValue();
            newColorMath();
            copyAll();

            drawPoints();
            trackBar1.Enabled = true;

            g.Clear(panel.BackColor);
            trackBar1.Value = 0;
        }

        void copyAll()
        {
            midleA.X = a.X;
            midleA.Y = a.Y;

            midleB.X = b.X;
            midleB.Y = b.Y;

            midleC.X = c.X;
            midleC.Y = c.Y;

            midleCA = colorA;
            midleCB = colorB;
            midleCC = colorC;

        }


        void newColorMath()
        {
            int k = k1 + k2 + k3;

            int R = (k1 * (int)colorA.R) / k + (k2 * (int)colorB.R) / k + (k3 * (int)colorC.R) / k;
            int G = (k1 * (int)colorA.G) / k + (k2 * (int)colorB.G) / k + (k3 * (int)colorC.G) / k;
            int B = (k1 * (int)colorA.B) / k + (k2 * (int)colorB.B) / k + (k3 * (int)colorC.B) / k;

            colorR = Color.FromArgb(R,G,B);

        }

        public Form1()
        {
            InitializeComponent();
            g = panel.CreateGraphics();
            creatPoints();

        }

        void creatPoints()
        {
            a.Y = panel.Height / 2;
            a.X = 0;

            b.X = panel.Width / 2;
            b.Y = 0;

            c.X = panel.Width;
            c.Y = panel.Height / 2;

            r.X = panel.Width / 2;
            r.Y = panel.Height / 2;
        }


        void drawPoints()
        {
            g.FillEllipse(new SolidBrush(midleCA), a.X - 10, a.Y - 10, 20, 20);
            g.FillEllipse(new SolidBrush(midleCB), b.X - 10, b.Y - 10, 20, 20);
            g.FillEllipse(new SolidBrush(midleCC), c.X - 10, c.Y - 10, 20, 20);
            g.FillEllipse(new SolidBrush(colorR), r.X - 10, r.Y - 10, 20, 20);
        }

        void drawMidle()
        {
            g.FillEllipse(new SolidBrush(midleCA), midleA.X - 10, midleA.Y - 10, 20, 20);
            g.FillEllipse(new SolidBrush(midleCB), midleB.X - 10, midleB.Y - 10, 20, 20);
            g.FillEllipse(new SolidBrush(midleCC), midleC.X - 10, midleC.Y - 10, 20, 20);
        }

        void takeKValue()
        {
            k1 = Convert.ToInt32(textBox1.Text);
            k2 = Convert.ToInt32(textBox2.Text);
            k3 = Convert.ToInt32(textBox3.Text);
        }

        void morphing(float t)
        {
            midleA.X = (1 - t) * a.X + t * r.X;
            midleA.Y = (1 - t) * a.Y + t * r.Y;

            midleB.X = (1 - t) * b.X + t * r.X;
            midleB.Y = (1 - t) * b.Y + t * r.Y;

            midleC.X = (1 - t) * c.X + t * r.X;
            midleC.Y = (1 - t) * c.Y + t * r.Y;

            R = (int)((1f - t) * float.Parse(Convert.ToString(colorA.R)) + t * float.Parse(Convert.ToString(colorR.R)));
            G = (int)((1f - t) * float.Parse(Convert.ToString(colorA.G)) + t * float.Parse(Convert.ToString(colorR.G)));
            B = (int)((1f - t) * float.Parse(Convert.ToString(colorA.B)) + t * float.Parse(Convert.ToString(colorR.B)));

            midleCA = Color.FromArgb(R, G, B);

            R = (int)((1f - t) * float.Parse(Convert.ToString(colorB.R)) + t * float.Parse(Convert.ToString(colorR.R)));
            G = (int)((1f - t) * float.Parse(Convert.ToString(colorB.G)) + t * float.Parse(Convert.ToString(colorR.G)));
            B = (int)((1f - t) * float.Parse(Convert.ToString(colorB.B)) + t * float.Parse(Convert.ToString(colorR.B)));

            midleCB = Color.FromArgb(R, G, B);

            R = (int)((1f - t) * float.Parse(Convert.ToString(colorC.R)) + t * float.Parse(Convert.ToString(colorR.R)));
            G = (int)((1f - t) * float.Parse(Convert.ToString(colorC.G)) + t * float.Parse(Convert.ToString(colorR.G)));
            B = (int)((1f - t) * float.Parse(Convert.ToString(colorC.B)) + t * float.Parse(Convert.ToString(colorR.B)));

            midleCC = Color.FromArgb(R, G, B);


        }


    }
}
