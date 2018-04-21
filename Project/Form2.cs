using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Project
{
    public partial class Form2 : Form
    {

        // Init Variables
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public static Dance danceSelected;
        public static JObject danceList;
        public static string newDanceName;
        public static string mode;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        public Form2()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void start_Click(object sender, EventArgs e)
        {
            mode = "beginner";
            Form1 main = new Form1();
            this.Hide();
            main.ShowDialog();
            this.Close();
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            Debug.WriteLine(danceList);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        void Init()
        {
            List<String> listValues = new List<String>();

            foreach (var item in danceList)
                listValues.Add(item.Value["name"].ToString());
            listBox1.DataSource = listValues;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            using (StreamReader file = new StreamReader(@"D:\dances.json"))
            {
                string raw = file.ReadToEnd();
                danceList = (JObject)JsonConvert.DeserializeObject(raw);
                file.Close();
            }
            Init();
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                ListBox lb = sender as ListBox;
                if (lb != null)
                {
                    var data = danceList[this.listBox1.SelectedItem.ToString()];


                    Dance selected = new Dance();
                    selected.name = data["name"].ToString();
                    selected.stepString = data["stepString"].ToString();
                    danceSelected = selected;


                    dance.Text = data["name"].ToString();
                    difficulty.Text = data["difficulty"].ToString();
                    steps.Text = data["stepString"].ToString().Length.ToString() ;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            mode = "addDance";
            newDanceName = Microsoft.VisualBasic.Interaction.InputBox("Input new dance", "Name:", "Here...");
            Form1 main = new Form1();
            this.Hide();
            main.ShowDialog();
            this.Close();
        }

        private void remove_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex != -1)
                {
                    danceList.Remove(listBox1.SelectedItem.ToString());
                    File.WriteAllText(@"D:\dances.json", Form2.danceList.ToString());
                    Init();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}