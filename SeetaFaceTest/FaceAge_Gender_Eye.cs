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
    public partial class FaceAge_Gender_Eye : Form
    {
        public FaceAge_Gender_Eye()
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
            Bitmap Bitmap1 = new Bitmap(path1);
            StringBuilder sb2 = new StringBuilder(15000);
            var sid1 = SeetaFace.bitmaptoImagedat(Bitmap1);
            SeetaFace.AgeGender aa =SeetaFace.AgeandGender(sid1);
            string gender = string.Empty;
            string eye_left = string.Empty;
            string eye_right = string.Empty;
            if (aa.gender == 1)
            {
                gender = "女";
            }
            else
            {
                gender = "男";
            }
            if(aa.eye_left==0)
            {
                eye_left = "闭眼";
            }
            else if(aa.eye_left == 1)
            {
                eye_left = "睁眼";
            }
            else if (aa.eye_left == 2)
            {
                eye_left = "不是眼睛区域";
            }
            else 
            {
                eye_left = "未识别";
            }
          
            if (aa.eye_right == 0)
            {
                eye_right = "闭眼";
            }
            else if (aa.eye_right == 1)
            {
                eye_right = "睁眼";
            }
            else if (aa.eye_right == 2)
            {
                eye_right = "不是眼睛区域";
            }
            else
            {
                eye_right = "未识别";
            }
            MessageBox.Show("年龄：" + aa.age + "性别:" + gender + "左眼" + eye_left + "右眼" + eye_right );
        }
    }
}
