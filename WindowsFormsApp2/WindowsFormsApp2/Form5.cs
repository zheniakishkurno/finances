using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Guna.UI2.WinForms;
using WMPLib;


namespace WindowsFormsApp2
{ 
    public partial class Form5 : Form
    {
        private string Username;
        public int BackGround = 0;
        public int Plauplay = 0;
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
        public Form5(int background, int plauplay, string username)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100); // Установка координат X и Y для местоположения формы

            InitializeComponent();
            // Связываем textBox1 с ToolTip и устанавливаем описание "Введите имя"
            toolTip1.SetToolTip(guna2TextBox1, "Введите время когда было произведена трата");

            // Связываем textBox2 с ToolTip и устанавливаем описание "Введите пароль"
            toolTip2.SetToolTip(guna2TextBox2, "Введите сумму траты");
            // Связываем textBox2 с ToolTip и устанавливаем описание "Введите пароль"
            toolTip3.SetToolTip(guna2TextBox3, "Введите на что потратили деньги");

            Plauplay = plauplay;
            BackGround = background;
            if (BackGround == 1)
            {
                // Меняем цвет формы на черный
                this.BackColor = Color.Black;
                BackGround = 1;
                // Меняем цвет обводки кнопки на белый и текст внутри кнопки на белый для guna2Button2
                guna2Button2.BorderColor = Color.White;
                guna2Button2.ForeColor = Color.White;

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
                guna2Button2.BorderColor = Color.Black;
                guna2Button2.ForeColor = Color.Black;

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
            // Другие операции и инициализации формы
            this.Username = username;
            label1.Text = "Welcome, " + username + "!";
            guna2TextBox1.TextChanged += Guna2TimeTextBox_TextChanged;
            guna2TextBox1.KeyPress += Guna2TimeTextBox_KeyPress;
            this.Resize += Form5_Resize;
        }

        private void CenterLabel()
        {
            // Рассчитываем новые координаты для центрирования Label по горизонтали
            int labelX = (ClientSize.Width - label1.Width) / 2;
            int labelY = label1.Location.Y; // Не изменяем положение Label по вертикали

            // Устанавливаем новые координаты Label
            label5.Location = new System.Drawing.Point(labelX, labelY);
        }

        private void Form5_Resize(object sender, EventArgs e)
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
            CenterLabel();
            // Установка размеров кнопки guna2Button1 пропорционально изменению размеров формы
            guna2Button1.Width = (int)(this.Width * 0.2); // Например, кнопка будет занимать 20% ширины формы
            guna2Button1.Height = (int)(this.Height * 0.1); // Например, кнопка будет занимать 10% высоты формы

            guna2Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Button1.Location = new Point(10, this.ClientSize.Height - guna2Button1.Height - 10);


            // Установка размеров кнопки guna2Button1 пропорционально изменению размеров формы
            guna2Button2.Width = (int)(this.Width * 0.2); // Например, кнопка будет занимать 20% ширины формы
            guna2Button2.Height = (int)(this.Height * 0.1); // Например, кнопка будет занимать 10% высоты формы

            // Установка якорей для кнопки
            guna2Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            // Установка положения кнопки в нижнем правом углу
            guna2Button2.Location = new Point(this.ClientSize.Width - guna2Button1.Width - 10, this.ClientSize.Height - guna2Button1.Height - 10);

            // Размещение label2
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point((this.ClientSize.Width - label2.Width) / 2, (this.ClientSize.Height - label2.Height - guna2TextBox1.Height - 10 - 30) / 2 - 30); // Увеличенный отступ на 30 пикселей вверх от центра

            // Размещение guna2TextBox1
            guna2TextBox1.Anchor = AnchorStyles.None;
            guna2TextBox1.Width = (int)(this.Width * 0.4);
            guna2TextBox1.Location = new Point((this.ClientSize.Width - guna2TextBox1.Width) / 2, label2.Bottom + 10); // Пример смещения на 10 пикселей вниз от label2

            // Размещение label3
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point((this.ClientSize.Width - label3.Width) / 2, guna2TextBox1.Bottom + 10); // Пример смещения на 10 пикселей вниз от guna2TextBox1

            // Размещение guna2TextBox2
            guna2TextBox2.Anchor = AnchorStyles.None;
            guna2TextBox2.Width = (int)(this.Width * 0.4);
            guna2TextBox2.Location = new Point((this.ClientSize.Width - guna2TextBox2.Width) / 2, label3.Bottom + 10); // Пример смещения на 10 пикселей вниз от label3

            // Размещение label4
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Location = new Point((this.ClientSize.Width - label4.Width) / 2, guna2TextBox2.Bottom + 10); // Пример смещения на 10 пикселей вниз от guna2TextBox2

            // Размещение guna2TextBox3
            guna2TextBox3.Anchor = AnchorStyles.None;
            guna2TextBox3.Width = (int)(this.Width * 0.4);
            guna2TextBox3.Location = new Point((this.ClientSize.Width - guna2TextBox3.Width) / 2, label4.Bottom + 10); // Пример смещения на 10 пикселей вниз от label4

        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7(BackGround, Plauplay, Username);
            form7.Size = this.Size;
            // Показываем Form2 и скрываем текущую форму (Form1)
            form7.Show();
            this.Hide();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (monthCalendar1.SelectionStart != DateTime.Today)
            {
                monthCalendar1.SelectionStart = DateTime.Today;
                monthCalendar1.SelectionEnd = DateTime.Today;
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            // Удаляем все символы, кроме цифр и двоеточий
            string formattedText = string.Join("", guna2TextBox1.Text.Where(c => char.IsDigit(c) || c == ':'));

            // Добавляем двоеточие, если его нет и если введенные символы соответствуют формату часов
            if (formattedText.Length == 2 && !formattedText.Contains(":"))
            {
                formattedText = formattedText.Insert(2, ":");
            }

            // Если введены часы, ограничиваем их до 23
            if (formattedText.Length >= 3)
            {
                int hours = int.Parse(formattedText.Substring(0, 2));
                if (hours > 23)
                {
                    formattedText = "23";
                }
            }

            // Если введены минуты, ограничиваем их до 59
            if (formattedText.Length == 5)
            {
                int minutes = int.Parse(formattedText.Substring(3, 2));
                if (minutes > 59)
                {
                    formattedText = formattedText.Substring(0, 3) + "59";
                }
            }

            // Ограничиваем длину текста до 5 символов (формат "00:00")
            if (formattedText.Length > 5)
            {
                formattedText = formattedText.Substring(0, 5);
            }

            // Обновляем текст в textBox
            guna2TextBox1.Text = formattedText;
            guna2TextBox1.SelectionStart = guna2TextBox1.Text.Length; // Перемещаем курсор в конец текста

        }

        private void guna2TimeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox textBox = (Guna2TextBox)sender;

            // Разрешаем только цифры и двоеточие
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ':'))
            {
                e.Handled = true;
            }

            // Проверяем, чтобы двоеточие было введено только один раз и только после двух цифр для формата HH:MM
            if ((e.KeyChar == ':') && (textBox.Text.IndexOf(':') > -1 || textBox.Text.Length >= 5))
            {
                e.Handled = true;
            }
        }

        private void Guna2TimeTextBox_TextChanged(object sender, EventArgs e)
        {
            Guna2TextBox textBox = (Guna2TextBox)sender;

            if (textBox.Text.Length == 2 && !textBox.Text.Contains(":"))
            {
                textBox.Text += ":";
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        private void Guna2TimeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox textBox = (Guna2TextBox)sender;

            // Разрешаем только цифры и двоеточие
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ':'))
            {
                e.Handled = true;
            }

            // Проверяем, чтобы двоеточие было введено только один раз и только после двух цифр для формата HH:MM
            if ((e.KeyChar == ':') && (textBox.Text.IndexOf(':') > -1 || textBox.Text.Length >= 5))
            {
                e.Handled = true;
            }
        }
        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Server=localhost;Database=finance;Uid=root;Password=zhe27;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Проверяем, что все поля заполнены
                    if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "" && guna2TextBox3.Text != "" && Username != "")
                    {
                        string query = "INSERT INTO program (username, time, sum, what, date) VALUES (@username, @time, @sum, @what, @date)";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@username", Username); // Логин из label1
                            command.Parameters.AddWithValue("@time", guna2TextBox1.Text); // Время из guna2TextBox1
                            command.Parameters.AddWithValue("@sum", guna2TextBox2.Text); // Сумма из guna2TextBox2
                            command.Parameters.AddWithValue("@what", guna2TextBox3.Text); // На что из guna2TextBox3
                            command.Parameters.AddWithValue("@date", monthCalendar1.SelectionStart); // Дата из monthCalendar1

                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Данные успешно сохранены в базе данных.");
                        OpenForm1();
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, заполните все поля перед сохранением данных.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }

        }
        // Метод для открытия формы Form1
        void OpenForm1()
        {
            Form5 form5 = new Form5(BackGround, Plauplay, Username);
            form5.Show();
            form5.Size = this.Size;
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // Устанавливаем положение формы
            this.Location = new Point(100, 100); // Пример: x=100, y=100
        }
    }
}
