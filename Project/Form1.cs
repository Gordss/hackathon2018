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

        public char[] tanc = "qwertynbvcx".ToCharArray();
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

            Button move = this.Controls.Find(ev.KeyChar.ToString(), true).FirstOrDefault() as Button;
            if (move == null)
                return;

            if (rightMove)
                move.BackColor = Color.Green;
            else
                move.BackColor = Color.Red;
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
