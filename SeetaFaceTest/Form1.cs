
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeetaFaceTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        

        private void button1_Click_1(object sender, EventArgs e)
        {
            FaceRecognition fr = new FaceRecognition();
            fr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FaceLandMark fl = new FaceLandMark();
            fl.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FaceCompare fc = new FaceCompare();
            fc.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FaceAntiSpoofing fa = new FaceAntiSpoofing();
            fa.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FaceAge_Gender_Eye fage = new FaceAge_Gender_Eye();
            fage.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FaceMask fm = new FaceMask();
            fm.Show();
        }
    }
}
