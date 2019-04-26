using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBAUtils
{
    public partial class Application : Form
    {
        string rebootTime;
        RBAUtils rbautils = new RBAUtils();

        public Application()
        {
            InitializeComponent();

            this.Text = "Ingenico Device RBA Utilities Application";

            if(rbautils.IsConnected())
            { 
                tabControl1.TabPages.Remove(tabPage2);
                rebootTime = rbautils.Get24RebootTime();
                Debug.WriteLine("INITIAL REBOOT TIME VALUE={0}", (object)rebootTime);
                this.txtRebootTime.Text = rebootTime;
            }
            else
            {
                tabControl1.TabPages.Remove(tabPage1);
            }
        }

        private async void SoftBlink(Control ctrl, Color c1, Color c2, short CycleTime_ms, bool BkClr)
        {
            var sw = new Stopwatch(); sw.Start();
            short halfCycle = (short)Math.Round(CycleTime_ms * 0.5);
            while (true)
            {
                await Task.Delay(1);
                var n = sw.ElapsedMilliseconds % CycleTime_ms;
                var per = (double)Math.Abs(n - halfCycle) / halfCycle;
                var red = (short)Math.Round((c2.R - c1.R) * per) + c1.R;
                var grn = (short)Math.Round((c2.G - c1.G) * per) + c1.G;
                var blw = (short)Math.Round((c2.B - c1.B) * per) + c1.B;
                var clr = Color.FromArgb(red, grn, blw);
                if (BkClr) ctrl.BackColor = clr; else ctrl.ForeColor = clr;
            }
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            if(!rbautils.IsConnected())
            { 
                new Thread(() => 
                {
                    SoftBlink(lblWarning1, Color.FromArgb(30, 30, 30), Color.Red, 2000, true);
                    SoftBlink(lblMessage1, Color.FromArgb(30, 30, 30), Color.Green, 2000, true);
                    SoftBlink(lblMessage2, Color.FromArgb(30, 30, 30), Color.Green, 2000, true);
                    SoftBlink(lblMessage3, Color.FromArgb(30, 30, 30), Color.Green, 2000, true);
                    Thread.Sleep(6000);
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }).Start();
            }
        }

        private void OnDeselectingMainTabPage(object sender, TabControlCancelEventArgs e)
        {
            if(this.pictureBox1.Visible)
            {
                e.Cancel = true;
            }
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if(rebootTime.Equals(this.txtRebootTime.Text))
            {
                this.button1.Enabled = false;
            }
            else
            {
                this.button1.Enabled = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            rbautils.Set24RebootTime(this.txtRebootTime.Text);
            this.button1.Enabled = false;
            this.pictureBox1.Visible = true;
            this.panel1.Visible = true;

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Debug.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                Debug.Write("WAITING FOR DEVICE TO COMPLETE REBOOT[ ");
                for(int index = 0; index < 40; index ++)
                { 
                    Debug.Write(".");
                    Thread.Sleep(1000);
                }
                Debug.WriteLine(" ] - ONLINE");
                this.Invoke(new MethodInvoker(() =>
                {
                    rbautils = new RBAUtils();
                    rebootTime = rbautils.Get24RebootTime();
                    Debug.WriteLine("UPDATED REBOOT TIME VALUE={0}", (object)rebootTime);
                    this.txtRebootTime.Text = rebootTime;
                    this.pictureBox1.Visible = false;
                    this.panel1.Visible = false;
                }));
            }).Start();;
        }
    }
}
