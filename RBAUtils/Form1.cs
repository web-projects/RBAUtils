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
        RBAUtils rbautils;

        public Application()
        {
            InitializeComponent();

            this.Text = "Ingenico Device RBA Utilities Application";
        }

        private void KillRunningDAL()
        {
            var appManager = Process.GetProcesses().Where(pr => pr.ProcessName.Equals("TCIPAAppManager", StringComparison.CurrentCultureIgnoreCase));

            foreach (var process in appManager)
            {
                try
                { 
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "wmic";
                    startInfo.Arguments = "process where name='TCIPAAppManager.exe' delete";
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    Process processTemp = new Process();
                    processTemp.StartInfo = startInfo;
                    processTemp.EnableRaisingEvents = true;

                    processTemp.Start();

                    startInfo.Arguments = "process where name='TCIPADAL.exe' delete";
                    processTemp.Start();

                    Thread.Sleep(2500);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
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
            tabControl1.TabPages.Remove(tabPage2);
            this.pictureBox1.Visible = true;
            this.label2.Visible = false;
            this.label3.Visible = false;
            this.label4.Visible = true;
            this.panel1.Visible = true;

            new Thread(() => 
            {
                KillRunningDAL();

                rbautils = new RBAUtils();

                this.Invoke(new MethodInvoker(() =>
                {
                    this.label4.Visible = false;
                    this.pictureBox1.Visible = false;
                    this.panel1.Visible = false;

                    if(rbautils.IsConnected())
                    { 
                        rebootTime = rbautils.Get24RebootTime();
                        Debug.WriteLine("INITIAL REBOOT TIME VALUE={0}", (object)rebootTime);
                        this.txtRebootTime.Text = rebootTime;
                    }
                    else
                    {
                        tabControl1.TabPages.Add(tabPage2);
                        tabControl1.TabPages.Remove(tabPage1);

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
                }));
            }).Start();
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
            this.label2.Visible = true;
            this.label3.Visible = true;

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
                rbautils = new RBAUtils();
                this.Invoke(new MethodInvoker(() =>
                {
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
