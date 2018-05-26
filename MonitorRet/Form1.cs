using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorRet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Worker workerObject = new Worker();
        Thread workerThread;
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            workerThread = new Thread(DoWork);
            workerThread.Start();
            richTextBox1.Text += "main thread: Starting worker thread... \r\n";

            Thread.Sleep(1000);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RequestStop(true);

            //workerThread.Join();

            richTextBox1.Text += "main thread: Worker thread has terminated.\r\n";
        }

        #region
        public void DoWork()
        {
            while (!_shouldStop)
            {
                richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.Text += "worker thread: working...\r\n"; });
            }
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.Text += "worker thread: terminating gracefully. \r\n"; });
        }
        public void RequestStop(bool end)
        {
            _shouldStop = end;
        }
        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop;


        delegate void SetTextCallback(TextBox textBox, string text);
        private void SetText(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { textBox, text });
            }
            else
            {
                textBox.Text = text;
            }
        }

        /*private void SetText(TextBox txt, string text)
        {
            if (txt.InvokeRequired)
            {
                Invoke((MethodInvoker)(() => txt.Text = text));
            }
            else
            {
                txt.Text = text;
            }
        }*/
        #endregion

    }
}
