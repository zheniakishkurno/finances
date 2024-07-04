using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WMPLib;
using OfficeOpenXml;
using OfficeOpenXml.Style;


namespace WindowsFormsApp2
{
    public partial class Form8 : Form
    {
        private string Username;
        public int BackGround = 0;
        public int Plauplay = 0;
        private MySqlConnection connection;
        private string connectionString;
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
        public Form8(int background, int plauplay, string username)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100); // Установка координат X и Y для местоположения формы
                                                 // Устанавливаем контекст лицензирования
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            InitializeComponent();
            // Скрываем label9 при загрузке формы
            label9.Visible = false;
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
            // Инициализация ToolTip для TextBox
            toolTipForTextBox = new ToolTip();
            // Устанавливаем подсказку для TextBox1
            toolTipForTextBox.SetToolTip(guna2TextBox1, "сумма за все время");
            // Инициализация ToolTip для TextBox
            toolTipForTextBox1 = new ToolTip();
            // Устанавливаем подсказку для TextBox1
            toolTipForTextBox1.SetToolTip(guna2TextBox2, "среднее зя все время");
            // Инициализация ToolTip для TextBox
            toolTipForTextBox2 = new ToolTip();
            // Устанавливаем подсказку для TextBox1
            toolTipForTextBox2.SetToolTip(guna2TextBox3, "среднее зя время которое вы выбрали");
            // Инициализация ToolTip для TextBox
            toolTipForTextBox3 = new ToolTip();
            // Устанавливаем подсказку для TextBox1
            toolTipForTextBox3.SetToolTip(guna2TextBox4, "сумма зя время которое вы выбрали");

            // Инициализация ToolTip для TextBox
            ToolTip toolTipForTextBox4 = new ToolTip();
            // Устанавливаем подсказку для TextBox1
            toolTipForTextBox4.SetToolTip(label3, "дата от которрой вы хотите посчитать");
            ToolTip toolTipForTextBox5 = new ToolTip();
            // Устанавливаем подсказку для TextBox1
            toolTipForTextBox5.SetToolTip(label4, "дата до которрой вы хотите посчитать");

            // Другие операции и инициализации формы
            this.Username = username;
            label1.Text = "Welcome, " + username + "!";
            string connectionString = "Server=localhost;Database=finance;Uid=root;Password=zhe27;";
            connection = new MySqlConnection(connectionString);
            this.Resize += Form8_Resize;
        }
        private void Form8_Resize(object sender, EventArgs e)
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
            // Рассчитываем позиции и размеры для кнопок
            int buttonWidth = (int)(this.Width * 0.2); // Ширина кнопок будет 20% ширины формы
            int buttonHeight = (int)(this.Height * 0.1); // Высота кнопок будет 10% высоты формы
            int buttonSpacing = 10; // Расстояние между кнопками и нижним краем формы

            // Первая кнопка
            guna2Button1.Width = buttonWidth;
            guna2Button1.Height = buttonHeight;
            guna2Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Button1.Location = new Point(10, this.ClientSize.Height - buttonHeight - buttonSpacing);

            // Вторая кнопка
            guna2Button2.Width = buttonWidth;
            guna2Button2.Height = buttonHeight;
            guna2Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Button2.Location = new Point((this.ClientSize.Width - buttonWidth) / 2, this.ClientSize.Height - buttonHeight - buttonSpacing);

            // Третье кнопка
            guna2Button8.Width = buttonWidth;
            guna2Button8.Height = buttonHeight;
            guna2Button8.Anchor = AnchorStyles.Bottom | AnchorStyles.Right; // Установка якоря справа и снизу
            guna2Button8.Location = new Point(this.ClientSize.Width - buttonWidth - buttonSpacing, this.ClientSize.Height - buttonHeight - buttonSpacing);

            int textBoxTop1 = 100; // Пример: устанавливаем верхнюю границу на 100 пикселей от верхнего края формы

            // Рассчитываем ширину элементов
            int elementWidth1 = (int)(this.Width * 0.2);

            // Размещение текстовых полей в одну строку
            int textBoxLeft1 = (this.ClientSize.Width - elementWidth1 * 2 - 10) / 2; // Горизонтальное расположение текстовых полей

            // Первое текстовое поле
            guna2TextBox1.Width = elementWidth1;
            guna2TextBox1.Location = new Point(textBoxLeft1, textBoxTop1);

            // Второе текстовое поле
            guna2TextBox2.Width = elementWidth1;
            guna2TextBox2.Location = new Point(textBoxLeft1 + elementWidth1 + 10, textBoxTop1); // Добавляем отступ между текстовыми полями

            // Размещаем лейблы по центру относительно текстовых полей
            label5.Location = new Point(guna2TextBox1.Left + (guna2TextBox1.Width - label5.Width) / 2, guna2TextBox1.Top - label5.Height - 5);
            label6.Location = new Point(guna2TextBox2.Left + (guna2TextBox2.Width - label6.Width) / 2, guna2TextBox2.Top - label6.Height - 5);

            // Размещаем кнопки по центру относительно текстовых полей
            guna2Button3.Width = buttonWidth;
            guna2Button3.Height = buttonHeight;
            guna2Button3.Anchor = AnchorStyles.None;
            guna2Button3.Location = new Point(guna2TextBox1.Left + (guna2TextBox1.Width - buttonWidth) / 2, guna2TextBox1.Bottom + buttonSpacing);

            guna2Button4.Width = buttonWidth;
            guna2Button4.Height = buttonHeight;
            guna2Button4.Anchor = AnchorStyles.None;
            guna2Button4.Location = new Point(guna2TextBox2.Left + (guna2TextBox2.Width - buttonWidth) / 2, guna2TextBox2.Bottom + buttonSpacing);

            // Размещение лейбла между guna2Button3 и guna2Button4
            int labelWidth = label7.Width; // Ширина лейбла
            int totalButtonsWidth = guna2Button3.Width + guna2Button4.Width; // Суммарная ширина двух кнопок
            int labelLeft = guna2Button3.Left + (totalButtonsWidth - labelWidth) / 2; // Определяем левую позицию лейбла
            int labelTop = Math.Max(guna2Button3.Bottom, guna2Button4.Bottom) + 10; // Определяем верхнюю позицию лейбла
            label7.Location = new Point(labelLeft, labelTop); // Устанавливаем позицию лейбла

            // Размещение dateTimePicker1 под лейблом
            dateTimePicker1.Anchor = AnchorStyles.None;
            dateTimePicker1.Width = (int)(this.Width * 0.2);
            dateTimePicker1.Location = new Point((this.ClientSize.Width - dateTimePicker1.Width) / 2, label7.Bottom + 10); // Позиция под лейблом

            // Размещение dateTimePicker2 под dateTimePicker1
            dateTimePicker2.Anchor = AnchorStyles.None;
            dateTimePicker2.Width = (int)(this.Width * 0.2);
            dateTimePicker2.Location = new Point((this.ClientSize.Width - dateTimePicker2.Width) / 2, dateTimePicker1.Bottom + 10); // Позиция под dateTimePicker1

            // Создание и настройка первого лейбла (label3)
            label3.AutoSize = true; // Автоматический размер лейбла
            label3.Location = new Point(textBoxLeft1 - label3.Width - 1, dateTimePicker1.Top); // Позиция лейбла слева от dateTimePicker1

            // Создание и настройка второго лейбла (label4)
            label4.AutoSize = true; // Автоматический размер лейбла
            label4.Location = new Point(textBoxLeft1 - label4.Width - 1, dateTimePicker2.Top); // Позиция лейбла слева от dateTimePicker2

            // Размещение guna2TextBox3 под dateTimePicker2
            guna2TextBox3.Anchor = AnchorStyles.None;
            guna2TextBox3.Width = (int)(this.Width * 0.2);
            guna2TextBox3.Location = new Point((this.ClientSize.Width - guna2TextBox3.Width) / 2, dateTimePicker2.Bottom + 10); // Позиция под dateTimePicker2

            // Размещение guna2Button5 под guna2TextBox3
            guna2Button5.Anchor = AnchorStyles.None;
            guna2Button5.Width = (int)(this.Width * 0.1); // Примерное значение ширины
            guna2Button5.Location = new Point((this.ClientSize.Width - guna2Button5.Width) / 2, guna2TextBox3.Bottom + 5); // Позиция под guna2TextBox3

            // Размещение guna2TextBox4 под guna2Button5
            guna2TextBox4.Anchor = AnchorStyles.None;
            guna2TextBox4.Width = (int)(this.Width * 0.2);
            guna2TextBox4.Location = new Point((this.ClientSize.Width - guna2TextBox4.Width) / 2, guna2Button5.Bottom + 10); // Позиция под guna2Button5

            // Размещение guna2Button6 под guna2TextBox4
            guna2Button6.Anchor = AnchorStyles.None;
            guna2Button6.Width = (int)(this.Width * 0.1); // Примерное значение ширины
            guna2Button6.Location = new Point((this.ClientSize.Width - guna2Button6.Width) / 2, guna2TextBox4.Bottom + 5); // Позиция под guna2TextBox4

            // Размещение label8 под guna2Button6
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.Location = new Point((this.ClientSize.Width - label8.Width) / 2, guna2Button6.Bottom + 10); // Позиция под guna2Button6

            // Размещение dateTimePicker3 под label8
            dateTimePicker3.Anchor = AnchorStyles.None;
            dateTimePicker3.Width = (int)(this.Width * 0.15); // Примерное значение ширины
            dateTimePicker3.Location = new Point((this.ClientSize.Width - dateTimePicker3.Width) / 2, label8.Bottom + 10); // Позиция под label8

            // Размещение guna2TextBox5 под dateTimePicker3, guna2TextBox6 под guna2TextBox5, и label9 под guna2TextBox6
            label9.Anchor = guna2TextBox5.Anchor = guna2TextBox6.Anchor = AnchorStyles.None;
            label9.AutoSize = true;
            label9.Location = new Point((this.ClientSize.Width - label9.Width) / 2, dateTimePicker3.Bottom + 10); // Позиция под dateTimePicker3
            guna2TextBox5.Width = guna2TextBox6.Width = (int)(this.Width * 0.2); // Примерное значение ширины
            int totalWidth = label9.Width + guna2TextBox5.Width + guna2TextBox6.Width;
            int startX = (this.ClientSize.Width - totalWidth) / 2;
            label9.Location = new Point(startX, dateTimePicker3.Bottom + 10);
            guna2TextBox5.Location = new Point(label9.Right, label9.Top);
            guna2TextBox6.Location = new Point(guna2TextBox5.Right, guna2TextBox5.Top);

            // Размещение guna2Button7 под label9 с немного большим отступом
            guna2Button7.Anchor = AnchorStyles.None;
            guna2Button7.Width = (int)(this.Width * 0.1); // Примерное значение ширины
            guna2Button7.Location = new Point((this.ClientSize.Width - guna2Button7.Width) / 2, label9.Bottom + 50); // Немного ниже label9

            // Подстройка размеров DataGridView
            dataGridView1.Dock = DockStyle.Top; // Растягивание только по высоте
            dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top; // Зафиксировать по верхнему краю и растянуть по ширине
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }
        // Загрузка данных в DataGridView
        private void LoadData(string username)
        {
            string query = $"SELECT * FROM program WHERE username = '{username}';";

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Установка источника данных DataGridView
                dataGridView1.DataSource = dt;

                // Скрытие столбцов "логин" и "id"
                dataGridView1.Columns["username"].Visible = false;
                dataGridView1.Columns["id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form14 form14 = new Form14(BackGround, Plauplay, Username);
            form14.Size = this.Size;
            // Показываем Form2 и скрываем текущую форму (Form1)
            form14.Show();
            this.Hide();
        }


        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Проверяем, есть ли хотя бы одна выделенная строка в DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем идентификаторы строк для удаления из базы данных
                List<int> idsToDelete = new List<int>();

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow) // Исключаем строку добавления новых данных
                    {
                        int id = Convert.ToInt32(row.Cells["id"].Value); // Предположим, что "id" - это имя столбца с уникальным идентификатором строки
                        idsToDelete.Add(id);
                    }
                }

                // Удаляем строки из базы данных
                DeleteRowsFromDatabase(idsToDelete);

                // Удаляем строки из DataGridView
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        dataGridView1.Rows.Remove(row);
                    }
                }
            }
            else
            {
                MessageBox.Show("Нет выделенных строк для удаления.");
            }
        }

        // Метод для удаления строк из базы данных
        private void DeleteRowsFromDatabase(List<int> ids)
        {
            try
            {
                connection.Open();

                foreach (int id in ids)
                {
                    string query = $"DELETE FROM program WHERE id = {id};"; // Предположим, что "program" - это имя вашей таблицы
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении строк из базы данных: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            // Устанавливаем положение формы
            this.Location = new Point(100, 100); // Пример: x=100, y=100
            // Загрузить данные с определенным username при загрузке формы
            LoadData(Username);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                // Выполняем запрос, чтобы получить общую сумму столбца "sum"
                string sumQuery = $"SELECT SUM(sum) FROM program WHERE Username = '{Username}';";


                MySqlCommand sumCmd = new MySqlCommand(sumQuery, connection);
                object sumResult = sumCmd.ExecuteScalar();

                if (sumResult != null && sumResult != DBNull.Value)
                {
                    double totalSum = Convert.ToDouble(sumResult);

                    // Выводим результат в TextBox
                    guna2TextBox1.Text = totalSum.ToString("F2"); // Округляем до двух знаков после запятой
                }
                else
                {
                    guna2TextBox1.Text = "Нет данных";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении запроса: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                // Выполняем запрос, чтобы получить среднее значение столбца "sum"
                string avgQuery = $"SELECT AVG(sum) FROM program WHERE Username = '{Username}';";

                MySqlCommand avgCmd = new MySqlCommand(avgQuery, connection);
                object avgResult = avgCmd.ExecuteScalar();

                if (avgResult != null && avgResult != DBNull.Value)
                {
                    double average = Convert.ToDouble(avgResult);

                    // Выводим результат в TextBox
                    guna2TextBox2.Text = average.ToString("F2"); // Округляем до двух знаков после запятой
                }
                else
                {
                    guna2TextBox2.Text = "Нет данных";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении запроса: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                DateTime startDate = dateTimePicker1.Value.Date;
                DateTime endDate = dateTimePicker2.Value.Date;

                // Проверяем, что начальная дата меньше или равна конечной дате
                if (startDate > endDate)
                {
                    MessageBox.Show("Начальная дата не может быть больше конечной даты.");
                    return;
                }

                // Выполняем запрос, чтобы получить среднее значение столбца "sum" за выбранный период
                string avgQuery = $"SELECT AVG(sum) FROM program WHERE date BETWEEN '{startDate:yyyy-MM-dd} 00:00:00' AND '{endDate:yyyy-MM-dd} 23:59:59' AND Username = '{Username}';";
                MySqlCommand avgCmd = new MySqlCommand(avgQuery, connection);
                object avgResult = avgCmd.ExecuteScalar();

                if (avgResult != null && avgResult != DBNull.Value)
                {
                    double average = Convert.ToDouble(avgResult);

                    // Выводим результат в TextBox
                    guna2TextBox3.Text = average.ToString("F2"); // Округляем до двух знаков после запятой
                }
                else
                {
                    guna2TextBox3.Text = "Нет данных";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении запроса: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void toolTipForTextBox2_Popup(object sender, PopupEventArgs e)
        {
            // Создаем экземпляр ToolTip
            ToolTip toolTip1 = new ToolTip();
            // Устанавливаем подсказку для dateTimePicker1
            toolTip1.SetToolTip(dateTimePicker1, "Выберите начальную дату, от которой хотите рассчитать среднее значение");
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                DateTime startDate = dateTimePicker1.Value.Date;
                DateTime endDate = dateTimePicker2.Value.Date;

                // Проверяем, что начальная дата меньше или равна конечной дате
                if (startDate > endDate)
                {
                    MessageBox.Show("Начальная дата не может быть больше конечной даты.");
                    return;
                }

                // Выполняем запрос, чтобы получить сумму столбца "sum" за выбранный период
                string sumQuery = $"SELECT SUM(sum) FROM program WHERE date BETWEEN '{startDate:yyyy-MM-dd} 00:00:00' AND '{endDate:yyyy-MM-dd} 23:59:59' AND Username = '{Username}';";
                MySqlCommand sumCmd = new MySqlCommand(sumQuery, connection);
                object sumResult = sumCmd.ExecuteScalar();

                if (sumResult != null && sumResult != DBNull.Value)
                {
                    double totalSum = Convert.ToDouble(sumResult);

                    // Выводим результат в TextBox
                    guna2TextBox4.Text = totalSum.ToString("F2"); // Округляем до двух знаков после запятой
                }
                else
                {
                    guna2TextBox4.Text = "Нет данных";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении запроса: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            // Показываем label9 и устанавливаем его текст
            label9.Visible = true;
            try
            {
                connection.Open();

                // Получаем выбранную дату из dateTimePicker3 или текущую дату, если дата не выбрана
                DateTime selectedDate = dateTimePicker3.Value.Date;
                if (selectedDate == DateTime.Today && DateTime.Now.TimeOfDay < dateTimePicker3.Value.TimeOfDay)
                {
                    // Если выбран сегодняшний день и время еще не наступило, используем предыдущий день
                    selectedDate = selectedDate.AddDays(-1);
                }

                // Выполняем запрос для вычисления среднего значения за выбранный день
                 string avgQuery = $"SELECT AVG(sum) AS average_sum FROM program WHERE DATE(date) = '{selectedDate.ToString("yyyy-MM-dd")}' AND Username = '{Username}';";

                MySqlCommand avgCmd = new MySqlCommand(avgQuery, connection);
                double average = Convert.ToDouble(avgCmd.ExecuteScalar());

                // При загрузке формы (или при инициализации)
                label9.Visible = false; // Сначала делаем лейбл невидимым
                label9.Text = $"Среднее значение за";
                label9.Visible = true; // Теперь делаем лейбл видимым, так как в нем есть текст
                // Выводим результат в текстовые поля
                guna2TextBox5.Text = selectedDate.ToShortDateString();
                guna2TextBox6.Text = average.ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении запроса: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            SaveToDatabase(Username);
            MessageBox.Show("Данные успешно сохранены в базе данных.");
        }
        private void SaveToDatabase(string Username)
        {
            // Путь к директории
            string directoryPath = @"C:\Users\Женя\Desktop\програмы\проект уп\WindowsFormsApp2\WindowsFormsApp2\bin\Debug";
            string username = Username; // Получаем имя пользователя

            // Генерируем уникальное имя файла для каждого пользователя
            string fileName = Path.Combine(directoryPath, $"{username}_Finances.xlsx");

            // Создаем новый FileInfo для нового файла
            FileInfo newFile = new FileInfo(fileName);

            // Проверка заполнения всех полей
            if (IsAllFieldsFilled())
            {
                using (ExcelPackage package = new ExcelPackage(newFile))
                {
                    // Создаем листы для каждой колонки данных или добавляем данные на существующие листы
                    for (int i = 1; i <= 8; i++)
                    {
                        string columnName = $"Column{i}";

                        // Проверяем, существует ли лист с таким именем
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == columnName);

                        // Если лист не найден, создаем новый
                        if (worksheet == null)
                        {
                            worksheet = package.Workbook.Worksheets.Add(columnName);
                            // Добавляем заголовки на новый лист
                            worksheet.Cells[1, 1].Value = "Логин:";
                            worksheet.Cells[2, 1].Value = username;
                            worksheet.Cells[1, 2].Value = "Название столбца:";
                            worksheet.Cells[2, 2].Value = GetColumnName(i);
                            // Подписываем страницу
                            worksheet.Cells[1, 1].Style.Font.Bold = true;
                            worksheet.Cells[2, 1].Style.Font.Bold = true;
                            worksheet.Cells[4, 1].Style.Font.Bold = true;
                            worksheet.Cells[1, 1, 2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            // Автоподгоняем ширину столбцов
                            worksheet.Cells.AutoFitColumns();
                        }

                        // Записываем данные в соответствующий столбец
                        worksheet.Cells[2, 1].Value = GetColumnValue(i);
                    }

                    // Сохраняем изменения
                    package.Save();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для получения значения для каждой колонки
        private string GetColumnValue(int columnNumber)
        {
            switch (columnNumber)
            {
                case 1:
                    return guna2TextBox1.Text; // Сумма всех
                case 2:
                    return guna2TextBox2.Text; // Среднее по всем
                case 3:
                    return guna2TextBox3.Text; // Среднее по определенному
                case 4:
                    return dateTimePicker1.Value.ToShortDateString(); // Для определенных данных 1
                case 5:
                    return dateTimePicker2.Value.ToShortDateString(); // Для определенных данных 2
                case 6:
                    return guna2TextBox4.Text; // Сумма для определенного
                case 7:
                    return dateTimePicker3.Value.ToShortDateString(); // Дата
                case 8:
                    return guna2TextBox6.Text; // Среднее за один день
                default:
                    return ""; // В случае неизвестного номера колонки возвращаем пустое значение
            }
        }

        // Метод для получения названия столбца по его номеру
        private string GetColumnName(int columnNumber)
        {
            switch (columnNumber)
            {
                case 1:
                    return "Сумма всех";
                case 2:
                    return "Среднее по всем";
                case 3:
                    return "Среднее по определенному";
                case 4:
                    return "Для определенных данных 1";
                case 5:
                    return "Для определенных данных 2";
                case 6:
                    return "Сумма для определенного";
                case 7:
                    return "Дата";
                case 8:
                    return "Среднее за один день";
                default:
                    return ""; // В случае неизвестного номера колонки возвращаем пустое значение
            }
        }

        private bool IsAllFieldsFilled()
        {
            // Проверяем, заполнены ли все текстовые поля и выбраны ли все даты
            return !string.IsNullOrWhiteSpace(guna2TextBox1.Text) &&
                   !string.IsNullOrWhiteSpace(guna2TextBox2.Text) &&
                   !string.IsNullOrWhiteSpace(guna2TextBox3.Text) &&
                   !string.IsNullOrWhiteSpace(guna2TextBox4.Text) &&
                   !string.IsNullOrWhiteSpace(guna2TextBox6.Text) &&
                   dateTimePicker1.Checked &&
                   dateTimePicker2.Checked &&
                   dateTimePicker3.Checked;
        }



        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}

