using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;
//using Tao.Platform.Windows;

namespace Lab3
{
    public partial class Form1 : Form
    {
        PointF Target;
        private List<Fish> Fishes;
        int TickNo;
        float SizeX;
        float SizeY;
        Fish Leader;
        int FishesCount = 1500;
        bool Debugging = false;
        bool ShowLeaderAxis = true;
        bool ClearTarget = false;
        bool ShowLeader = false;
        bool ShowTarget = false;
        bool Hang = false;

        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            AnT.InitializeContexts();
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            Gl.glClearColor(255, 255, 255, 1);

            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();


            if (Debugging)
            {
                //if ((float)AnT.Width <= (float)AnT.Height)
                //{
                //    Glu.gluOrtho2D(0.0, 30.0 * (float)AnT.Height / (float)AnT.Width, 0.0, 30.0);
                //    SizeX = 30.0F * (float)AnT.Height / (float)AnT.Width;
                //}
                //else
                //{
                //    Glu.gluOrtho2D(0.0, 30.0 * (float)AnT.Width / (float)AnT.Height, 0.0, 30.0);
                //    SizeX = 30.0F * (float)AnT.Width / (float)AnT.Height;
                //}
                //SizeY = 30F;

                //Gl.glMatrixMode(Gl.GL_MODELVIEW);
                //Gl.glLoadIdentity();
                //timer1.Start();
                //Fishes = new List<Fish>();
                //Leader = new Fish(23, 15, 23, 15, 0, 0);
                //Random rnd = new Random();
                //for (int i = 0; i < FishesCount; i++)
                //{
                //    Fish a = new Fish((float)rnd.NextDouble() * SizeX, (float)rnd.NextDouble() * SizeY,
                //        (float)rnd.NextDouble() * (SizeX - 10) + 5, (float)rnd.NextDouble() * (SizeY - 10) + 5, 0, i); //rnd.NextDouble() * 180 - 90);
                //    Fishes.Add(a);
                //}
                //Target = new PointF((float)rnd.NextDouble() * (SizeX - 10) + 5, (float)rnd.NextDouble() * (SizeY - 10) + 5);
                //TickNo = 0;
            }
            else
            {
                Random rand = new Random();
                Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.1, 200);
                Gl.glMatrixMode(Gl.GL_MODELVIEW);
                Gl.glLoadIdentity();
                //Gl.glEnable(Gl.GL_DEPTH_TEST); 
                SizeX = 33.1F;
                SizeY = 20.4F;
                timer1.Start();
                Fishes = new List<Fish>();
                Leader = new Fish(0, 0, 0, 0, 0, 0, Color.Blue);
                Random rnd = new Random();
                for (int i = 0; i < FishesCount; i++)
                {
                    Fish a = new Fish(2 * ((float)rnd.NextDouble() - 0.5F) * (SizeX-2.5F), 2 * ((float)rnd.NextDouble() - 0.5F) * (SizeY -2.5F),
                        2 * ((float)rnd.NextDouble() - 0.5F) * (SizeX - 5), 2 * ((float)rnd.NextDouble() - 0.5F) * (SizeY - 10),
                        ((float)rnd.Next(0, 360) - 180), i, Color.FromArgb(1, rand.Next(0, 60), rand.Next(120, 254)));
                    Fishes.Add(a);
                }
                Target = new PointF(2 * ((float)rnd.NextDouble() - 0.5F) * (SizeX - 5), 2 * ((float)rnd.NextDouble() - 0.5F) * (SizeY - 5));
                TickNo = 0;
            }
        }
        private void DrawFish(int TaleState, Color col)
        {
            double size = 7;
            Gl.glBegin(Gl.GL_TRIANGLE_STRIP);
            Gl.glColor3f((float)col.R / 255, (float)col.G / 255, (float)col.B / 255);
            Gl.glVertex3d(-10 / size,   5 / size,   0);   //1
            Gl.glVertex3d(-10 / size,   0,          3 / size);    //2
            Gl.glVertex3d(-8 / size,    1 / size,   0);    //3
            Gl.glVertex3d(-7 / size,    0,          2 / size);    //4
            Gl.glVertex3d(0,            3 / size,   0);     //5
            Gl.glVertex3d(0,            0,          3 / size);     //6
            Gl.glVertex3d(3.5 / size,   2.2 / size, 0); //7
            Gl.glVertex3d(3.5 / size,   0,          3 / size);   //8
            Gl.glVertex3d(5 / size,     0,          0);     //9
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLE_STRIP);
            Gl.glVertex3d(-10 / size, 5 / size, 0);   //1
            Gl.glVertex3d(-10 / size, 0, 3 / size);    //2
            Gl.glColor3f(0f, 0f, 0f);
            Gl.glVertex3d(-10 / size, 0, 0);
            Gl.glVertex3d(-10 / size, -5 / size, 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLE_STRIP);
            Gl.glVertex3d(-10 / size, -5 / size, 0);   //1
            Gl.glVertex3d(-10 / size, 0, 3 / size);    //2
            Gl.glVertex3d(-8 / size, -1 / size, 0);    //3
            Gl.glVertex3d(-7 / size, 0, 2 / size);    //4
            Gl.glVertex3d(0, -3 / size, 0);     //5
            Gl.glVertex3d(0, 0, 3 / size);     //6
            Gl.glVertex3d(3.5 / size, -2.2 / size, 0); //7
            Gl.glVertex3d(3.5 / size, 0, 3 / size);   //8
            Gl.glVertex3d(5 / size, 0, 0);     //9
            Gl.glEnd();
        }
        private void Draw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            // очищаем текущую матрицу 
            Gl.glLoadIdentity();
            if (Debugging)
            {
                // устанавливаем текущий цвет - красный 
                Gl.glColor3f(255, 0, 0);
                {
                    if (ShowLeader)
                    {
                        Fish f = Leader;
                        Gl.glPushMatrix();
                        double x = f.m_Pos.X;
                        double y = f.m_Pos.Y;
                        Gl.glTranslated(x, y, 0);
                        Gl.glRotated(-90 + f.m_Angle, 0, 0, 1);
                        Gl.glBegin(Gl.GL_LINE_LOOP);
                        Gl.glVertex2d(-0.5, -0.5);
                        Gl.glVertex2d(+0.5, -0.5);
                        Gl.glVertex2d(0, +1.5);
                        Gl.glEnd();
                        Gl.glPopMatrix();
                    }
                    if (ShowLeaderAxis)
                    {
                        Fish f = Leader;
                        double x = f.m_Pos.X;
                        double y = f.m_Pos.Y;
                        Gl.glPushMatrix();
                        Gl.glTranslated(x, y, 0);

                        Gl.glBegin(Gl.GL_LINES);
                        Gl.glVertex2d(-10.0, 0);
                        Gl.glVertex2d(10.0, 0);
                        Gl.glEnd();

                        Gl.glBegin(Gl.GL_LINES);
                        Gl.glVertex2d(0, -10.0);
                        Gl.glVertex2d(0, 10.0);
                        Gl.glEnd();

                        Gl.glBegin(Gl.GL_LINES);
                        Gl.glVertex2d(0, 0);
                        Gl.glVertex2d(-x + Target.X, -y + Target.Y);
                        Gl.glEnd();

                        Gl.glRotated(-90 + f.m_Angle, 0, 0, 1);
                        Gl.glBegin(Gl.GL_LINES);
                        Gl.glVertex2d(-10.0, 0);
                        Gl.glVertex2d(10.0, 0);
                        Gl.glEnd();

                        Gl.glBegin(Gl.GL_LINES);
                        Gl.glVertex2d(0, -10.0);
                        Gl.glVertex2d(0, 10.0);
                        Gl.glEnd();

                        Gl.glPopMatrix();
                    }
                }
                foreach (Fish f in Fishes)
                {
                    Gl.glPushMatrix();
                    double x = f.m_Pos.X;
                    double y = f.m_Pos.Y;
                    Gl.glTranslated(x, y, 0);
                    Gl.glRotated(-90 + f.m_Angle, 0, 0, 1);
                    Gl.glBegin(Gl.GL_LINE_LOOP);
                    Gl.glVertex2d(-0.5, -0.5);
                    Gl.glVertex2d(+0.5, -0.5);
                    Gl.glVertex2d(0, +1.5);
                    Gl.glEnd();
                    Gl.glPopMatrix();
                }
                if (ShowTarget && !Target.IsEmpty)
                {
                    double x = Target.X;
                    double y = Target.Y;
                    Gl.glBegin(Gl.GL_QUADS);
                    Gl.glVertex2d(x - 0.5, y - 0.5);
                    Gl.glVertex2d(x + 0.5, y - 0.5);
                    Gl.glVertex2d(x + 0.5, y + 0.5);
                    Gl.glVertex2d(x - 0.5, y + 0.5);
                    Gl.glEnd();
                }
            }
            else
            {
                if (ShowLeader)
                {
                    Fish f = Leader;
                    Gl.glPushMatrix();
                    double x = f.m_Pos.X;
                    double y = f.m_Pos.Y;
                    Gl.glTranslated(x, y, -50);
                    Gl.glRotated(f.m_Angle, 0, 0, 1);
                    DrawFish(0, f.m_color);
                    Gl.glPopMatrix();
                }
                foreach (Fish f in Fishes)
                {
                    Gl.glPushMatrix();
                    double x = f.m_Pos.X;
                    double y = f.m_Pos.Y;
                    Gl.glTranslated(x, y, -50);
                    Gl.glRotated(f.m_Angle, 0, 0, 1);
                    DrawFish(0, f.m_color);
                    Gl.glPopMatrix();
                }
                if (ShowTarget && !Target.IsEmpty)
                {
                    Gl.glPushMatrix();
                    double x = Target.X;
                    double y = Target.Y;
                    Gl.glTranslated(x, y, -50);
                    Gl.glBegin(Gl.GL_POLYGON);
                    Gl.glVertex3d(-0.5, -0.5, 0);
                    Gl.glVertex3d(0.5, -0.5, 0);
                    Gl.glVertex3d(0.5, 0.5, 0);
                    Gl.glVertex3d(0.5, 0.5, 0);
                    Gl.glEnd();
                    Gl.glPopMatrix();
                }
            }
            Gl.glFlush();
            AnT.Invalidate();
        }
        private void MoveFishes()
        {
            if (!Hang)
            {
                Leader.Move(Target);  //Leader moves at constant near maximum speed
                foreach (Fish f in Fishes)
                {
                    //f.Move(Leader.m_Pos.X + f.m_ID * 0.002F, Leader.m_Pos.Y + f.m_ID * 0.002F);
                    f.Move(Target.X + f.m_ID * 0.002F, Target.Y + f.m_ID * 0.002F);
                }
            }
            else
            {
                //Random rnd = new Random();
                //Leader.HangAround();
                //Leader.isHanging = true;
                foreach (Fish f in Fishes)
                {
                    //if (rnd.NextDouble() > 0.85)
                    //{
                    //    f.isHanging = true;
                    //}
                    f.HangAround();
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Draw();
            MoveFishes();
            //TickNo++;
            if (Distance(Leader.m_Pos, Target) < 1)
            {
                //Target = new PointF();
            }
            if (TickNo > 400)
            {
                Random rnd = new Random();
                //Target = new PointF(2 * ((float)rnd.NextDouble() - 0.5F) * (SizeX-5), 2 * ((float)rnd.NextDouble() - 0.5F) * (SizeY-5));
                TickNo = 0;
                foreach (Fish f in Fishes)
                {
                    f.isHanging = false;
                }
                Leader.m_HangTarget.X = 2 * ((float)rnd.NextDouble() - 0.5F) * (SizeX - 5);
                Leader.m_HangTarget.Y = 2 * ((float)rnd.NextDouble() - 0.5F) * (SizeY - 10);
            }
            if (ClearTarget)
            {
                if (TickNo == 125)
                {
                    Target = new PointF();
                }
            }
        }
        private float Distance(PointF p1, PointF p2)
        {
            float X = p1.X - p2.X;
            float Y = p1.Y - p2.Y;
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        private void AnT_MouseMove(object sender, MouseEventArgs e)
        {
            float XOff, YOff;
            XOff = (float)e.X / AnT.Width - 0.5f;
            YOff = -(float)e.Y / AnT.Height + 0.5f;
            Target = new PointF(2 * XOff * SizeX, 2 * YOff * SizeY);
        }

        private void AnT_MouseDown(object sender, MouseEventArgs e)
        {
            Hang = true;
            foreach (Fish f in Fishes)
            {
                f.isHanging = true;
            }
        }

        private void AnT_MouseUp(object sender, MouseEventArgs e)
        {
            Hang = false;
            foreach (Fish f in Fishes)
            {
                f.isHanging = false;
            }
        }
    }
    public class Fish
    {
        public Fish(float x, float y, float HangX, float HangY, double angle, int ID, Color col)
        {
            Random rand = new Random();
            m_Pos = new PointF(x, y);
            m_HangTarget = new PointF(HangX, HangY);
            m_Angle = angle;
            m_ID = ID;
            m_color = col;
        }
        public void Move(float x, float y)
        {
            Move(new PointF(x, y));
        }
        public void Move(PointF Target)
        {
            this.RotateTo(Target);
            double AngleToTarget;
            AngleToTarget = this.GetAngleTo(Target);
            ChangeSpeed(AngleToTarget);
            float y = (float)Math.Sin(m_Angle / 57);
            float x = (float)Math.Cos(m_Angle / 57);
            if (Math.Abs(m_Angle) < 90)
            {
                x = Math.Abs(x);
            }
            else
            {
                x = -Math.Abs(x);
            }
            if (m_Angle > 0)
            {
                y = Math.Abs(y);
            }
            else
            {
                y = -Math.Abs(y);
            }
            m_Pos.X += (float)m_Speed * x;
            m_Pos.Y += (float)m_Speed * y;
        }
        public void ChangeSpeed(double AngleToTarget)
        {
            //Changes speed as if fish had momentum
            double NeededSpeed = 0.125 + 0.115 * (180 - Math.Abs(AngleToTarget) * 1.5) / 180;
            double SpeedDiff;
            SpeedDiff = NeededSpeed - m_Speed;
            if (Math.Abs(SpeedDiff) > 0.01)
            {
                m_Speed += Math.Sign(SpeedDiff) * 0.001;
            }
            else
            {
                m_Speed += SpeedDiff;
            }
        }
        public void RotateTo(float x, float y)
        {
            RotateTo(new PointF(x, y));
        }
        public void RotateTo(PointF Target)
        {
            Random rnd = new Random();
            double step = 0.9 + rnd.NextDouble() * 0.6;                  // Rotate angle
            double absoluteAngleTo = Math.Atan2(Target.Y - m_Pos.Y, Target.X - m_Pos.X);
            double relativeAngleTo = absoluteAngleTo - m_Angle/57;

            while (relativeAngleTo > Math.PI)
            {
                relativeAngleTo -= 2.0D * Math.PI;
            }

            while (relativeAngleTo < -Math.PI)
            {
                relativeAngleTo += 2.0D * Math.PI;
            }
            relativeAngleTo *= 57;
            if (Math.Abs(relativeAngleTo) > MaxIgnoredAngle)
            {
                if (Math.Abs(relativeAngleTo + step) < Math.Abs(relativeAngleTo - step))
                {
                    m_Angle -= step;
                }
                else
                {
                    m_Angle += step;
                }
            }
            if (m_Angle < -180)
            {
                m_Angle = 360 + m_Angle;
            }
            else if (m_Angle > 180)
            {
                m_Angle = -360 + m_Angle;
            }
        }
        public double GetAngleTo(float x, float y)
        {
            return GetAngleTo(new PointF(x, y));
        }
        public double GetAngleTo(PointF Target)
        {
            double absoluteAngleTo = Math.Atan2(Target.Y - m_Pos.Y, Target.X - m_Pos.X);
            double relativeAngleTo = absoluteAngleTo - m_Angle / 57;

            while (relativeAngleTo > Math.PI)
            {
                relativeAngleTo -= 2.0D * Math.PI;
            }

            while (relativeAngleTo < -Math.PI)
            {
                relativeAngleTo += 2.0D * Math.PI;
            }
            return relativeAngleTo;
        }
        public void HangAround()
        {
            //if (isHanging)
            //{
                Move(m_HangTarget);
            //}
            //else
            //{
                //Move(0);
            //}
        }
        public PointF m_Pos;                    //Current position
        public PointF m_HangTarget;             //Point which will fish lead to when isn't following leader fish
        public double m_Angle { get; set; }     //Self angle where positive X axis is 0
        public double m_Speed { get; set; }
        public bool isHanging = false;
        public int m_ID;
        private const double MaxIgnoredAngle = 5.0D;        //Fish won't rotate if relative angle to target is less than this angle
        public Color m_color;
    }
}
