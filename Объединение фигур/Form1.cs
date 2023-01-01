using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Объединение_фигур
{
    public partial class Morphing : Form
    {
        Graphics g;
        Random random = new Random();

        float k1 = 1;
        float k2 = 2;
        float k3 = 3;


        float[,] firstLine = new float[4, 2];
        float[,] secondLine = new float[4, 2];
        float[,] theardLine = new float[4, 2];

        float[,] midle1 = new float[4, 2];
        float[,] midle2 = new float[4, 2];
        float[,] midle3 = new float[4, 2];

        float[,] resultLine = new float[4, 2];



        public Morphing()
        {
            InitializeComponent();
            g = panel.CreateGraphics();

            fillLine(ref firstLine);
            fillLine(ref secondLine, 300);
            fillLine(ref theardLine, 610);

            copyValues(firstLine, midle1);
            copyValues(secondLine, midle2);
            copyValues(theardLine, midle3);

            
        }

        void takeProp()
        {
            try
            {
                k1 = float.Parse(textBox1.Text);
                k2 = float.Parse(textBox2.Text);
                k3 = float.Parse(textBox3.Text);
            }
            catch
            {
                MessageBox.Show("Не верный формат данных!");
                
            }
        }

        void copyValues(float[,] whatCopy, float[,] result)
        {
            for (int i = 0; i < whatCopy.GetLength(0); i++)
            {
                result[i, 0] = whatCopy[i, 0];
                result[i, 1] = whatCopy[i, 1];
            }
        }

        void fillLine(ref float[,] line, int k = 10)
        {

            for (int i = 0; i < line.GetLength(0); i++)
            {
                line[i, 0] = random.Next(1 + k, 200 + k);
                line[i, 1] = random.Next(10, 400);
            }
        }

        void drawAll()
        {

            drawDote(midle1, Color.Green);
            drawDote(midle2, Color.Blue);
            drawDote(midle3, Color.DeepPink);

            drawLines(midle1, Color.Green);
            drawLines(midle2, Color.Blue);
            drawLines(midle3, Color.DeepPink);




            drawDote(firstLine, Color.Black);
            drawDote(secondLine, Color.Black);
            drawDote(theardLine, Color.Black);
            drawDote(resultLine, Color.Red);

            drawLines(firstLine, Color.Black);
            drawLines(secondLine, Color.Black);
            drawLines(theardLine, Color.Black);
            drawLines(resultLine, Color.Red);
        }

        void drawDote(float[,] line, Color color)
        {

            Brush brush = new SolidBrush(color);

            for (int i = 0; i < line.GetLength(0); i++)
            {
                g.FillEllipse(brush, line[i, 0] - 10, line[i, 1] - 10, 20, 20);
            }
        }

        void drawLines(float[,] line, Color color)
        {
            Pen pen = new Pen(color, 2);

            for (int i = 0; i < line.GetLength(0) - 1; i++)
            {
                g.DrawLine(pen, line[i, 0], line[i, 1], line[i + 1, 0], line[i + 1, 1]);
            }

            g.DrawLine(pen, line[line.GetLength(0) - 1, 0], line[line.GetLength(0) - 1, 1], line[0, 0], line[0, 1]);
        }

        void mathResult(float[,] first, float[,] second, float[,] theard)
        {

            float n = k1 + k2 + k3;


            for (int i = 0; i < first.GetLength(0); i++)
            {
                resultLine[i, 0] = firstLine[i, 0] * k1 / n + secondLine[i, 0] * k2 / n + theardLine[i, 0] * k3 / n;
                resultLine[i, 1] = firstLine[i, 1] * k1 / n + secondLine[i, 1] * k2 / n + theardLine[i, 1] * k3 / n;
            }
        }

        void morphing(float t)
        {
            for (int i = 0; i < firstLine.GetLength(0); i++)
            {
                midle1[i, 0] = (1 - t) * firstLine[i, 0] + t * resultLine[i, 0];
                midle1[i, 1] = (1 - t) * firstLine[i, 1] + t * resultLine[i, 1];

                midle2[i, 0] = (1 - t) * secondLine[i, 0] + t * resultLine[i, 0];
                midle2[i, 1] = (1 - t) * secondLine[i, 1] + t * resultLine[i, 1];

                midle3[i, 0] = (1 - t) * theardLine[i, 0] + t * resultLine[i, 0];
                midle3[i, 1] = (1 - t) * theardLine[i, 1] + t * resultLine[i, 1];
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            float t = (float)trackBar1.Value / (float)trackBar1.Maximum;
            morphing(t);

            g.Clear(panel.BackColor);

            drawAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            takeProp();

            mathResult(firstLine, secondLine, theardLine);

            drawAll();
        }
    }
}
