using MyFigures;
using System;
using System.Windows.Forms;

namespace Lab5_OOP
{
    public partial class FormEllipses : Form
    {
        private Button but;
        public FormEllipses(Button but)
        {
            InitializeComponent();
            this.but = but;
            for (int i = 0; i < FiguresContainer.EllipsesList.Count; i++)
            {
                figure_box.Items.Add(FiguresContainer.EllipsesList[i]);
                figure_box.Items[i] = $"Ell{FiguresContainer.EllipsesList[i].number}";
            }
            buttonDelete.Enabled = false;
            Button_New_Cords.Enabled = false;
            Button_New_Size.Enabled = false;
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            Close();
            but.Enabled = true;
        }

        private void Button_Draw_Click(object sender, EventArgs e)
        {
            if (int.TryParse(setX.Text, out int number) && int.TryParse(setY.Text, out number) &&
                int.TryParse(width.Text, out number) && int.TryParse(height.Text, out number))
            {
                int x = int.Parse(setX.Text);
                int y = int.Parse(setY.Text);
                int width_f = int.Parse(width.Text);
                int height_f = int.Parse(height.Text);
                if (!(width_f < 0 || height_f < 0))
                {
                    if (!(x < 0 || y < 0 || x + width_f > Figure.pictureBox.Width || y + height_f > Figure.pictureBox.Height))
                    {
                        Ellipse Ell = new Ellipse(x, y, width_f, height_f);
                        Ell.Draw();
                        figure_box.Items.Add(Ell);
                        figure_box.Items[figure_box.FindStringExact(Ell.ToString())] = $"Ell{Ell.number}";
                    }
                    else
                    {
                        MessageBox.Show("Фигура должна полностью помещаться на холст");
                    }
                }
                else
                {
                    MessageBox.Show("У фигуры должна быть положительная длина и высота");
                }
            }
            else
            {
                MessageBox.Show("Некорректный формат ввода");
            }
        }

        private void Button_New_Cords_Click(object sender, EventArgs e)
        {
            if (int.TryParse(new_X.Text, out int number) && int.TryParse(new_Y.Text, out number))
            {
                if (FiguresContainer.EllipsesList[figure_box.SelectedIndex] != null)
                {
                    Ellipse Ell = FiguresContainer.EllipsesList[figure_box.SelectedIndex];
                    Ell.MoveTo(int.Parse(new_X.Text), int.Parse(new_Y.Text));
                }
                else
                {
                    MessageBox.Show("Выберите существующую фигуру");
                }
            }
            else
            {
                MessageBox.Show("Ошибка ввода");
            }
        }

        private void Button_New_Size_Click(object sender, EventArgs e)
        {
            if (int.TryParse(new_width.Text, out int number) && int.TryParse(new_height.Text, out number))
            {
                int width = int.Parse(new_width.Text);
                int height = int.Parse(new_height.Text);
                if (width > 0 && height > 0)
                {
                    if (FiguresContainer.EllipsesList[figure_box.SelectedIndex] != null)
                    {
                        Ellipse Ell = FiguresContainer.EllipsesList[figure_box.SelectedIndex];
                        Ell.ResizeEll(width, height);
                    }
                    else 
                    {
                        MessageBox.Show("Выберите существующую фигуру");
                    }
                }
                else
                {
                    MessageBox.Show("У фигуры должна быть положительная длина и высота");
                }
            }
            else
            {
                MessageBox.Show("Ошибка ввода");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (FiguresContainer.EllipsesList[figure_box.SelectedIndex] != null)
            {
                Ellipse Ell = FiguresContainer.EllipsesList[figure_box.SelectedIndex];
                FiguresContainer.EllipsesList.Remove(Ell);
                Ell.DeleteF(Ell, true);
                figure_box.Items.Clear();
                figure_box.SelectedIndex = -1;
                for (int i = 0; i < FiguresContainer.EllipsesList.Count; i++)
                {
                    figure_box.Items.Add(FiguresContainer.EllipsesList[i]);
                    figure_box.Items[i] = $"Ell{FiguresContainer.EllipsesList[i].number}";
                }
                buttonDelete.Enabled = false;
                Button_New_Cords.Enabled = false;
                Button_New_Size.Enabled = false;
            }
            else 
            {
                MessageBox.Show("Выберите существующую фигуру");
            }
        }

        private void FormEllipses_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void figure_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDelete.Enabled = true;
            Button_New_Cords.Enabled = true;
            Button_New_Size.Enabled = true;
        }
    }
}
