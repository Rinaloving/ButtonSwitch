using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwitchTest
{
    public enum CheckStyle { style1 = 0 };
    public partial class UserControl1 : UserControl
    {
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


        bool isCheck = false;
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
            Rectangle rec = new Rectangle(0,0,Size.Width, this.Size.Height);

            if (isCheck)
            {
                g.DrawImage(bitMapOn,rec);
                //MessageBox.Show("开关打开了！");
                Form1.state = true;
            }
            else
            {
                g.DrawImage(bitMapOff,rec);
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
        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void UserControl1_MouseClick(object sender, MouseEventArgs e)
        {
            isCheck = !isCheck;
            this.Invalidate(); //刷新
        }
    }
}
