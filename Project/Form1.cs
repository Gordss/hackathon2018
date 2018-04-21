using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form1 : Form
    {

        public char[] tanc = "12345asfxv".ToCharArray();
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

            string temp = ev.KeyChar.ToString();

            switch (temp)
            {
                case "1" : temp = "one"; break;
                case "2" : temp = "two"; break;
                case "3" : temp = "three"; break;
                case "4" : temp = "four"; break;
                case "5" : temp = "five"; break;
            }
            PictureBox move = this.Controls.Find(temp, true).FirstOrDefault() as PictureBox;
            if (move == null)
                return;

            if (rightMove)
                move.Image = Project.Properties.Resources.step_true;
            else
                move.Image = Project.Properties.Resources.step_false;
            i++;
            if (i == tanc.Length)
                i = 0;
        }

        private void backtomenu_Click(object sender, EventArgs e)
        {
            Form2 menu = new Form2();
            this.Hide();
            menu.ShowDialog();
            this.Close();
        }
    }
}
