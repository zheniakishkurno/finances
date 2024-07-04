using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using WMPLib;

namespace WindowsFormsApp2
{
    public partial class Form12 : Form
    {
        private MySqlConnection connection;
        private string connectionString;
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
        public Form12(int background, int plauplay, string username)
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
            string connectionString = "Server=localhost;Database=finance;Uid=root;Password=zhe27;";
            connection = new MySqlConnection(connectionString);
            this.Activated += Form12_Activated; // Подключаем обработчик события Activated
            this.Resize += Form12_Resize;

            // Связываем textBox1 с ToolTip и устанавливаем описание "Введите имя"
            toolTip1.SetToolTip(guna2TextBox1, "Введите имя");

            // Связываем textBox2 с ToolTip и устанавливаем описание "Введите пароль"
            toolTip2.SetToolTip(guna2TextBox2, "Введите пароль");
        }
        private void Form12_Resize(object sender, EventArgs e)
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
            int rightMargin = 20; // Отступ от правого края формы

            guna2TextBox1.Width = textBoxWidth;
            guna2TextBox1.Height = textBoxHeight;
            guna2TextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2TextBox1.Location = new Point(this.ClientSize.Width - textBoxWidth - rightMargin, 10);

            guna2TextBox2.Width = textBoxWidth;
            guna2TextBox2.Height = textBoxHeight;
            guna2TextBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2TextBox2.Location = new Point(this.ClientSize.Width - textBoxWidth - rightMargin, guna2TextBox1.Bottom + textBoxSpacing);

            // Рассчитываем размеры и положение для кнопок
            int buttonWidth = (int)(this.Width * 0.2); // Ширина кнопок будет 20% ширины формы
            int buttonHeight = (int)(this.Height * 0.1); // Высота кнопок будет 10% высоты формы
            int buttonSpacing = 10; // Расстояние между кнопками

            guna2Button1.Width = buttonWidth;
            guna2Button1.Height = buttonHeight;
            guna2Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Button1.Location = new Point(10, this.ClientSize.Height - buttonHeight - buttonSpacing);

            guna2Button2.Width = buttonWidth;
            guna2Button2.Height = buttonHeight;
            guna2Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Button2.Location = new Point((this.ClientSize.Width - buttonWidth) / 2, this.ClientSize.Height - buttonHeight - buttonSpacing);

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
            guna2DataGridView1.Location = new Point(textBoxWidth + rightMargin, guna2TextBox2.Bottom + textBoxSpacing); // Поменял значение на отступ от правого края
        }
        private void Form12_Activated(object sender, EventArgs e)
        {
            // ReSharper disable once ENC0020
            LoadData(); // Загружаем данные каждый раз, когда форма становится активной
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            // Устанавливаем положение формы
            this.Location = new Point(100, 100); // Пример: x=100, y=100
            // Загрузить данные с определенным username при загрузке формы
            LoadData();
        }
        private void LoadData()
        {
            string query = "SELECT * FROM admin;"; // SQL-запрос для выборки всех данных из таблицы program

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Установка источника данных DataGridView
                guna2DataGridView1.DataSource = dt;
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
            Form9 form9 = new Form9(BackGround, Plauplay, Username);
            form9.Size = this.Size;
            // Показываем Form2 и скрываем текущую форму (Form1)
            form9.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Проверяем, есть ли хотя бы одна выделенная строка в DataGridView
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                // Получаем идентификаторы строк для удаления из базы данных
                List<int> idsToDelete = new List<int>();

                foreach (DataGridViewRow row in guna2DataGridView1.SelectedRows)
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
                foreach (DataGridViewRow row in guna2DataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        guna2DataGridView1.Rows.Remove(row);
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
                    string query = $"DELETE FROM admin WHERE id = {id};"; // Предположим, что "program" - это имя вашей таблицы
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



        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Database=finance;Uid=root;Password=zhe27;";

            bool isValid = true; // Переменная для отслеживания валидации

            // Проверка валидации для guna2TextBox1
            if (guna2TextBox1.Text.Length < 4 || (!IsDigits(guna2TextBox1.Text) && !IsEnglish(guna2TextBox1.Text)))
            {
                ShowError(guna2TextBox1, "Введите не менее 4 символов на английском языке или цифры в первом поле.");
                isValid = false; // Устанавливаем флаг в false, если ввод неверен
            }
            else
            {
                SetValid(guna2TextBox1);
            }

            // Проверка валидации для guna2TextBox2
            if (guna2TextBox2.Text.Length < 4 || (!IsDigits(guna2TextBox2.Text) && !IsEnglish(guna2TextBox2.Text)))
            {
                ShowError(guna2TextBox2, "Введите от 4 до 16 символов на английском языке или цифры во втором поле.");
                isValid = false; // Устанавливаем флаг в false, если ввод неверен
            }
            else
            {
                SetValid(guna2TextBox2);
            }

            if (isValid) // Проверяем, все ли поля валидны
            {
                string username = guna2TextBox1.Text;
                string password = guna2TextBox2.Text;

                // Вставка нового пользователя в таблицу "user"
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string insertQuery = "INSERT INTO admin (username, password) VALUES (@Username, @Password)";
                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            connection.Close();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Пользователь успешно добавлен в базу данных.");
                            }
                            else
                            {
                                MessageBox.Show("Не удалось добавить пользователя в базу данных.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при добавлении пользователя: " + ex.Message);
                        }
                    }
                }
            }

            // Метод для отображения сообщения об ошибке и изменения цвета рамки
            void ShowError(Guna.UI2.WinForms.Guna2TextBox textBox, string errorMessage)
            {
                textBox.BorderColor = System.Drawing.Color.Red;
                MessageBox.Show(errorMessage);
            }

            // Метод для установки цвета рамки валидного поля
            void SetValid(Guna.UI2.WinForms.Guna2TextBox textBox)
            {
                textBox.BorderColor = System.Drawing.Color.Gray;
            }
        }
        // Метод для проверки, содержит ли строка только цифры
        private bool IsDigits(string text)
        {
            return Regex.IsMatch(text, @"^[0-9]+$");
        }

        // Метод для проверки, содержит ли строка только английские буквы
        private bool IsEnglish(string text)
        {
            return Regex.IsMatch(text, @"^[a-zA-Z]+$");
        }


        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
