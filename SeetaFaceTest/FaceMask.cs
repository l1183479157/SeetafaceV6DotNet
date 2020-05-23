using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeetaFaceTest
{
    public partial class FaceMask : Form
    {
        public FaceMask()
        {
            InitializeComponent();
        }
        private static string path1;
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath + "\\photo";
            ofd.Filter = "(*.jpg,*.png,*.jpeg,*.bmp,*.gif)|*.jpg;*.png;*.jpeg;*.bmp;*.gif|All files(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
                path1 = ofd.FileName;
            }
            else
            {
                MessageBox.Show("请选择文件");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bit = new Bitmap(path1);
            var sid = SeetaFace.bitmaptoImagedat(bit);
            var result = SeetaFace.MaskDetect(sid);
            if (result > 0.5)
            {
                MessageBox.Show("佩戴口罩");
            }
            else
            {
                MessageBox.Show("未佩戴口罩");
            }
          
        }
    }
}
