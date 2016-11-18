using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCatchVedio
{
    public partial class Form1 : Form
    {
        private string URL;
        private int RemainingTime = 5;
        private int Start = 0;
        public Form1()
        {
            InitializeComponent();
            timer1.Stop();//計時開始
            timer2.Stop();//下載
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            URL = textBox1.Text;
            Start = int.Parse(textBox2.Text);
            label8.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm ss ");
            timer1.Start();//計時開始
            timer1.Interval = 1050;
            timer2.Start();//下載
            timer2.Interval = 2000;
            StartButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = (RemainingTime--).ToString()+" s";
            label10.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm ss ");
            if (RemainingTime == 0)
                RemainingTime = 180;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Start <= int.Parse(textBox3.Text))
            {
                if (Start != int.Parse(textBox2.Text))
                    URL = URL.Replace("pid-"+(Start - 1).ToString(), "pid-" + Start.ToString());
                label7.Text = Start.ToString();
                this.GoalWeb.Url = new Uri(URL);
                Start++;
            }
            else
            {
                timer1.Stop();//計時開始
                timer2.Stop();//下載
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (StopButton.Text == "暫停下載")
            {
                timer1.Stop();//計時開始
                timer2.Stop();//下載
                StopButton.Text = "重新開始";
            }
            else if (StopButton.Text == "重新開始")
            {
                timer1.Start();//計時開始
                timer2.Start();//下載
                StopButton.Text = "暫停下載";
            }
        }
    }
}
