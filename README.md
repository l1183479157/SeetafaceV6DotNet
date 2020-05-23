1.说明  
  本文演示.net掉用seefacev6各个接口实例，有不好的地方希望大家批评指正  
  seetafacev6项目地址https://github.com/seetafaceengine/SeetaFace6 
  
2.接口功能说明  
 a）返回人脸矩形坐标（x,y,width,heigth）  
 b)返回人脸5点坐标和68点坐标  
 c)返回人脸1024维特征矩阵  
 d）返回两张人脸对比相似度  
 e）返回活体检测状态（0：真实人脸 1：假人脸 2：无法判断3：正在识别）  
 注意：输入参数为1时调用单针检测，输入参数为2时调用多帧检测  
 f）返回年龄  
 g）返回性别  
 i）返回眼睛状态（0：闭眼 1：睁眼：2：不是眼睛区域 3未识别）  
 g）口罩检测 
 
3.运行SeetaFaceTest  
1>添加model文件夹到SeetaFaceTest\bin\Debug下（此处为人脸识别所用的各种模型文件）  
model下载地址在https://github.com/seetafaceengine/SeetaFace6  
2>将SeetaFaceTest\dll目录下的SeetaFace.dll放在SeetaFaceTest\bin\Debug下  
3>将SeetaFaceTest\dll的BitmapToData.dll和Newtonsoft.Json.dll添加到SeetaFaceTest  
项目引用  
4>将SeetaFace\lib\x64下的所有dll添加到SeetaFaceTest\bin\Debug下  
5>将SeetaFaceTest设置为启动项运行即可 
