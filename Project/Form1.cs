using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form1 : Form
    {

        public char[] tanc;
        public Dance danceSelected;
        public int i = 0;

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(52, 73, 94);
        }

        void Form1_KeyPress(object sender, KeyPressEventArgs ev)
        {
            bool rightMove = false;
            if(ev.KeyChar.ToString() == tanc[i].ToString())
                rightMove = true;

            PictureBox move = this.Controls.Find(ev.KeyChar.ToString(), true).FirstOrDefault() as PictureBox;
            if (move == null)
                return;

            if (rightMove)
                move.Image = Project.Properties.Resources.step_true;
            else
                move.Image = Project.Properties.Resources.step_false;
            i++;
            if (i == tanc.Length)
            {
                Thread.Sleep(2000);
                Form2 main = new Form2();
                this.Hide();
                main.ShowDialog();
                this.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(@"D:\dances.json"))
            {
                var raw = file.ReadToEnd();
                var dance = JsonConvert.DeserializeObject<List<Dance>>(raw);
                foreach(var item in dance)
                {
                    if (item.name == Form2.danceInput)
                        danceSelected = item;
                }
                tanc = danceSelected.stepString.ToCharArray();
                Debug.WriteLine(dance);
            }
        }
    }
}
