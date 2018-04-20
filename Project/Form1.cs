using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if(e.KeyChar.ToString() == q.Name)
            {
                q.BackColor = Color.Green;
            }
            
        }
    }
}
