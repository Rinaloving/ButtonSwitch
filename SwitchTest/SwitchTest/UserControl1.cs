using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using System.Xml.Linq;

namespace SwitchTest
{
    public enum CheckStyle { style1 = 0 };

    delegate void SetSwitchState(string keyName, string newKeyValue);


    public partial class UserControl1 : UserControl
    {
        //string switchstate = root.Element("switchstate").FirstNode.ToString();
        public static bool isCheck = false;
        public UserControl1()
        {
            InitializeComponent();

            //设置Style支持透明背景色并且双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;




            this.Cursor = Cursors.Hand;
            this.Size = new Size(87, 27);
        }
        public static string path = @"myApp.xml";
        static XElement root = XElement.Load(path); // bin/Debug目录下


        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked
        {
            set { isCheck = value; this.Invalidate(); }
            get { return isCheck; }
        }

        CheckStyle checkStyle = CheckStyle.style1;
        /// <summary>
        /// 样式
        /// </summary>
        public CheckStyle CheckStyleX
        {
            set { checkStyle = value; this.Invalidate(); }
            get { return checkStyle; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap bitMapOn = null;
            Bitmap bitMapOff = null;


            if (checkStyle == CheckStyle.style1)
            {
                bitMapOn = global::SwitchTest.Properties.Resources.btncheckon1;
                bitMapOff = global::SwitchTest.Properties.Resources.btncheckoff1;
            }


            Graphics g = e.Graphics;
            Rectangle rec = new Rectangle(0, 0, Size.Width, this.Size.Height);


            //Thread td = new Thread(setState);
            // td.Start();

            if (isCheck)
            {
                g.DrawImage(bitMapOn, rec);
                //MessageBox.Show("开关打开了！");
                //SetSwitchState setSwitch = modifyItem;
                modifyItem("switchstate", "true");
                //switchstate = root.Element("switchstate").FirstNode.ToString(); //读取App.config配置文件中的值
                // this.Invoke(setSwitch);
                Form1.state = true;
            }
            else
            {
                g.DrawImage(bitMapOff, rec);
                //SetSwitchState setSwitch = modifyItem;
                modifyItem("switchstate", "false");
                //switchstate = root.Element("switchstate").FirstNode.ToString();  //读取App.config配置文件中的值
                // this.Invoke(setSwitch);

                Form1.state = false;
                // MessageBox.Show("开关关闭了！");
            }
        }
        /// <summary>
        /// 鼠标点击触发开关
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void UserControl1_Click(object sender, EventArgs e)
        //{
        //    isCheck = !isCheck;
        //    this.Invalidate(); //刷新
        //}

        private void UserControl1_MouseClick(object sender, MouseEventArgs e)
        {
            //  switchstate = ConfigurationManager.AppSettings["switchstate"].ToString(); //读取App.config配置文件中的值
            isCheck = !isCheck;
            this.Invalidate(); //刷新
        }



        /// <summary>
        /// 修改myApp.xml配置文件中某个节点的值
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="newKeyValue"></param>
        public static void modifyItem(string keyName, string newKeyValue)
        {
            root.SetElementValue(keyName, newKeyValue);
            root.Save(path);
        }

        public void setState(string keyName)
        {
            try
            {
                isCheck = bool.Parse(root.Element(keyName).FirstNode.ToString());
            }
            catch (Exception ex)
            {
                isCheck = true;
                throw;
            }

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            setState("switchstate");
        }
    }
}