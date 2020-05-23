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
    public partial class FaceCompare : Form
    {
        public FaceCompare()
        {
            InitializeComponent();
        }
        private static string path1;
        private static string path2;
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

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath + "\\photo";
            ofd.Filter = "(*.jpg,*.png,*.jpeg,*.bmp,*.gif)|*.jpg;*.png;*.jpeg;*.bmp;*.gif|All files(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.ImageLocation = ofd.FileName;
                path2 = ofd.FileName;
            }
            else
            {
                MessageBox.Show("请选择文件");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bitmap Bitmap1 = new Bitmap(path1);
            Bitmap Bitmap2 = new Bitmap(path2);
            StringBuilder sb1 = new StringBuilder(15000);
            StringBuilder sb2 = new StringBuilder(15000);
            var sid1 = SeetaFace.bitmaptoImagedat(Bitmap1);
            var sid2 =SeetaFace.bitmaptoImagedat(Bitmap2);
            SeetaFace.GetFeature(sid1, sb1);
            SeetaFace.GetFeature(sid2, sb2);
            var score = SeetaFace.CalculateSimilarity(sb1.ToString(), sb2.ToString());
            MessageBox.Show((score * 100).ToString());
        }
    }
}
