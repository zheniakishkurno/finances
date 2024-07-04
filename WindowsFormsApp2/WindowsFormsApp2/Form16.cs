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
    public partial class Form16 : Form
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
        public Form16(int background, int plauplay, string username)
        {
            InitializeComponent();
           
            string connectionString = "Server=localhost;Database=finance;Uid=root;Password=zhe27;";
            connection = new MySqlConnection(connectionString);
            this.Resize += Form16_Resize;
        }
        private void Form16_Resize(object sender, EventArgs e)
        {
            }

        private void Form16_Load(object sender, EventArgs e)
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
    }
}
