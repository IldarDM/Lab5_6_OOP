using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFigures
{
    public class Polygon : Figure
    {
        public Point[] points;
        public static int count = 0;
        public int r_x;
        public int r_y;
        public Polygon() { }
        public Polygon(Point[] points) 
        {
            bool flag = true;
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
                FiguresContainer.PolygonsList.Add(this);
                FiguresContainer.figureList.Add(this);
            }

        }
        public Polygon(Point[] points, int x, int y, int r_x, int r_y)
        {
            this.x = x;
            this.y = y;
            this.r_x = r_x;
            this.r_y = r_y;
            this.points = points;
            number = count;
            count++;
            FiguresContainer.PolygonsList.Add(this);
            FiguresContainer.figureList.Add(this);
        }
        public override void Draw()
        {
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawPolygon(pen, points);
            pictureBox.Image = bitmap;
            DrawText("Pol", number, x, y, r_x - x, r_y - y);
        }
        public override void MoveTo(int dx, int dy)
        {
            bool flag = true;
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].X + dx < pictureBox.Width && points[i].X + dx > 0 &&
                    points[i].Y + dy < pictureBox.Height && points[i].Y + dy > 0)
                {
                    points[i].X += dx; points[i].Y += dy;
                }
                else
                {
                    MessageBox.Show("Фигура должна полностью помещаться на холст");
                    flag = false;
                    return;
                }
            }
            if (flag)
            {
                this.x += dx; this.y += dy;
                this.r_x += dx; this.r_y += dy;
                DeleteF(this, false);
                Draw();
            }

        }
    }
}
