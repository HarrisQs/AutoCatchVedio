using System;
using System.Windows.Forms;

namespace AutoCatchVedio
{
    public partial class Form1 : Form
    {
        private string URL;
        private int RemainingTime = 180;
        private int Start = 0;
        public Form1()
        {
            InitializeComponent();
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "RunDll32.exe";
            process.StartInfo.Arguments = "InetCpl.cpl,ClearMyTracksByProcess 255";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            URL = textBox1.Text;
            Start = int.Parse(textBox2.Text);
            label8.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm ss ");
            timer1.Start();//計時開始           
            timer2.Start();//下載
            StartButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {//倒數的
            timer1.Interval = 1050;
            label5.Text = (RemainingTime--).ToString()+" s";
            label10.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm ss ");
            if (RemainingTime == 0)
                RemainingTime = 180;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = 180000;
            if (Start <= int.Parse(textBox3.Text))
            {
                if (Start != int.Parse(textBox2.Text))
                    URL = URL.Replace("pid-"+(Start - 1).ToString(), "pid-" + Start.ToString());
                label7.Text = Start.ToString();
                this.GoalWeb.Url = new Uri(URL);
                Start++;
            }
            else//下載完成
            {
                timer1.Stop();//計時開始
                timer2.Stop();//下載
                MessageBox.Show("下載完成摟!");
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (StopButton.Text == "暫停下載")
            {//暫停下載
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
