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
    public partial class FaceAntiSpoofing : Form
    {
        public FaceAntiSpoofing()
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
            string path = Application.StartupPath;
            Bitmap Bitmap = new Bitmap(path + "\\timg.jpg");
            var sid = SeetaFace.bitmaptoImagedat(Bitmap);
            var result =SeetaFace.FaceAntiSpoofing(sid, 0);
            switch (result)
            {
                case 0:
                    MessageBox.Show("真实人脸");
                    break;
                case 1:

                    MessageBox.Show("假人脸");
                    break;
                case 2:
                    MessageBox.Show("无法判断");
                    break;
                case 3:
                    MessageBox.Show("正在检测");
                    break;
            }
        }
    }
    
}
