using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form1 : Form
    {

        public int i = 0;
        public char last_keyPressed;
        private string data;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        List<char> newDanceCombo = new List<char>();

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(52, 73, 94);
            if (Form2.danceSelected != null)
                data = Form2.danceSelected.stepString;
            switch (Form2.mode)
            {
                case "addDance":
                    this.KeyPress += new KeyPressEventHandler(this.AddDance);
                    break;
                case "beginner":
                    this.KeyPress += new KeyPressEventHandler(this.DanceBeginner);
                    DanceNextStep(i-1, data);
                    break;
                /*
                case "advanced":
                    this.KeyPress += new KeyPressEventHandler(this.DanceAdvanced);
                    break;
                */
            }
        }

        private void backbutton_click(object sender, EventArgs e)
        {
            Form2 main = new Form2();
            this.Hide();
            main.ShowDialog();
            this.Close();
        }

        void DanceNextStep(int currentStep, string data)
        {
            PictureBox nextStep = new PictureBox();
            nextStep = this.Controls.Find("key_" + data.ToCharArray()[currentStep + 1].ToString(), true).FirstOrDefault() as PictureBox;
            nextStep.Image = Project.Properties.Resources.step_awaiting;
        }

        void DanceBeginner(object sender, KeyPressEventArgs ev)
        {

            if (ev.KeyChar == last_keyPressed)
                return;

            bool rightMove = false;
            string key = ev.KeyChar.ToString().ToLower();
            last_keyPressed = ev.KeyChar;
            PictureBox move;

            if (i == data.Length - 1)
            {

                if (key == data.ToCharArray()[i].ToString())
                {
                    move = this.Controls.Find("key_" + key, true).FirstOrDefault() as PictureBox;
                    rightMove = true;
                }
                else
                    move = this.Controls.Find("key_" + data.ToCharArray()[i].ToString(), true).FirstOrDefault() as PictureBox;

                if (move == null)
                    return;

                if (rightMove)
                    move.Image = Project.Properties.Resources.step_true;
                else
                    move.Image = Project.Properties.Resources.step_false;

                Form2 main = new Form2();
                this.Hide();
                main.ShowDialog();
                this.Close();

                return;
            }

            DanceNextStep(i, data);

            if (key == data.ToCharArray()[i].ToString())
            {
                move = this.Controls.Find("key_" + key, true).FirstOrDefault() as PictureBox;
                rightMove = true;
            }
            else
                move = this.Controls.Find("key_" + data.ToCharArray()[i].ToString(), true).FirstOrDefault() as PictureBox;

            if (move == null)
                return;

            if (rightMove)
                move.Image = Project.Properties.Resources.step_true;
            else
                move.Image = Project.Properties.Resources.step_false;

            i++;
        }

        void AddDance(object sender, KeyPressEventArgs ev)
        {

            if (ev.KeyChar == last_keyPressed)
                return;

            if (ev.KeyChar == '9')
            {
                Dance newDance = new Dance();
                newDance.name = Form2.newDanceName;
                newDance.stepString = string.Join("", newDanceCombo.ToArray()).ToString().ToLower();
                newDance.difficulty = "Beginner";
                newDance.steps = null;

                Form2.danceList.Add(Form2.newDanceName, JToken.FromObject(newDance));
                //Form2.danceList.Remove("pesho");
                Debug.WriteLine(Form2.danceList.ToString());

                if(newDance.stepString.Length != 0)
                    File.WriteAllText(@"D:\dances.json", Form2.danceList.ToString());
                

                Form2 main = new Form2();
                this.Hide();
                main.ShowDialog();
                this.Close();
            }

            last_keyPressed = ev.KeyChar;
            PictureBox move = this.Controls.Find("key_" + ev.KeyChar.ToString(), true).FirstOrDefault() as PictureBox;

            if (move == null)
                return;

            newDanceCombo.Add(ev.KeyChar);
            move.Image = Project.Properties.Resources.step_true;

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
