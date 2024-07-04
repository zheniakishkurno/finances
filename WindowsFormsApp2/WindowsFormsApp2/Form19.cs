﻿using System;
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
    public partial class Form19 : Form
    {
        private string Username;
        public int BackGround = 0;
        public int Plauplay = 0;
        private MySqlConnection connection;
        private static WindowsMediaPlayer player = new WindowsMediaPlayer();
        private static string[] songs = { "song1.mp3" };
        private static int currentSongIndex = 0;

        private const int MinFormWidth = 818;
        private const int MinFormHeight = 497;
        public Form19(int background, int plauplay, string username)
        {
            InitializeComponent();

            // Связываем textBox1 с ToolTip и устанавливаем описание "Введите имя"
            toolTip1.SetToolTip(guna2TextBox1, "среднего за выбранный день");

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
            // Устанавливаем контекст лицензирования
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            this.Username = username;
            label1.Text = "Welcome, " + username + "!";
            string connectionString = "Server=localhost;Database=finance;Uid=root;Password=zhe27;";
            connection = new MySqlConnection(connectionString);
            this.Resize += Form19_Resize;
        }
        private void Form19_Resize(object sender, EventArgs e)
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
            // Рассчитываем размеры и положение для текстовых полей
            int textBoxWidth = (int)(this.Width * 0.2); // Ширина текстовых полей будет 20% ширины формы
            int textBoxHeight = (int)(this.Height * 0.05); // Высота текстовых полей будет 5% высоты формы
            int textBoxSpacing = 10; // Расстояние между текстовыми полями
            int rightMargin = 10; // Отступ от правого края формы
                                  // Позиционирование dateTimePicker1
            dateTimePicker1.Width = textBoxWidth;
            dateTimePicker1.Height = textBoxHeight;
            dateTimePicker1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dateTimePicker1.Location = new Point(this.ClientSize.Width - textBoxWidth - rightMargin, 10);

            // Позиционирование guna2Button2 под guna2TextBox1
            guna2TextBox1.Width = textBoxWidth;
            guna2TextBox1.Height = textBoxHeight;
            guna2TextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2TextBox1.Location = new Point(this.ClientSize.Width - textBoxWidth - rightMargin, dateTimePicker1.Bottom + textBoxSpacing);

            // Позиционирование guna2Button2 под guna2TextBox1
            // Обновляем размеры и положение для кнопки guna2Button2
            // Обновляем размеры и положение для кнопки guna2Button2
            guna2Button2.AutoSize = true; // Разрешаем кнопке автоматически менять размер в зависимости от текста
            guna2Button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // Устанавливаем положение кнопки относительно guna2TextBox1
            guna2Button2.Location = new Point(guna2TextBox1.Right - guna2Button2.Width, guna2TextBox1.Bottom + textBoxSpacing);


            // Рассчитываем размеры и положение для кнопок
            int buttonWidth = (int)(this.Width * 0.2); // Ширина кнопок будет 20% ширины формы
            int buttonHeight = (int)(this.Height * 0.1); // Высота кнопок будет 10% высоты формы
            int buttonSpacing = 10; // Расстояние между кнопками

            // Позиционирование guna2Button1 внизу слева
            guna2Button1.Width = buttonWidth;
            guna2Button1.Height = buttonHeight;
            guna2Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Button1.Location = new Point(10, this.ClientSize.Height - buttonHeight - buttonSpacing);

            // Позиционирование guna2Button3 внизу справа
            guna2Button3.Width = buttonWidth;
            guna2Button3.Height = buttonHeight;
            guna2Button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            guna2Button3.Location = new Point(this.ClientSize.Width - buttonWidth - 10, this.ClientSize.Height - buttonHeight - buttonSpacing);

            // Рассчитываем размеры и положение для DataGridView
            int dataGridViewWidth = this.ClientSize.Width - 50 - textBoxWidth - rightMargin; // Поменял значение на отступ от правого края
            int dataGridViewHeight = this.ClientSize.Height - 2 * (buttonHeight + buttonSpacing) - 2 * textBoxHeight - 3 * textBoxSpacing;
            guna2DataGridView1.Width = dataGridViewWidth;
            guna2DataGridView1.Height = dataGridViewHeight;
            guna2DataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            guna2DataGridView1.Location = new Point(textBoxWidth + rightMargin, guna2Button2.Bottom + textBoxSpacing); // Поменял значение на отступ от правого края
                                                                                                                       // Устанавливаем положение формы

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
                guna2DataGridView1.DataSource = dt;

                // Скрытие столбцов "логин" и "id"
                guna2DataGridView1.Columns["username"].Visible = false;
                guna2DataGridView1.Columns["id"].Visible = false;
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form14 form14 = new Form14(BackGround, Plauplay, Username);
            form14.Size = this.Size;
            // Показываем Form2 и скрываем текущую форму (Form1)
            form14.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                // Получаем выбранную дату из dateTimePicker3 или текущую дату, если дата не выбрана
                DateTime selectedDate = dateTimePicker1.Value.Date;
                if (selectedDate == DateTime.Today && DateTime.Now.TimeOfDay < dateTimePicker1.Value.TimeOfDay)
                {
                    // Если выбран сегодняшний день и время еще не наступило, используем предыдущий день
                    selectedDate = selectedDate.AddDays(-1);
                }

                // Выполняем запрос для вычисления среднего значения за выбранный день
                string avgQuery = $"SELECT AVG(sum) AS average_sum FROM program WHERE DATE(date) = '{selectedDate.ToString("yyyy-MM-dd")}' AND Username = '{Username}';";

                MySqlCommand avgCmd = new MySqlCommand(avgQuery, connection);
                double average = Convert.ToDouble(avgCmd.ExecuteScalar());

                // Выводим результат в текстовый поле
                guna2TextBox1.Text = average.ToString("F2");
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

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form19_Load(object sender, EventArgs e)
        {
            this.Location = new Point(100, 100); // Пример: x=100, y=100
            // Загрузить данные с определенным username при загрузке формы
            LoadData(Username);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

            SaveToDatabase(Username);

            MessageBox.Show("Данные успешно сохранены в базе данных.");
        }

        private void SaveToDatabase(string Username)
        {
            string directoryPath = @"C:\Users\Женя\Desktop\програмы\проект уп\WindowsFormsApp2\WindowsFormsApp2\bin\Debug";
            string username = Username;

            string fileName = Path.Combine(directoryPath, $"{username}_Finances.xlsx");
            FileInfo newFile = new FileInfo(fileName);

            if (!string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                using (ExcelPackage package = new ExcelPackage(newFile))
                {
                    // Имя листа для первой страницы
                    string firstSheetName = "среднего за выбранный день";

                    // Получаем лист для данных пользователя на первой странице
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == firstSheetName);

                    // Если лист не найден, создаем новый
                    if (worksheet == null)
                    {
                        // Создаем новый лист и заполняем его данными
                        worksheet = package.Workbook.Worksheets.Add(firstSheetName);
                        worksheet.Cells[1, 1].Value = "Логин:";
                        worksheet.Cells[2, 1].Value = username;

                        // Добавляем подписи для столбцов
                        worksheet.Cells[1, 2].Value = "Дата";
                        worksheet.Cells[1, 3].Value = "Данные";

                        worksheet.Cells[1, 1].Style.Font.Bold = true;
                        worksheet.Cells[2, 1].Style.Font.Bold = true;
                        worksheet.Cells[1, 1, 2, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                        worksheet.Cells.AutoFitColumns();
                    }

                    // Находим первую пустую строку на листе
                    int nextRow = worksheet.Dimension?.Rows + 1 ?? 1;
                    worksheet.Cells[nextRow, 3].Value = guna2TextBox1.Text;

                    // Сохраняем данные из dateTimePicker1
                    worksheet.Cells[nextRow, 2].Value = dateTimePicker1.Value.ToString("dd/MM/yyyy");

                    package.Save();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните данные для сохранения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





    }
}
