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
    public partial class Form9 : Form
    {
        public int BackGround = 0;
        public int Plauplay = 0;
        private string Username;
        private static WindowsMediaPlayer player = new WindowsMediaPlayer();
        private static string[] songs = { "song1.mp3" };
        private static int currentSongIndex = 0;

        private const int MinFormWidth = 818;
        private const int MinFormHeight = 497;
        public static void Play()
        {
            if (player != null)
            {
                player.URL = songs[currentSongIndex];
                player.controls.play();
            }
        }

        public static void Pause()
        {
            if (player != null)
            {
                player.controls.pause();
            }
        }
        public Form9(int background, int plauplay, string username)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100); // Установка координат X и Y для местоположения формы

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
            // Добавляем обработчик события загрузки формы
            this.Load += Form9_Load;
            this.Resize += Form9_Resize;
        
    }
        private void Form9_Resize(object sender, EventArgs e)
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



        private void Form9_Load(object sender, EventArgs e)
        {
            // Добавляем записи в комбобокс
            comboBox1.Items.Add("таблица пользователей ");
            comboBox1.Items.Add("таблица админа ");
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(BackGround, Plauplay, Username);
            form6.Size = this.Size;
            // Показываем Form2 и скрываем текущую форму (Form1)
            form6.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Проверяем выбранный элемент
            if (comboBox1.SelectedIndex == 0)
            {
                // Открываем 11 форму
                Form11 form11 = new Form11(BackGround, Plauplay, Username);
                form11.Size = this.Size;
                // Показываем Form2 и скрываем текущую форму (Form1)
                form11.Show();
                this.Hide();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                // Открываем 12 форму
                Form12 form12 = new Form12(BackGround, Plauplay, Username);
                form12.Size = this.Size;
                // Показываем Form2 и скрываем текущую форму (Form1)
                form12.Show();
                this.Hide();
            }
        }

        private void Form9_Load_1(object sender, EventArgs e)
        {
            // Устанавливаем положение формы
            this.Location = new Point(100, 100); // Пример: x=100, y=100
        }
    }
}
