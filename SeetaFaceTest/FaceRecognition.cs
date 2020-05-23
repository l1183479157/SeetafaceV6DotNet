using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeetaFaceTest
{
    public partial class FaceRecognition : Form
    {
        public FaceRecognition()
        {
            InitializeComponent();
        }
        private static string path;
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath + "\\photo";
            ofd.Filter = "(*.jpg,*.png,*.jpeg,*.bmp,*.gif)|*.jpg;*.png;*.jpeg;*.bmp;*.gif|All files(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
                path = ofd.FileName;
            }
            else
            {
                MessageBox.Show("请选择文件");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            Bitmap Bitmap = new Bitmap(path);
            var sid = SeetaFace.bitmaptoImagedat(Bitmap);
            StringBuilder sb = new StringBuilder(15000);
            SeetaFace.DetectFace(sid, sb);
            JsonSerializer serializer = new JsonSerializer();
            var faces = serializer.Deserialize<SeetaFace.SeetaRect[]>(
                new JsonTextReader(
                    new StringReader(sb.ToString())
                )
            );
            Graphics g = Graphics.FromImage(Bitmap);
            Pen p = new Pen(Color.Red);
            for (int i = 0; i < faces.Count(); i++)
            {
                 Rectangle rt = new Rectangle() {
                X = faces[i].x, Y = faces[i].y, Width = faces[i].width, Height = faces[i].height
            };
            g.DrawRectangle(p, rt);
            pictureBox2.Image = Bitmap;
            }
           
            //Rect rect = new Rect(faces[0].x, faces[0].y, faces[0].width, faces[0].height);
            //Bitmap newBitmap = new Bitmap(Bitmap,rect);
            //Cv2.ImShow("111", newBitmap);

        }

      
    }
}
