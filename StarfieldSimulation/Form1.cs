using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarfieldSimulation
{
    public partial class Form1 : Form
    {
        public class Star
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }
        }

        public Star[] stars = new Star[20000];
        public Random random = new Random();
        public Graphics graphics;

        public Form1()
        {
            InitializeComponent();
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);
            foreach (var star in stars)
            {
                DrawStar(star);
                MoveStar(star);
            }
            pictureBox1.Refresh();
        }

        public void MoveStar(Star star)
        {
            star.Z -= 15;
            if (star.Z < 1)
            {
                star.Z = random.Next(1, pictureBox1.Width);
                star.X = random.Next(-pictureBox1.Width, pictureBox1.Width);
                star.Y = random.Next(-pictureBox1.Height, pictureBox1.Height);
            }
        }

        public void DrawStar(Star star)
        {
            float starSize = Map(star.Z, 0, pictureBox1.Width, 9, 0);
            float x = Map(star.X / star.Z, 0, 1, 0, pictureBox1.Width) + pictureBox1.Width / 2;
            float y = Map(star.Y / star.Z, 0, 1, 0, pictureBox1.Height) + pictureBox1.Height / 2;
            graphics.FillEllipse(Brushes.GreenYellow, x, y, starSize, starSize);
        }

        public float Map(float n, float start1, float stop1, float start2, float stop2)
        {
            return ((n - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star()
                {
                    X = random.Next(-pictureBox1.Width, pictureBox1.Width),
                    Y = random.Next(-pictureBox1.Height, pictureBox1.Height),
                    Z = random.Next(1, pictureBox1.Width)
                };
            }
            timer1.Start();
        }

        public void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
