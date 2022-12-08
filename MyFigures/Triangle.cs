using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFigures
{
    public class Triangle : Polygon
    {
        public static new int count = 0;
        public Triangle() { }
        public Triangle(Point[] points)
        {
            bool flag = true;
            if (points.Length != 3)
            {
                MessageBox.Show("Треугольник должен задаваться 3 точками");
            }
            else
            {
                for (int i = 0; i < points.Length; i++)
                {
                    if (x > points[i].X) { x = points[i].X; }
                    if (x < 0) { MessageBox.Show("Фигура должна полностью помещаться на холст"); flag = false; }
                    if (y > points[i].Y) { y = points[i].Y; }
                    if (y < 0) { MessageBox.Show("Фигура должна полностью помещаться на холст"); flag = false; }
                    if (r_x < points[i].X) { r_x = points[i].X; }
                    if (r_x > pictureBox.Width) { MessageBox.Show("Фигура должна полностью помещаться на холст"); flag = false; }
                    if (r_y < points[i].Y) { r_y = points[i].Y; }
                    if (r_y > pictureBox.Height) { MessageBox.Show("Фигура должна полностью помещаться на холст"); flag = false; }
                }
                if (flag)
                {
                    this.points = points;
                    number = count;
                    count++;
                    FiguresContainer.TrianglesList.Add(this);
                    FiguresContainer.figureList.Add(this);
                }
            }

        }
        public Triangle(Point[] points, bool flag)
        {
            if (points.Length != 3)
            {
                MessageBox.Show("Треугольник должен задаваться 3 точками");
            }
            this.points = points;
        }
        public override void Draw()
        {
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawPolygon(pen, points);
            pictureBox.Image = bitmap;
            DrawText("Tri", number, x, y, r_x - x, r_y - y);
        }
        public void Draw(bool f)
        {
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawPolygon(pen, points);
            pictureBox.Image = bitmap;
        }
    }
}

