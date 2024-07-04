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
using WMPLib;
using System.IO; // Для работы с файлами и путями
using OfficeOpenXml; // Для работы с Excel файлами



namespace WindowsFormsApp2
{
    public partial class Form13 : Form
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

        private int currentPageIndex = 0;
        private List<ExcelWorksheet> excelWorksheets = new List<ExcelWorksheet>();
        private List<DataTable> excelDataTables = new List<DataTable>(); // список для хранения данных из Excel


        public Form13(int background, int plauplay, string username)
        {
            InitializeComponent();
            // Устанавливаем контекст лицензирования
          
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

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
            this.Resize += Form13_Resize;
            guna2Button2.Click += guna2Button2_Click;
            guna2Button3.Click += guna2Button3_Click;

        }

        private void Form13_Load(object sender, EventArgs e)
        {
            // Устанавливаем положение формы
            this.Location = new Point(100, 100); // Пример: x=100, y=100
                                                 // Загрузить данные из Excel при загрузке формы
                                                 // Получаем путь к файлу Excel
            string directoryPath = @"C:\Users\Женя\Desktop\програмы\проект уп\WindowsFormsApp2\WindowsFormsApp2\bin\Debug";
            string fileName = Path.Combine(directoryPath, $"{Username}_Finances.xlsx");

            // Загрузить данные из Excel при загрузке формы
            LoadDataFromExcel();
        }

        private void LoadDataFromExcel()
        {
            string directoryPath = @"C:\Users\Женя\Desktop\програмы\проект уп\WindowsFormsApp2\WindowsFormsApp2\bin\Debug";
            string fileName = Path.Combine(directoryPath, $"{Username}_Finances.xlsx");

            try
            {
                if (!File.Exists(fileName))
                {
                    MessageBox.Show("Файл не найден");
                    return;
                }

                excelWorksheets.Clear(); // Очищаем список перед загрузкой новых данных
                excelDataTables.Clear(); // Очищаем список перед загрузкой новых данных

                using (ExcelPackage package = new ExcelPackage(new FileInfo(fileName)))
                {
                    // Загрузить все листы из Excel
                    foreach (var worksheet in package.Workbook.Worksheets)
                    {
                        // Добавляем лист Excel в список excelWorksheets
                        excelWorksheets.Add(worksheet);

                        // Создаем DataTable для каждого листа и добавляем его в список
                        DataTable dt = GetWorksheetData(worksheet);
                        excelDataTables.Add(dt);
                    }

                    // Показываем данные первого листа, если они есть
                    if (excelDataTables.Count > 0)
                    {
                        ShowDataTable(excelDataTables[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных из Excel: " + ex.Message);
            }
        }


        private DataTable GetWorksheetData(ExcelWorksheet worksheet)
        {
            DataTable dt = new DataTable();
            if (worksheet == null)
            {
                return dt;
            }

            try
            {
                foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    dt.Columns.Add(firstRowCell.Text);
                }

                for (int rowNum = 2; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                {
                    DataRow row = dt.Rows.Add();
                    for (int colNum = 1; colNum <= worksheet.Dimension.End.Column; colNum++)
                    {
                        row[colNum - 1] = worksheet.Cells[rowNum, colNum].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных из Excel: " + ex.Message);
            }

            return dt;
        }

        private void ShowDataTable(DataTable dataTable)
        {
            if (dataTable == null)
            {
                return;
            }

            try
            {
                // Отображаем данные из DataTable в вашем элементе управления (например, dataGridView1)
                guna2DataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при отображении данных из DataTable: " + ex.Message);
            }
        }
    

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7(BackGround, Plauplay, Username);
            form7.Size = this.Size;
            // Показываем Form2 и скрываем текущую форму (Form1)
            form7.Show();
            this.Hide();
        }
        private void Form13_Resize(object sender, EventArgs e)
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

            // Рассчитываем размеры и положение для кнопок
            int buttonWidth = (int)(this.Width * 0.2); // Ширина кнопок будет 20% ширины формы
            int buttonHeight = (int)(this.Height * 0.1); // Высота кнопок будет 10% высоты формы
            int buttonSpacing = 10; // Расстояние между кнопками

            guna2Button1.Width = buttonWidth;
            guna2Button1.Height = buttonHeight;
            guna2Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Button1.Location = new Point(10, this.ClientSize.Height - buttonHeight - buttonSpacing);


            // Рассчитываем размеры и положение для DataGridView
            int dataGridViewWidth = this.ClientSize.Width - 50 - textBoxWidth - rightMargin; // Поменял значение на отступ от правого края
            int dataGridViewHeight = this.ClientSize.Height - 2 * (buttonHeight + buttonSpacing) - 2 * textBoxHeight - 3 * textBoxSpacing;
            guna2DataGridView1.Width = dataGridViewWidth;
            guna2DataGridView1.Height = dataGridViewHeight;
            guna2DataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            
            guna2Button2.Width = buttonWidth;
            guna2Button2.Height = buttonHeight;
            guna2Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            // Перемещаем guna2Button2 под guna2DataGridView1 с отступом от нижнего края
            guna2Button2.Location = new Point(guna2DataGridView1.Left, guna2DataGridView1.Bottom + buttonSpacing);

            guna2Button3.Width = buttonWidth;
            guna2Button3.Height = buttonHeight;
            guna2Button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            // Перемещаем guna2Button3 под guna2DataGridView1 с отступом от нижнего края
            guna2Button3.Location = new Point(guna2DataGridView1.Right - buttonWidth, guna2DataGridView1.Bottom + buttonSpacing);

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            currentPageIndex--;
            if (currentPageIndex < 0)
            {
                currentPageIndex = excelDataTables.Count - 1;
            }

            ShowDataTable(excelDataTables[currentPageIndex]); // Изменил вызов метода на ShowDataTable
            UpdateCurrentPageLabel();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            currentPageIndex++;
            if (currentPageIndex >= excelDataTables.Count)
            {
                currentPageIndex = 0;
            }

            ShowDataTable(excelDataTables[currentPageIndex]); // Изменил вызов метода на ShowDataTable
            UpdateCurrentPageLabel();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void UpdateCurrentPageLabel()
        {
            // Получаем название текущего листа из списка excelWorksheets
            string sheetName = "";
            if (excelWorksheets.Count > 0 && currentPageIndex < excelWorksheets.Count)
            {
                sheetName = excelWorksheets[currentPageIndex].Name;
            }

            // Обновляем текст Label для отображения текущего номера листа
            label1.Text = $"Page {currentPageIndex + 1}: {sheetName}";
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
