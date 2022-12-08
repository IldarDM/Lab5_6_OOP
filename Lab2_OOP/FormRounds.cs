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
    public partial class FormRounds : Form
    {
        private Button but;
        public FormRounds(Button but)
        {
            InitializeComponent();
            this.but = but;
            for (int i = 0; i < FiguresContainer.RoundsList.Count; i++)
            {
                figure_box.Items.Add(FiguresContainer.RoundsList[i]);
                figure_box.Items[i] = $"Ro{FiguresContainer.RoundsList[i].number}";
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (FiguresContainer.RoundsList[figure_box.SelectedIndex] != null)
            {
                Round Ro = FiguresContainer.RoundsList[figure_box.SelectedIndex];
                FiguresContainer.RoundsList.Remove(Ro);
                Ro.DeleteF(Ro, true);
                figure_box.Items.Clear();
                figure_box.SelectedIndex = -1;
                for (int i = 0; i < FiguresContainer.RoundsList.Count; i++)
                {
                    figure_box.Items.Add(FiguresContainer.RoundsList[i]);
                    figure_box.Items[i] = $"Ro{FiguresContainer.RoundsList[i].number}";
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

        private void Button_New_Cords_Click(object sender, EventArgs e)
        {
            if (int.TryParse(new_X.Text, out int number) && int.TryParse(new_Y.Text, out number))
            {
                if (FiguresContainer.RoundsList[figure_box.SelectedIndex] != null)
                {
                    Round Ro = FiguresContainer.RoundsList[figure_box.SelectedIndex];
                    Ro.MoveTo(int.Parse(new_X.Text), int.Parse(new_Y.Text));
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
            if (int.TryParse(new_radius.Text, out int number))
            {
                int radius = int.Parse(new_radius.Text);
                if (radius > 0)
                {
                    if (FiguresContainer.RoundsList[figure_box.SelectedIndex] != null)
                    {
                        Round Ro = FiguresContainer.RoundsList[figure_box.SelectedIndex];
                        Ro.ResizeRound(radius);
                    }
                    else
                    {
                        MessageBox.Show("Выберите существующую фигуру");
                    }
                }
                else
                {
                    MessageBox.Show("У фигуры должен быть положительный радиус");
                }
            }
            else
            {
                MessageBox.Show("Ошибка ввода");
            }
        }
        private void FormRounds_MouseDown(object sender, MouseEventArgs e)
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

        private void Button_Draw_Click(object sender, EventArgs e)
        {
            if (int.TryParse(setX.Text, out int number) && int.TryParse(setY.Text, out number) && int.TryParse(radius.Text, out number))
            {
                int x = int.Parse(setX.Text);
                int y = int.Parse(setY.Text);
                int radius_f = int.Parse(radius.Text);
                if (!(radius_f < 0))
                {
                    if (!(x < 0 || y < 0 || x + radius_f > Figure.pictureBox.Width || y + radius_f > Figure.pictureBox.Height))
                    {
                        Round Ro = new Round(x, y, radius_f);
                        Ro.Draw();
                        figure_box.Items.Add(Ro);
                        figure_box.Items[figure_box.FindStringExact(Ro.ToString())] = $"Ro{Ro.number}";
                    }
                    else
                    {
                        MessageBox.Show("Фигура должна полностью помещаться на холст");
                    }
                }
                else
                {
                    MessageBox.Show("У фигуры должна быть положительный радиус");
                }
            }
            else
            {
                MessageBox.Show("Некорректный формат ввода");
            }
        }
    }
}

