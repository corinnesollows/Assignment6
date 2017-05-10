//Corinne Jones-Hoyland && Melanie Damilig
//11/3/2015

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

public class Program: Form
{
        private int x, y;
        private int x2, y2;
        private Button suspend = new Button();
        private Button resume = new Button();
        private Button abort = new Button();
        Thread t;
        Thread t2;

        public Program()
        {
            BackColor = Color.White;
            Text = "Animate Ball";
            abort.Text = "Abort";
            suspend.Text = "Suspend";
            resume.Text = "Resume";

            int w = 0;
            suspend.Location = new Point(w, 0);
            resume.Location = new Point(w += 10 + suspend.Width, 0);
            abort.Location = new Point(w += 10 + resume.Width, 0);

            abort.Click += new EventHandler(Abort_Click);
            suspend.Click += new EventHandler(Suspend_Click);
            resume.Click += new EventHandler(Resume_Click);

            Controls.Add(suspend);
            Controls.Add(resume);
            Controls.Add(abort);

            x = 50; y = 50;
            x2 = 150; y2 = 150;
            t = new Thread(new ThreadStart(Run1));
            t2 = new Thread(new ThreadStart(Run2));
            t.Start();
            t2.Start();
        }
        protected void Abort_Click(object sender, EventArgs e)
        {
            t.Abort();
            t2.Abort();
        }
        protected void Suspend_Click(object sender, EventArgs e)
        {
            t.Suspend();
            t2.Suspend();
        }
        protected void Resume_Click(object sender, EventArgs e)
        {
            t.Resume();
            t2.Resume();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillEllipse(Brushes.BlueViolet, x, y, 40, 40);
            g.FillEllipse(Brushes.Coral, x2, y2, 70, 70);
            g.DrawLine(new Pen(Color.Black, 1), new Point(0, 40), new Point(this.Width, 40));
            base.OnPaint(e);           
        }
        public void Run1()
        {
            int dx = 7, dy = 7;

            while (true)
            {
                x += dx;
                y += dy;
                Invalidate();
                Thread.Sleep(100);
                if (x + 40 >= ClientRectangle.Width || x <= 0)
                {
                    dx = -dx;
                }
                if ((y + 40 >= ClientRectangle.Height || y <= 40))
                {
                    dy = -dy;
                }
                if (Math.Sqrt(Math.Pow(x2 - x, 2) + Math.Pow(y2 - y, 2)) <= 55)
                {
                    dx = -dx;
                    dy = -dy;
                }
            }
        }
        public void Run2()
        {
            int dx = 5, dy = 5;

            while (true)
            {
                x2 += dx;
                y2 += dy;
                Invalidate();
                Thread.Sleep(100);
                if (x2 + 70 >= ClientRectangle.Width || x2 <= 0)
                {
                    dx = -dx;
                }
                if ((y2 + 70 >= ClientRectangle.Height || y2 <= 40))
                {
                    dy = -dy;
                }
                if (Math.Sqrt(Math.Pow(x2 - x, 2) + Math.Pow(y2 - y, 2)) <= 55)
                {
                    dx = -dx;
                    dy = -dy;
                }
            }
        }
        public static void Main()
        {
            Application.Run(new Program());
        }
   }