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
    public partial class Form1 : Form
    {
        string rebootTime;
        RBAUtils rbautils = new RBAUtils();

        public Form1()
        {
            InitializeComponent();

            this.Text = "Ingenico Device RBA Utilities Application";

            rebootTime = rbautils.Get24RebootTime();
            Debug.WriteLine("INITIAL REBOOT TIME VALUE={0}", (object)rebootTime);
            this.txtRebootTime.Text = rebootTime;
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
