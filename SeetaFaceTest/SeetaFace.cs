using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SeetaFaceTest
{
    public class SeetaFace
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SeetaImageData
        {
            public int width;
            public int height;
            public int channels;
            public IntPtr data;
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct SeetaRect
        {
            public int x;
            public int y;
            public int width;
            public int height;
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct Facepoints
        {
            public Facepoint[] Facepoint;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct Facepoint
        {
            public point[] points;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct point
        {
            public int x;
            public int y;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SeetaFaceInfo
        {
            public SeetaRect pos;
            public float score;
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct Result
        {
            public float score;
            public int index;
        };
        public struct Features
        {
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public float features;
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct AgeGender
        {
            public int age;
            public int gender;
            public int eye_left;
            public int eye_right;
        };
        [DllImport("SeetaFace.dll", EntryPoint = "Init", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Init();
        [DllImport("SeetaFace.dll", EntryPoint = "DetectFace", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DetectFace(SeetaImageData image, StringBuilder json);
        [DllImport("SeetaFace.dll", EntryPoint = "FivePoints", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FivePoints(SeetaImageData image, StringBuilder json);
        [DllImport("SeetaFace.dll", EntryPoint = "SixtyEightPoints", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SixtyEightPoints(SeetaImageData image, StringBuilder json);
        [DllImport("SeetaFace.dll", EntryPoint = "GetFeature", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetFeature(SeetaImageData image, StringBuilder json);
        [DllImport("SeetaFace.dll", EntryPoint = "CalculateSimilarity", CallingConvention = CallingConvention.Cdecl)]
        public static extern float CalculateSimilarity(String a, String b);
        [DllImport("SeetaFace.dll", EntryPoint = "FaceAntiSpoofing", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FaceAntiSpoofing(SeetaImageData image, int model);
        [DllImport("SeetaFace.dll", EntryPoint = "AgeandGender", CallingConvention = CallingConvention.Cdecl)]
        public static extern AgeGender AgeandGender(SeetaImageData image);
        [DllImport("SeetaFace.dll", EntryPoint = "MaskDetect", CallingConvention = CallingConvention.Cdecl)]
        public static extern float MaskDetect(SeetaImageData image);

        public static SeetaImageData bitmaptoImagedat(Bitmap bit)
        {
            SeetaImageData sid = new SeetaImageData()
            {
                width = bit.Width,
                height = bit.Height,
                channels = 3,
                data = BitmapToData.Convert.ToMat(bit)
            };
            return sid;
        }
    }
}
