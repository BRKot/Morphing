using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Морфинг
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        Graphics midle;

        float[,] firstLine = new float[4, 2];
        float[,] secondLine = new float[4, 2];
        float[,] midleLine = new float[4, 2];

        public Form1()
        {
            InitializeComponent();


            fillLine(ref firstLine, 50);
            fillLine(ref secondLine, 700);


            copyValues(firstLine, midleLine);
        }

        void copyValues(float[,] whatCopy, float[,] result)
        {
            for(int i = 0; i < whatCopy.GetLength(0); i++)
            {
                result[i,0] = whatCopy[i, 0];
                result[i, 1] = whatCopy[i, 1];
            }
        }

        void fillLine(ref float[,] line, int k)
        {
           
            for (int i = 0; i < line.GetLength(0); i++)
            {
                line[i, 0] = random.Next(1 + k, 100 + k);
                line[i,1] = random.Next(20,600);
            }
        }

        private void panel_Paint_1(object sender, PaintEventArgs e)
        {
            this.midle = panel.CreateGraphics();
            drawDote(firstLine, e.Graphics, Color.Black);
            drawDote(secondLine, e.Graphics, Color.Black);
            drawLines(firstLine, e.Graphics, Color.Black);
            drawLines(secondLine, e.Graphics, Color.Black);
            conectStartWithEnd();
        }

        void drawDote(float[,] line, Graphics g, Color color)
        {   

            Brush brush = new SolidBrush(color);

            for (int i = 0; i < line.GetLength(0); i++)
            { 
                g.FillEllipse(brush, line[i, 0] - 10, line[i, 1]-10,20,20);
            }
        }

        void drawLines(float[,] line, Graphics g, Color color)
        {
            Pen pen = new Pen(color, 2);

            for (int i = 0; i < line.GetLength(0) - 1; i++)
            {
                g.DrawLine(pen, line[i, 0], line[i, 1], line[i + 1, 0], line[i + 1, 1]);
            }
        }

        void clearLines(float[,] line)
        {
            Brush brush = new SolidBrush(panel.BackColor);

            for (int i = 0; i < line.GetLength(0); i++)
            {
                midle.FillEllipse(brush, line[i, 0] - 10, line[i, 1] - 10, 20, 20);
            }

            Pen pen = new Pen(panel.BackColor, 2);

            for (int i = 0; i < line.GetLength(0) - 1; i++)
            {
                midle.DrawLine(pen, line[i, 0], line[i, 1], line[i + 1, 0], line[i + 1, 1]);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            float t = (float)trackBar1.Value / (float)trackBar1.Maximum;
            clearLines(midleLine);

            morphing(t);

            conectStartWithEnd();

            drawDote(midleLine, midle, Color.Green);
            drawLines(midleLine, midle, Color.Green);

            drawDote(firstLine, midle, Color.Black);
            drawLines(firstLine, midle, Color.Black);

            drawDote(secondLine, midle, Color.Black);
            drawLines(secondLine, midle, Color.Black);

            
        }

        void morphing(float t)
        {
            for(int i = 0; i < midleLine.GetLength(0); i++)
            {
                this.midleLine[i, 0] = (1 - t) * firstLine[i, 0] + t * secondLine[i, 0];
                this.midleLine[i, 1] = (1 - t) * firstLine[i, 1] + t * secondLine[i, 1];
            }
        }

        void conectStartWithEnd()
        {
            Pen pen = new Pen(Color.DarkGray);

            for (int i = 0; i < firstLine.GetLength(0); i++)
            {
                midle.DrawLine(pen, firstLine[i, 0], firstLine[i, 1], secondLine[i, 0], secondLine[i, 1]);
            }
        }
    }
}
