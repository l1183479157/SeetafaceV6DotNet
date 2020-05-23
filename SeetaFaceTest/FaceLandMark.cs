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
    public partial class FaceLandMark : Form
    {
        public FaceLandMark()
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
            SeetaFace.FivePoints(sid, sb);
            JsonSerializer serializer = new JsonSerializer();
            var faces = serializer.Deserialize<SeetaFace.Facepoints>(
                new JsonTextReader(
                    new StringReader(sb.ToString())
                )
            );

            for (int i = 0; i < 5; i++)
            {
                Graphics g = Graphics.FromImage(Bitmap);
                Pen p = new Pen(Color.Red);
                g.DrawEllipse(p, faces.Facepoint[0].points[i].x, faces.Facepoint[0].points[i].y, 15, 15);

               

             }
            pictureBox2.Image = Bitmap;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap Bitmap = new Bitmap(path);
            var sid = SeetaFace.bitmaptoImagedat(Bitmap);
            StringBuilder sb = new StringBuilder(15000);
            SeetaFace.SixtyEightPoints(sid, sb);
            JsonSerializer serializer = new JsonSerializer();
            var faces = serializer.Deserialize<SeetaFace.Facepoints>(
                new JsonTextReader(
                    new StringReader(sb.ToString())
                )
            );

            for (int i = 0; i < 68; i++)
            {
                Graphics g = Graphics.FromImage(Bitmap);
                Pen p = new Pen(Color.Red);
                g.DrawEllipse(p, faces.Facepoint[0].points[i].x, faces.Facepoint[0].points[i].y, 5, 5);



            }
            pictureBox2.Image = Bitmap;

        }
    }
}
