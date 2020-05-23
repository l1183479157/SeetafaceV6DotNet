#pragma warning(disable: 4819)
#include "pch.h"
#include <seeta/Struct_cv.h>
#include <seeta/Struct.h>
#include <seeta/FaceDetector.h>
#include <seeta/FaceLandmarker.h>
#include <seeta/FaceRecognizer.h>
#include <seeta/FaceAntiSpoofing.h>
#include <seeta/AgePredictor.h>
#include <seeta/MaskDetector.h>
#include <seeta/EyeStateDetector.h>
#include <seeta/GenderPredictor.h>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <array>
#include <map>
#include <iostream>
#include "platform.h"
#include <queue>
#include <io.h>
#include <string>
#include <vector>
#include <fstream>
#include <array>
#include <map>
#include <iostream>
#include <chrono>
using namespace std;
seeta::ModelSetting::Device device = seeta::ModelSetting::CPU;

int id = 0;
seeta::ModelSetting FD_model("./model/face_detector.csta", device, id);
seeta::ModelSetting FL_model("./model/face_landmarker_pts5.csta", device, id);
seeta::ModelSetting FR_model("./model/face_recognizer_mask.csta", device, id);
seeta::ModelSetting FAS_model("./model/fas_first.csta", device, id);
seeta::ModelSetting AGE_model("./model/age_predictor.csta");
seeta::ModelSetting GD_model("./model/gender_predictor.csta");
seeta::ModelSetting ES_model("./model/eye_state.csta", device, id);
seeta::ModelSetting MD_model("./model/mask_detector.csta");
seeta::ModelSetting FL_68_model("./model/face_landmarker_pts68.csta");
struct  AgeGender
{
	int age;
	int gender;
	int eye_left_state;
	int eye_right_state;
	
};
std::string int2string(int num)
{
	char str[25];
	_itoa(num, str, 10);
	return str;
}
vector<string> split(string str, string separator)
{
	vector<string> result;
	int cutAt;
	while ((cutAt = str.find_first_of(separator)) != str.npos)
	{
		if (cutAt > 0)
		{
			result.push_back(str.substr(0, cutAt));
		}
		str = str.substr(cutAt + 1);
	}
	if (str.length() > 0)
	{
		result.push_back(str);
	}
	return result;
}
std::string to_stringnew(double val)
{

	char buf[200];

	sprintf(buf, "%.9f", val);

	return std::string(buf);

}
//extern "C" __declspec(dllexport) int Init()
//{
//
//
//	return 1;
//}
extern "C" __declspec(dllexport) int DetectFace(SeetaImageData sid, char* json)
{
	seeta::FaceDetector FD(FD_model);
	FD.set(seeta::FaceDetector::PROPERTY_MIN_FACE_SIZE, 80);
	auto faces = FD.detect(sid);
	std::string result = "[";
	for (int i = 0; i < faces.size; ++i)
	{

		auto face = faces.data[i];
		result += ("{");
		result += ("\"x\":" + int2string(face.pos.x) + ",");
		result += ("\"y\":" + int2string(face.pos.y) + ",");
		result += ("\"height\":" + int2string(face.pos.height) + ",");
		result += ("\"width\":" + int2string(face.pos.width) + "}" + ",");

	}
	result.pop_back();
	result += "]";
	strcpy(json, result.c_str());
	return faces.size;
}
extern "C" __declspec(dllexport) int FivePoints(SeetaImageData sid, char* json)
{
	seeta::FaceDetector FD(FD_model);
	seeta::FaceLandmarker FL(FL_model);
	FD.set(seeta::FaceDetector::PROPERTY_MIN_FACE_SIZE, 80);
	auto faces = FD.detect(sid);
	std::string result = "\{\"facepoint\"\: \[";
	for (int i = 0; i < faces.size; ++i)
	{
		result += "\{\"points\"\: \[";
		auto face = faces.data[i];
		std::vector<SeetaPointF> facefivepoints = FL.mark(sid, face.pos);
		for (int j = 0; j < 5; j++)
		{
			result += "{";
			result += ("\"x\":" + int2string(facefivepoints[j].x) + ",");
			result += ("\"y\":" + int2string(facefivepoints[j].y) + "}" + ",");
		}
		result.pop_back();
		result += "\]\}\,";
	}
	result.pop_back();
	result += "\]\}";
	strcpy(json, result.c_str());
	return faces.size;
}
extern "C" __declspec(dllexport) int SixtyEightPoints(SeetaImageData sid, char* json)
{
	seeta::FaceDetector FD(FD_model);
	seeta::FaceLandmarker FL(FL_68_model);
	FD.set(seeta::FaceDetector::PROPERTY_MIN_FACE_SIZE, 80);
	auto faces = FD.detect(sid);
	std::string result = "\{\"facepoint\"\: \[";
	for (int i = 0; i < faces.size; ++i)
	{
		result += "\{\"points\"\: \[";
		auto face = faces.data[i];
		std::vector<SeetaPointF> facefivepoints = FL.mark(sid, face.pos);
		for (int j = 0; j < 68; j++)
		{
			result += "{";
			result += ("\"x\":" + int2string(facefivepoints[j].x) + ",");
			result += ("\"y\":" + int2string(facefivepoints[j].y) + "}" + ",");
		}
		result.pop_back();
		result += "\]\}\,";
	}
	result.pop_back();
	result += "\]\}";
	strcpy(json, result.c_str());
	return faces.size;
}
extern "C" __declspec(dllexport) int GetFeature(SeetaImageData sid, char* json)
{
	seeta::FaceDetector FD(FD_model);
	seeta::FaceLandmarker FL(FL_model);
	seeta::FaceRecognizer FR(FR_model);
	FD.set(seeta::FaceDetector::PROPERTY_MIN_FACE_SIZE, 80);
	auto feature_size = 1024;
	std::string featurearray = "";
	std::unique_ptr<float[]> features(new float[feature_size]);
	auto faces = FD.detect(sid);
	SeetaPointF points[5];
	FL.mark(sid, faces.data[0].pos, points);
	FR.Extract(sid, points, features.get());
	auto aa = features.get();
	for (int i = 0; i < feature_size; ++i)
	{
		featurearray += to_stringnew(*aa);
		featurearray += ",";
		++aa;
	}
	strcpy(json, featurearray.c_str());
	return 1;
}
extern "C" __declspec(dllexport) float CalculateSimilarity(char* a, char* b)
{
	seeta::FaceRecognizer FR(FR_model);
	std::vector<std::string> result_a = split(a, ",");
	std::vector<std::string> result_b = split(b, ",");
	float features_a[1024] = { 0 };
	float features_b[1024] = { 0 };
	for (int i = 0; i < 1024; ++i)
	{
		features_a[i] = atof(result_a[i].c_str());
		features_b[i] = atof(result_b[i].c_str());
	}
	auto cal = FR.CalculateSimilarity(features_a, features_b);
	return cal;
}
extern "C" __declspec(dllexport) int  FaceAntiSpoofing(SeetaImageData sid ,int model)
{
	try 
	{
		seeta::FaceDetector FD(FD_model);
		seeta::FaceLandmarker FL(FL_model);
		seeta::FaceAntiSpoofing FAS(FAS_model);
		FAS_model.append("./model/fas_second.csta");
		//FAS.SetThreshold(0.3, 0.90);
		FD.set(seeta::FaceDetector::PROPERTY_MIN_FACE_SIZE, 80);
		auto faces = FD.detect(sid);
		auto face = faces.data[0];
		std::vector<SeetaPointF> facefivepoints = FL.mark(sid, face.pos);
		int status = 7;
		if (model == 0)
		{
			status = FAS.Predict(sid, face.pos, facefivepoints.data());
		}
		else
		{
			 status = FAS.PredictVideo(sid, face.pos, facefivepoints.data());
		}
		
		return status;
	}
	catch (exception e)
	{
		
	}
	
	
}
extern "C" __declspec(dllexport)AgeGender AgeandGender(SeetaImageData sid)
{
	/*seeta::ModelSetting setting;
	setting.set_device(SEETA_DEVICE_CPU);
	setting.set_id(0);
	setting.append("./models/a.csta");*/

	/*seeta::EyeStateDetector EBD(setting);*/
	//*new_esd1();
	//*new_esd();
	seeta::FaceDetector FD(FD_model);
	seeta::AgePredictor AP(AGE_model);
	seeta::GenderPredictor GP(GD_model);
	seeta::FaceLandmarker FL(FL_model);
	//seeta::MaskDetector MD(MD_model);
	seeta::EyeStateDetector ES(ES_model);
	//seeta::FaceAntiSpoofing FAS(FAS_model);
	auto faces = FD.detect(sid);
	auto points = FL.mark(sid, faces.data[0].pos);
	int genderint = 0;
	int age = 0;
	int eye_left_state=4;
	int eye_right_state=4;
	AP.PredictAgeWithCrop(sid,points.data(),age);
	seeta::GenderPredictor::GENDER gender;
	GP.PredictGenderWithCrop(sid, points.data(), gender);
	gender == seeta::GenderPredictor::FEMALE ? genderint = 1 : genderint = 0;
	seeta::EyeStateDetector::EYE_STATE left_eye, right_eye;
	ES.Detect(sid, points.data(), left_eye, right_eye);
	switch (left_eye)
	{
	case seeta::v6::EyeStateDetector::EYE_CLOSE:
		eye_left_state = 0;
		break;
	case seeta::v6::EyeStateDetector::EYE_OPEN:
		eye_left_state = 1;
		break;
	case seeta::v6::EyeStateDetector::EYE_RANDOM:
		eye_left_state = 2;
		break;
	case seeta::v6::EyeStateDetector::EYE_UNKNOWN:
		eye_left_state = 3;
		break;
	default:
		break;
	}
	switch (right_eye)
	{
	case seeta::v6::EyeStateDetector::EYE_CLOSE:
		eye_right_state = 0;
		break;
	case seeta::v6::EyeStateDetector::EYE_OPEN:
		eye_right_state = 1;
		break;
	case seeta::v6::EyeStateDetector::EYE_RANDOM:
		eye_right_state = 2;
		break;
	case seeta::v6::EyeStateDetector::EYE_UNKNOWN:
		eye_right_state = 3;
		break;
	default:
		break;
	}
	AgeGender ag;
	ag.age = age;
	ag.gender = genderint;
	ag.eye_left_state = eye_left_state;
	ag.eye_right_state = eye_right_state;
	return ag;
}
extern "C" __declspec(dllexport) float MaskDetect(SeetaImageData sid)
{
	seeta::MaskDetector MD(MD_model);
	seeta::FaceDetector FD(FD_model);
	FD.set(seeta::FaceDetector::PROPERTY_MIN_FACE_SIZE, 80);
	auto faces = FD.detect(sid);
	float score = 0;
	MD.detect(sid,faces.data[0].pos,&score);
	return score;
}