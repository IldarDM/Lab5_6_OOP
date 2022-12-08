using MyFigures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_OOP
{
    public partial class FormPolygons : Form
    {
        private Button but;
        PictureBox pictureBox;
        private Point[] points;
        private int number = 0;
        public FormPolygons(Button but, PictureBox pictureBox)
        {
            InitializeComponent();
            this.but = but;
            this.pictureBox = pictureBox;
            for (int i = 0; i < FiguresContainer.PolygonsList.Count; i++)
            {
                figure_box.Items.Add(FiguresContainer.PolygonsList[i]);
                figure_box.Items[i] = $"Pol{FiguresContainer.PolygonsList[i].number}";
            }
            CancelPoints.Enabled = false;
            AddPoint.Enabled = false;
            Draw.Enabled = false;
            Delete.Enabled = false;
            ChangeCords.Enabled = false;
        }
        private void FormPolygons_MouseDown(object sender, MouseEventArgs e)
        {
            Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            Close();
            but.Enabled = true;
        }

        private void ConfirmNum_Click(object sender, EventArgs e)
        {
            if (int.TryParse(PointsNum.Text, out int check))
            {
                int pointsNum = int.Parse(PointsNum.Text);
                if (pointsNum > 2)
                {
                    points = new Point[pointsNum];
                    PointsNum.Enabled = false;
                    CancelPoints.Enabled = true;
                    AddPoint.Enabled = true;
                    ConfirmNum.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Количество точек должно быть три и больше");
                }
            }
            else
            {
                MessageBox.Show("Количество точек - целое положительное число");
            }
        }

        private void AddPoint_Click(object sender, EventArgs e)
        {
            if (int.TryParse(X_cord.Text, out int check) && int.TryParse(Y_cord.Text, out check))
            {
                int x = int.Parse(X_cord.Text);
                int y = int.Parse(Y_cord.Text);
                if (!(x < 0 || y < 0 || x > pictureBox.Width || y > pictureBox.Height))
                {
                    points[number] = new Point(x, y);
                    number++;
                    if (number == points.Length)
                    {
                        AddPoint.Enabled = false;
                        Draw.Enabled = true;
                        X_cord.Enabled = false;
                        Y_cord.Enabled = false;
                    }
                    else
                    {
                        Points_Label.Text = $"Координаты {number + 1}-ой точки:";
                    }
                }
                else 
                {
                    MessageBox.Show("Точка должна быть в пределах холста");
                }
            }
            else
            {
                MessageBox.Show("Некорректный формат ввода");
            }
        }

        private void Draw_Click(object sender, EventArgs e)
        {
            int x, y, r_x, r_y;
            x = points[0].X; r_x = points[0].X;
            y = points[0].Y; r_y = points[0].Y;
            for (int i = 0; i < points.Length; i++)
            {
                if (x > points[i].X) { x = points[i].X; }
                if (x < 0) { MessageBox.Show("Фигура должна полностью помещаться на холст"); return; }
                if (y > points[i].Y) { y = points[i].Y; }
                if (y < 0) { MessageBox.Show("Фигура должна полностью помещаться на холст"); return; }
                if (r_x < points[i].X) { r_x = points[i].X; }
                if (r_x > pictureBox.Width) { MessageBox.Show("Фигура должна полностью помещаться на холст"); return; }
                if (r_y < points[i].Y) { r_y = points[i].Y; }
                if (r_y > pictureBox.Height) { MessageBox.Show("Фигура должна полностью помещаться на холст"); return; }
            }
            Polygon pol = new Polygon(points, x, y, r_x, r_y);
            pol.Draw();
            figure_box.Items.Add(pol);
            figure_box.Items[figure_box.FindStringExact(pol.ToString())] = $"Pol{pol.number}";
            CancelPoints_Click(sender, e);
        }

        private void figure_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            Delete.Enabled = true;
            ChangeCords.Enabled = true;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (FiguresContainer.PolygonsList[figure_box.SelectedIndex] != null)
            {
                Polygon Pol = FiguresContainer.PolygonsList[figure_box.SelectedIndex];
                FiguresContainer.PolygonsList.Remove(Pol);
                Pol.DeleteF(Pol, true);
                figure_box.Items.Clear();
                figure_box.SelectedIndex = -1;
                for (int i = 0; i < FiguresContainer.PolygonsList.Count; i++)
                {
                    figure_box.Items.Add(FiguresContainer.PolygonsList[i]);
                    figure_box.Items[i] = $"Rect{FiguresContainer.PolygonsList[i].number}";
                }
                Delete.Enabled = false;
                ChangeCords.Enabled = false;
            }
            else
            {
                MessageBox.Show("Выберите существующую фигуру");
            }
        }

        private void ChangeCords_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dX.Text, out int check) && int.TryParse(dY.Text, out check))
            {
                Polygon pol = FiguresContainer.PolygonsList[figure_box.SelectedIndex];
                pol.MoveTo(int.Parse(dX.Text), int.Parse(dY.Text));
            }
            else
            {
                MessageBox.Show("Неверный формат ввода");
            }
        }

        private void CancelPoints_Click(object sender, EventArgs e)
        {
            points = null; number = 0;
            PointsNum.Enabled = true; PointsNum.Text = "";
            CancelPoints.Enabled = false;
            AddPoint.Enabled = false;
            Draw.Enabled = false;
            ConfirmNum.Enabled = true;
            X_cord.Text = ""; Y_cord.Text = ""; X_cord.Enabled = true; Y_cord.Enabled = true;
            Points_Label.Text = "Координаты 1-ой точки:";
        }
    }
}

