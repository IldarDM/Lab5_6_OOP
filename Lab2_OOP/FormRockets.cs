using MyFigures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_OOP
{
    public partial class FormRockets : Form
    {
        private Button but;
        public FormRockets(Button but)
        {
            InitializeComponent();
            this.but = but;
            for (int i = 0; i < FiguresContainer.RocketsList.Count; i++)
            {
                figure_box.Items.Add(FiguresContainer.RocketsList[i]);
                figure_box.Items[i] = $"Rocket{FiguresContainer.RocketsList[i].number}";
            }
            buttonDelete.Enabled = false;
            Button_New_Cords.Enabled = false;
            Button_New_Size.Enabled = false;
            Start_button.Enabled = false;
        }
        private void FormRockets_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
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
                        Rocket Rock = new Rocket(x, y, width_f, height_f);
                        Rock.Draw();
                        figure_box.Items.Add(Rock);
                        figure_box.Items[figure_box.FindStringExact(Rock.ToString())] = $"Rock{Rock.number}";
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
            if (FiguresContainer.RocketsList[figure_box.SelectedIndex] != null)
            {
                Rocket Rocket = FiguresContainer.RocketsList[figure_box.SelectedIndex];
                if (int.TryParse(new_X.Text, out int number) && int.TryParse(new_Y.Text, out number))
                {
                    Rocket.MoveTo(int.Parse(new_X.Text), int.Parse(new_Y.Text));
                }
                else
                {
                    MessageBox.Show("Некорректный формат ввода");
                }
            }
            else
            {
                MessageBox.Show("Выберите существующую фигуру");
            }
        }

        private void figure_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDelete.Enabled = true;
            Button_New_Cords.Enabled = true;
            Button_New_Size.Enabled = true;
            Start_button.Enabled = true;
        }

        private void Button_New_Size_Click(object sender, EventArgs e)
        {
            if (int.TryParse(new_height.Text, out int check) && int.TryParse(new_width.Text, out check))
            {
                int height = int.Parse(new_height.Text);
                int width = int.Parse(new_width.Text);
                if (height > 0 && width > 0)
                {
                    Rocket Rocket = FiguresContainer.RocketsList[figure_box.SelectedIndex];
                    Rocket.ResizeRocket(width, height);
                }
                else 
                {
                    MessageBox.Show("Габариты ракеты - положительные числа");
                }
            }
            else
            {
                MessageBox.Show("Ошибка ввода, вводите целые числа");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Rocket Rocket = FiguresContainer.RocketsList[figure_box.SelectedIndex];
            FiguresContainer.RocketsList.Remove(Rocket);
            Rocket.DeleteF(Rocket, true);
            figure_box.Items.Clear();
            figure_box.SelectedIndex = -1;
            for (int i = 0; i < FiguresContainer.RocketsList.Count; i++)
            {
                figure_box.Items.Add(FiguresContainer.RocketsList[i]);
                figure_box.Items[i] = $"Rocket{FiguresContainer.RocketsList[i].number}";
            }
            buttonDelete.Enabled = false;
            Button_New_Cords.Enabled = false;
            Button_New_Size.Enabled = false;
            Start_button.Enabled = false;
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            Rocket Rocket = FiguresContainer.RocketsList[figure_box.SelectedIndex];
            Thread fly = new Thread(Rocket.Start_Rocket);
            fly.Start();
            if (Rocket.y == 0)
            {
                fly.Interrupt();
            }
        }
    }
}
