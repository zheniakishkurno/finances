using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace WindowsFormsApp2
{
    public partial class Form14 : Form
    {
        private string Username;
        public int BackGround = 0;
        public int Plauplay = 0;
        private static WindowsMediaPlayer player = new WindowsMediaPlayer();
        private static string[] songs = { "song1.mp3" };
        private static int currentSongIndex = 0;

        private const int MinFormWidth = 818;
        private const int MinFormHeight = 497;
        public Form14(int background, int plauplay, string username)
        {
            InitializeComponent();
            Plauplay = plauplay;
            BackGround = background;
            if (BackGround == 1)
            {
                // Меняем цвет формы на черный
                this.BackColor = Color.Black;
                BackGround = 1;
                // Меняем цвет обводки кнопки на белый и текст внутри кнопки на белый для guna2Button2
                guna2Button1.BorderColor = Color.White;
                guna2Button1.ForeColor = Color.White;

                // Меняем цвет текста на белый для всех остальных элементов управления на форме
                foreach (Control control in Controls)
                {
                    if (control is Label || control is Guna.UI2.WinForms.Guna2Button)
                    {
                        // Если элемент управления - это Label или Guna2Button, меняем его цвет текста на белый
                        control.ForeColor = Color.White;

                        // Если элемент управления - это Guna2Button, также меняем цвет обводки на белый
                        if (control is Guna.UI2.WinForms.Guna2Button)
                        {
                            ((Guna.UI2.WinForms.Guna2Button)control).BorderColor = Color.White;
                        }
                    }
                    // Добавьте другие типы элементов управления, которые нужно изменить на белый
                    //if (plauplay == 1)
                    //{
                    //    player = new WMPLib.WindowsMediaPlayer();
                    //    player.URL = songs[currentSongIndex];
                    //    plauplay = 1;
                    //    player.controls.play();
                    //}
                    //else
                    //{
                    //    player = new WMPLib.WindowsMediaPlayer();
                    //    player.URL = songs[currentSongIndex];
                    //    plauplay = 0;
                    //    player.controls.pause();
                    //}

                }
            }
            else
            {
                this.BackColor = Color.White;
                BackGround = 0;
                // Меняем цвет обводки кнопки на белый и текст внутри кнопки на белый для guna2Button2
                guna2Button1.BorderColor = Color.Black;
                guna2Button1.ForeColor = Color.Black;

                // Меняем цвет текста на белый для всех остальных элементов управления на форме
                foreach (Control control in Controls)
                {
                    if (control is Label || control is Guna.UI2.WinForms.Guna2Button)
                    {
                        // Если элемент управления - это Label или Guna2Button, меняем его цвет текста на белый
                        control.ForeColor = Color.Black;

                        // Если элемент управления - это Guna2Button, также меняем цвет обводки на белый
                        if (control is Guna.UI2.WinForms.Guna2Button)
                        {
                            ((Guna.UI2.WinForms.Guna2Button)control).BorderColor = Color.Black;
                        }
                    }
                    // Добавьте другие типы элементов управления, которые нужно изменить на белый
                    //if (plauplay == 1)
                    //{
                    //    player = new WMPLib.WindowsMediaPlayer();
                    //    player.URL = songs[currentSongIndex];
                    //    plauplay = 1;
                    //    player.controls.play();
                    //}
                    //else
                    //{
                    //    player = new WMPLib.WindowsMediaPlayer();
                    //    player.URL = songs[currentSongIndex];
                    //    plauplay = 0;
                    //    player.controls.pause();
                    //}

                }
            }
            this.Username = username;
            label1.Text = "Welcome, " + username + "!";
            this.Resize += Form14_Resize;

        }
        private void Form14_Resize(object sender, EventArgs e)
        {
            // Проверяем, не меньше ли размер формы, чем минимальные значения
            if (this.Width < MinFormWidth)
            {
                this.Width = MinFormWidth; // Если меньше, устанавливаем минимальную ширину
            }
            if (this.Height < MinFormHeight)
            {
                this.Height = MinFormHeight; // Если меньше, устанавливаем минимальную высоту
            }
            int margin = 40; // Отступ от краев формы

            // Вычисляем новую ширину ComboBox
            int comboBoxWidth = this.Width - 12 * margin; // Ширина ComboBox, равная ширине формы за вычетом отступов с обеих сторон
            comboBox1.Width = comboBoxWidth;

            // Вычисляем новое положение ComboBox по центру формы
            int comboBoxX = (this.Width - comboBoxWidth) / 2;
            int comboBoxY = comboBox1.Location.Y; // Оставляем Y координату без изменений
            comboBox1.Location = new Point(comboBoxX, comboBoxY);

            // Установка размеров кнопки guna2Button1 пропорционально изменению размеров формы
            guna2Button1.Width = (int)(this.Width * 0.2); // Например, кнопка будет занимать 20% ширины формы
            guna2Button1.Height = (int)(this.Height * 0.1); // Например, кнопка будет занимать 10% высоты формы

            guna2Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Button1.Location = new Point(10, this.ClientSize.Height - guna2Button1.Height - 10);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Проверяем выбранный элемент
            if (comboBox1.SelectedIndex == 0)
            {
                // Открываем 11 форму
                Form15 form15 = new Form15(BackGround, Plauplay, Username);
                form15.Size = this.Size;
                // Показываем Form2 и скрываем текущую форму (Form1)
                form15.Show();
                this.Hide();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                // Открываем 11 форму
                Form20 form20 = new Form20(BackGround, Plauplay, Username);
                form20.Size = this.Size;
                // Показываем Form2 и скрываем текущую форму (Form1)
                form20.Show();
                this.Hide();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                // Открываем 11 форму
                Form17 form17 = new Form17(BackGround, Plauplay, Username);
                form17.Size = this.Size;
                // Показываем Form2 и скрываем текущую форму (Form1)
                form17.Show();
                this.Hide();
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                // Открываем 11 форму
                Form18 form18 = new Form18(BackGround, Plauplay, Username);
                form18.Size = this.Size;
                // Показываем Form2 и скрываем текущую форму (Form1)
                form18.Show();
                this.Hide();
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                // Открываем 11 форму
                Form19 form19 = new Form19(BackGround, Plauplay, Username);
                form19.Size = this.Size;
                // Показываем Form2 и скрываем текущую форму (Form1)
                form19.Show();
                this.Hide();
            }
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            // Добавляем записи в комбобокс
            comboBox1.Items.Add("таблица расчет суммы ");
            comboBox1.Items.Add("таблица расчет средней суммы ");
            comboBox1.Items.Add("таблица расчет сумму за определеный срок  ");
            comboBox1.Items.Add("таблица расчет средней сумму за определеный срок");
            comboBox1.Items.Add("таблица расчет среднее за один день  ");
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7(BackGround, Plauplay, Username);
            form7.Size = this.Size;
            // Показываем Form2 и скрываем текущую форму (Form1)
            form7.Show();
            this.Hide();
        }
    }
}
