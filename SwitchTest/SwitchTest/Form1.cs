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

namespace SwitchTest
{
    public partial class Form1 : Form
    {

        public static bool state = true;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread tread = new Thread(TimeShow);
            tread.Start();
        }

        public void TimeShow()
        {
            int j = 0;
            int k = 0;
                for (int i = 0; i < 100; i++)
                {
                    if (state)
                    {
                        Thread.Sleep(1000);
                        label1.Text = (i-k).ToString();
                        j++;
                        this.Invalidate();
                    }
                    else
                    {
                       
                        Thread.Sleep(1000);
                        label2.Text = (i-j).ToString();
                        k++;
                        this.Invalidate();
                    }
                    
                }
            
        }
    }
}
