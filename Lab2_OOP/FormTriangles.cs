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
    public partial class FormTriangles : Form
    {
        private Button but;
        PictureBox pictureBox;
        private Point[] points = new Point[3];
        private int number = 0;
        public FormTriangles(Button but, PictureBox pictureBox)
        {
            InitializeComponent();
            this.but = but;
            this.pictureBox = pictureBox;
            for (int i = 0; i < FiguresContainer.TrianglesList.Count; i++)
            {
                figure_box.Items.Add(FiguresContainer.TrianglesList[i]);
                figure_box.Items[i] = $"Tri{FiguresContainer.TrianglesList[i].number}";
            }
            AddPoint.Enabled = true;
            Draw.Enabled = false;
            Delete.Enabled = false;
            ChangeCords.Enabled = false;
        }
        private void FormTriangles_MouseDown(object sender, MouseEventArgs e)
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
            Triangle Tri = new Triangle(points);
            Tri.Draw();
            figure_box.Items.Add(Tri);
            figure_box.Items[figure_box.FindStringExact(Tri.ToString())] = $"Tri{Tri.number}";
            CancelPoints_Click(sender, e);
        }

        private void figure_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            Delete.Enabled = true;
            ChangeCords.Enabled = true;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (FiguresContainer.TrianglesList[figure_box.SelectedIndex] != null)
            {
                Triangle Tri = FiguresContainer.TrianglesList[figure_box.SelectedIndex];
                FiguresContainer.TrianglesList.Remove(Tri);
                Tri.DeleteF(Tri, true);
                figure_box.Items.Clear();
                figure_box.SelectedIndex = -1;
                for (int i = 0; i < FiguresContainer.TrianglesList.Count; i++)
                {
                    figure_box.Items.Add(FiguresContainer.TrianglesList[i]);
                    figure_box.Items[i] = $"Rect{FiguresContainer.TrianglesList[i].number}";
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
                Triangle Tri = FiguresContainer.TrianglesList[figure_box.SelectedIndex];
                Tri.MoveTo(int.Parse(dX.Text), int.Parse(dY.Text));
            }
            else
            {
                MessageBox.Show("Неверный формат ввода");
            }
        }

        private void CancelPoints_Click(object sender, EventArgs e)
        {
            points = null; number = 0;
            CancelPoints.Enabled = false;
            AddPoint.Enabled = false;
            Draw.Enabled = false;
            X_cord.Text = ""; Y_cord.Text = ""; X_cord.Enabled = true; Y_cord.Enabled = true;
            Points_Label.Text = "Координаты 1-ой точки:";
        }
    }
}
