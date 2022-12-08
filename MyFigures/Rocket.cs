using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFigures
{
    public class Rocket : Figure
    {
        public Triangle Body { get; set; }
        public Triangle Window { get; set; }
        public Triangle Left_wing { get; set; }
        public Triangle Right_wing { get; set; }
        public Triangle Left_engine { get; set; }
        public Triangle Middle_engine { get; set; }
        public Triangle Right_engine { get; set; }
        public List<Triangle> Rocket_parts { get; set; } = new List<Triangle>();
        public int left_x, high_y, right_x, low_y;
        static public int count = 0;

        public Rocket() { }
        public Rocket(int x, int y, int width, int height)
        {
            if (x - width / 10 < 0 || y < 0 || x + width / 10 > pictureBox.Width || y + height * 11 / 10 > pictureBox.Height)
            {
                MessageBox.Show("Фигура должна полностью помещаться на холст!");
            }
            else
            {
                this.x = x; this.y = y; this.width = width; this.height = height;
                left_x = x - width / 10;
                high_y = y;
                right_x = x + width / 10;
                low_y = y + height * 11 / 10;
                Create_Body();
                Create_Window();
                Create_Left_wing();
                Create_Right_wing();
                Create_Left_engine();
                Create_Middle_engine();
                Create_Right_engine();
                FiguresContainer.RocketsList.Add(this);
                FiguresContainer.figureList.Add(this);
                number = count;
                count++;
            }
        }
        private void Create_Body()
        {
            Point[] points = new Point[3];
            points[0] = new Point(x, y + height);
            points[1] = new Point(x + width, y + height);
            points[2] = new Point(x + width / 2, y);
            Body = new Triangle(points, false);
            Rocket_parts.Add(Body);
        }

        private void Create_Window()
        {
            Point[] points = new Point[3];
            points[0] = new Point(x + width * 2 / 5, y + height * 3 / 5);
            points[1] = new Point(x + width * 3 / 5, y + height * 3 / 5);
            points[2] = new Point(x + width / 2, y + height * 2 / 5);
            Window = new Triangle(points, false);
            Rocket_parts.Add(Window);
        }
        private void Create_Left_wing()
        {
                Point[] points = new Point[3];
                points[0] = new Point(x + width / 5, y + height * 3 / 5);
                points[1] = new Point(x + width * 3 / 10, y + height * 2 / 5);
                points[2] = new Point(x - width / 10, y + height * 3 / 5);
                Left_wing = new Triangle(points, false);
                Rocket_parts.Add(Left_wing);
        }

        private void Create_Right_wing()
        {

                Point[] points = new Point[3];
                points[0] = new Point(x + width * 4 / 5, y + height * 3 / 5);
                points[1] = new Point(x + width * 7 / 10, y + height * 2 / 5);
                points[2] = new Point(x + width * 11 / 10, y + height * 3 / 5);
                Right_wing = new Triangle(points, false);
                Rocket_parts.Add(Right_wing);
           
        }
        private void Create_Left_engine()
        {

                Point[] points = new Point[3];
                points[0] = new Point(x + width * 1 / 5, y + height * 9 / 10);
                points[1] = new Point(x + width * 1 / 10, y + height * 11 / 10);
                points[2] = new Point(x + width * 3 / 10, y + height * 11 / 10);
                Left_engine = new Triangle(points, false);
                Rocket_parts.Add(Left_engine);

        }
        private void Create_Middle_engine()
        {

                Point[] points = new Point[3];
                points[0] = new Point(x + width * 1 / 2, y + height * 9 / 10);
                points[1] = new Point(x + width * 2 / 5, y + height * 11 / 10);
                points[2] = new Point(x + width * 3 / 5, y + height * 11 / 10);
                Middle_engine = new Triangle(points, false);
                Rocket_parts.Add(Middle_engine);

        }
        private void Create_Right_engine()
        {
                Point[] points = new Point[3];
                points[0] = new Point(x + width * 4 / 5, y + height * 9 / 10);
                points[1] = new Point(x + width * 7 / 10, y + height * 11 / 10);
                points[2] = new Point(x + width * 9 / 10, y + height * 11 / 10);
                Right_engine = new Triangle(points, false);
                Rocket_parts.Add(Right_engine);
        }
        public override void Draw()
        {
            foreach (Triangle part in Rocket_parts)
            {
                part.Draw(false);
            }
            DrawText("Rocket ", number, x, y + height * 3 / 5, width, height * 2 / 5);
        }
        public override void MoveTo(int dx, int dy)
        {
            if (x + dx - width / 10 < 0 || y + dy < 0 || x + dx + width / 10 > pictureBox.Width || y + dy + height * 11 / 10 > pictureBox.Height)
            {
                MessageBox.Show("Фигура должна полностью помещаться на холст!");
            }
            else
            {
                x += dx; y += dy;
                left_x = x - width / 10;
                high_y = y;
                right_x = x + width / 10;
                low_y = y + height * 11 / 10;

                foreach (Triangle part in Rocket_parts)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        part.points[i].X += dx;
                        part.points[i].Y += dy;
                    }
                }
                DeleteF(this, false);
                Draw();
            }
        }

        public void ResizeRocket(int width, int height)
        {
            if (x - width / 10 < 0 || y < 0 || x + width / 10 > pictureBox.Width || y + height * 11 / 10 > pictureBox.Height)
            {
                MessageBox.Show("Фигура должна полностью помещаться на холст!");
            }
            else
            {
                this.width = width; this.height = height;
                left_x = x - width / 10;
                high_y = y;
                right_x = x + width / 10;
                low_y = y + height * 11 / 10;
                Rocket_parts.Clear();
                Create_Body();
                Create_Window();
                Create_Left_wing();
                Create_Right_wing();
                Create_Left_engine();
                Create_Middle_engine();
                Create_Right_engine();
                DeleteF(this, false);
                Draw();
            }

        }

        public void Start_Rocket()
        {
            while (high_y > 5)
            {
                MoveTo(0, -5);
                Task.Delay(50).GetAwaiter().GetResult();
            }
            MoveTo(0, high_y);
        }
    }
}
