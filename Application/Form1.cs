using Common.LoggerManager;
using RBAUtils.Utilities;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBAUtils
{
    public partial class Application : Form
    {
        string terminalPort;
        string terminalModel;
        string terminalSerialNumber;
        string terminalTime;
        string rebootTime;
        string devicePartNumber;

        Utils rbautils;

        public Application()
        {
            InitializeComponent();

            this.Text = $"Ingenico Device RBA Utilities Application - {Assembly.GetEntryAssembly().GetName().Version}";
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

            // Panel 2 items
            this.panel2.Visible = true;
            this.pictureWaitDisplay.Visible = true;
            this.lblSearchingforDevice.Visible = true;
            this.lblWaitforDevice.Visible = false;
            this.lblDeviceReboot.Visible = false;

            this.panel1.Visible = true;

            new Thread(() =>
            {
                KillRunningDAL();

                rbautils = new Utils();

                this.Invoke(new MethodInvoker(async () =>
                {
                    this.panel2.Visible = false;
                    this.lblSearchingforDevice.Visible = false;
                    this.pictureWaitDisplay.Visible = false;

                    if (rbautils.IsConnected())
                    {
                        this.txtTerminalPort.Text = terminalPort = rbautils.GetDeviceConnectedPort();
                        Logger.info($"TERMINAL COMM PORT={(object)terminalPort}");
                        this.txtTerminalModel.Text = terminalModel = rbautils.GetTerminalModel();
                        Logger.info($"INITIAL TERMINAL MODEL={(object)terminalModel}"); 
                        this.txtTerminalSN.Text = terminalSerialNumber = rbautils.GetTerminalSerialNumber();
                        Logger.info($"INITIAL TERMINAL SERIAL NUMBER={(object)terminalSerialNumber}");
                        this.txtTerminalTimeStamp.Text = terminalTime = rbautils.GetTerminalTimeStamp();
                        Logger.info($"INITIAL TERMINAL TIME VALUE={(object)terminalTime}");
                        this.txtTerminalRebootTime.Text = rebootTime = rbautils.Get24RebootTime();
                        Logger.info($"INITIAL REBOOT TIME VALUE={(object)rebootTime}");
                        this.txtTerminalPartNum.Text = devicePartNumber = rbautils.GetDevicePartNumber();
                        Logger.info($"DEVICE PART NUMBER={(object)devicePartNumber}");
                        await Task.Run(async () => 
                        { 
                            await Task.Delay(500);
                            this.Invoke(new MethodInvoker(() =>
                            { 
                                this.txtTerminalRebootTime.SelectAll(); 
                                this.txtTerminalRebootTime.Focus(); 
                            }));
                        });
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
                            Process.GetCurrentProcess().Kill();
                        }).Start();
                    }
                }));
            }).Start();
        }

        private void OnDeselectingMainTabPage(object sender, TabControlCancelEventArgs e)
        {
            if (this.pictureWaitDisplay.Visible)
            {
                e.Cancel = true;
            }
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (rebootTime?.Equals(this.txtTerminalTimeStamp.Text) ?? false)
            {
                this.btnUpdate.Enabled = false;
            }
            else
            {
                this.btnUpdate.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rbautils.Set24RebootTime(this.txtTerminalRebootTime.Text);

            this.panel1.Visible = false;
            this.btnUpdate.Enabled = false;

            this.panel2.Visible = true;
            this.pictureWaitDisplay.Visible = true;
            this.lblWaitforDevice.Visible = true;
            this.lblDeviceReboot.Visible = true;

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Debug.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                Debug.Write("WAITING FOR DEVICE TO COMPLETE REBOOT[ ");
                for (int index = 0; index < 40; index++)
                {
                    Debug.Write(".");
                    Thread.Sleep(1000);
                }
                Debug.WriteLine(" ] - ONLINE");
                rbautils = new Utils();
                this.Invoke(new MethodInvoker(async () =>
                {
                    this.txtTerminalPort.Text = terminalPort = rbautils.GetDeviceConnectedPort();
                    Logger.info($"TERMINAL COMM PORT={(object)terminalPort}");
                    this.txtTerminalModel.Text = terminalModel = rbautils.GetTerminalModel();
                    Logger.info($"INITIAL TERMINAL MODEL={(object)terminalModel}");
                    this.txtTerminalSN.Text = terminalSerialNumber = rbautils.GetTerminalSerialNumber();
                    Logger.info($"INITIAL TERMINAL SERIAL NUMBER={(object)terminalSerialNumber}");
                    this.txtTerminalTimeStamp.Text = terminalTime = rbautils.GetTerminalTimeStamp();
                    Logger.info($"INITIAL TERMINAL TIME VALUE={(object)terminalTime}");
                    this.txtTerminalRebootTime.Text = rebootTime = rbautils.Get24RebootTime();
                    Logger.info($"UPDATED REBOOT TIME VALUE={(object)rebootTime}");
                    this.panel2.Visible = false;
                    this.pictureWaitDisplay.Visible = false;
                    this.panel1.Visible = true;
                    await Task.Run(async () =>
                    {
                        await Task.Delay(500);
                        this.Invoke(new MethodInvoker(() =>
                        {
                            this.txtTerminalRebootTime.SelectAll();
                            this.txtTerminalRebootTime.Focus();
                        }));
                    });
                }));
            }).Start(); ;
        }
    }
}
