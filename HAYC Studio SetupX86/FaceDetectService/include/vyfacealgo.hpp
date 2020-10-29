#ifndef __VYFACEALGO_HPP__
#define __VYFACEALGO_HPP__

#ifndef USE_DLIB

#include <cassert>
#include <vector>
#include <string>
#include <assert.h>
#include <windows.h>
#include <vector>
#include "StaticLogger.h"
//#include "stdafx.h"
#include <opencv2/opencv.hpp>
#include <tchar.h>
#include "VYAlgoSDK_def.h"
#include "mtcnn.hpp"
#include "SmartX1App.h"
#include <thread>
#include <mutex>
#include "simple_facetracker.h"
#include "XRingBuffer.h"
#include "imgprocess.h"
#include "simple_facetracker.h"
//#include "cutface.hpp"
using namespace std;
using namespace cv;


_declspec(dllexport) int GetVersion(string &version);
void watchNet(void* net,std::string savefile);
int _cropFaces(cv::Mat &srcimg, cv::Mat &destimg, VYLANDMARKSF &landmarks, CutFace_params &cp);
//void LogWrite(vyalgoLog *logger, int type, int linenum, const std::string str_print_info);
class vyalgoLog
{
public:
	vyalgoLog();
	~vyalgoLog();
	int Init();
	int Uinit();
	void PrintInfo(ILogger::LogLevel ll, string info);
	//CDynamicLogger GetLogger();
	//UINT WINAPI GetLogInfoFunc(LPVOID  pParam);

private:
	CStaticLogger Logger;
	bool logger_hasinit;

};


class  __declspec(dllexport) InitVYAlgo
{

public:
	InitVYAlgo();
	~InitVYAlgo();

	int                    InitModel(const std::string& datafile, int sdk_init_method = SDK_INIT_SIMPLE_MTCNN);
	int                    uInit();
	int                    SetLogFile(vyalgoLog* log);


	std::string             _datapath;
	vyalgoLog*               log_algo_init;
	vector<std::string>     deploy;
	vector<std::string>     models;
	vector<std::string>		meanfiles;
	void                    *lder;

	/*void                    * _net_whole_ex;
	void                    * lder_pex;
	void                    * net_patch1_ex;
	void                    * net_detect_de;*/
private:
	//bool					decodeFile(const std::string datafile);
	//bool					deleteFile(const std::string datafile);




};

class  __declspec(dllexport) Detector
{

public:
	Detector();
	~Detector();

	int                    Initmodle(const std::string& deploy_mode_file, const std::string& model_file, const std::string &mean_file);//提取模型
	int                    Initmodle_pose(const std::string& deploy_mode_file, const std::string& model_file, const std::string &mean_file);
	int                    Detect(std::string Filepath, std::string SavePath);
	int                    DoDetectEx(const std::string strFilePath, std::vector<VYFACELAND> &facesArray, int method = FLAG_DETECT_DL);
	int                    DoDetectEx(VYImage* pImgMemory, std::vector<VYFACELAND> &facesArray, int method = FLAG_DETECT_DL);
	//bool                    DoDetectEx(BYTE* pImgMemory, std::vector<VYCONFRECT> &facesArray, int method = FLAG_DETECT_DL);
	int                    DoDetectEx_FacePose(const std::string strFilePath, std::vector<VYFACELAND> &facesArray, std::vector<VYFACEPOSE> &faceposeArray, int method = FLAG_DETECT_DL);
	int                    DoDetectEx_FacePose(VYImage* pImgMemory, std::vector<VYFACELAND> &facesArray, std::vector<VYFACEPOSE> &faceposeArray, int method = FLAG_DETECT_DL);
	//bool                    DoDetectEx_Landmarks(const std::string strFilePath, std::vector<VYFACELAND> &facesArray, int method = FLAG_DETECT_DL);
	//bool                    DoDetectEx_Landmarks(VYImage* pImgMemory, std::vector<VYFACELAND> &facesArray, int method = FLAG_DETECT_DL);
	int                    DoDetectEx_CutFaces(Mat &image, vector<Mat>  &face_crop_list, std::vector<VYFACELAND> &facesArray, int flag = FLAG_EXTRACT_WITHOUT_DETECTION, int method = FLAG_DETECT_DL);
	int                    DoDetectEx_Landmarks(const std::string strFilePath, vector<Mat> &face_crop_list, std::vector<VYFACELAND> &facesArray, int flag = FLAG_EXTRACT_WITHOUT_DETECTION, int method = FLAG_DETECT_DL);
	int                    DoDetectEx_Landmarks(VYImage* pImgMemory, vector<Mat> &face_crop_list, std::vector<VYFACELAND> &facesArray, int flag = FLAG_EXTRACT_WITHOUT_DETECTION, int method = FLAG_DETECT_DL);
	int                    Init(const std::string& datafile, InitVYAlgo *initalgo, int sdk_init_method=SDK_INIT_SIMPLE_MTCNN);
	int                    Init(InitVYAlgo *initalgo, int sdk_init_method = SDK_INIT_SIMPLE_MTCNN);
	int                    uInit();
	cmtcnn*				   get_mtcnn();
	//bool                    Initmodle(const std::string& deploy_mode_file, const std::string& model_file);


private:
	//bool					decodeFile(std::string datafile);
	//bool					deleteFile(std::string datafile);
	int                     update_face_conf(Mat image, std::vector<VYFACELAND> &facesArray);
	int                     _update_cutFaces(cv::Mat &srcimg, VYFACELAND &landmarks, CutFace_params &cp);

	int                     Splite_img(Mat image, std::vector<VYRECT> &subimg);
	int                    DoDetect_1_single(cv::Mat image, std::vector<VYCONFRECT> &facesArray);
	int                    DoDetect_1_ssd(Mat image, std::vector<VYFACELAND> &facesArray);
	int                    DoDetect_1_mtcnn(Mat image, std::vector<VYFACELAND> &facesArray);
	int                    DoDetect_1_mtcnn_simple_stage(Mat image, std::vector<VYFACELAND> &facesArray);
	int                    DoDetect_2(Mat image, std::vector<VYFACELAND> &facesArray);
	int                    _nms(const std::vector<VYCONFRECT>& srcRects_vy, std::vector<VYCONFRECT>& resRects_vy, float thresh);
	int					    parse_args();
	
	//bool                    cutFaces(cv::Mat &srcimg, cv::Mat &destimg, vector<Point2f> &landmarks, CutFace_params &cp);
private:
	float _nms_th;
	float _confidence;
	int _model_width;
	int _model_height;
	int _face_width;
	int _face_height;
	int _face_size;
	int _pose_estimation;
	//int _feature_size;

	int ec_mc_y;             //在截取后的照片中,两眼中点与嘴中点的距离，用于求缩放比例
	int ec_y;                //在截取后的照片中,两眼中点与上边界的距离
	int crop_face_size;     //最终截取的人脸大小

	float _left, _right, _top, _bottom;//计算人脸姿态 用于Margin人脸框大小的变量
	int _M_height, _M_width;
	
	std::string _datapath;
	void* _net;
	void* _net_face_pose;
	void* lder;
	vyalgoLog *log_de;
	std::string _deploy_model_file;
	std::string _model_file;
	std::string _mean_file;
	int _sdk_init_method;
	cmtcnn *_mtcnn;
	//float*  _input_data;
	//int _height;
	//int _width;
	//int _size;
	//=======================提取整脸特征所用变量===================
	float*  _input_data;
	float*  _input_data_face_pose;
	int _height;
	int _width;
	int _size;
	int _height_face_pose;
	int _width_face_pose;
	int _size_face_pose;
	
private:
	char*                   m_pFrameBuff;
	VYFeature               m_stVYFeature;
};
//Gender discrimination
class  __declspec(dllexport) Gender
{

public:
	Gender();
	~Gender();

	int                    Initmodle(const std::string& deploy_mode_file, const std::string& model_file, const std::string &mean_file);//提取模型
	//bool                    Detect(std::string Filepath, std::string SavePath);
	int                    DoGenderEx(const std::string strFilePath, std::vector<VYFACELAND> &facesArray, std::vector<VYRECTGENDER> &genderArray, int method = FLAG_CALL_FROM_FILE);
	int                    DoGenderEx(VYImage* pImgMemory, std::vector<VYFACELAND> &facesArray, std::vector<VYRECTGENDER> &genderArray, int method = FLAG_CALL_FROM_MEM);
	int                    Init(const std::string& datafile, InitVYAlgo *initalgo);
	int                    Init(InitVYAlgo *initalgo);
	int                    uInit();
	//bool                    Initmodle(const std::string& deploy_mode_file, const std::string& model_file);


private:
	//bool					decodeFile(std::string datafile);
	//bool					deleteFile(std::string datafile);
	int                    DoGender_1(cv::Mat image, std::vector<VYFACELAND> &facesArray, std::vector<VYRECTGENDER> &genderArray);
	int                    DoGender_2(cv::Mat image, std::vector<VYFACELAND> &facesArray, std::vector<VYRECTGENDER> &genderArray);
	int					parse_args();

private:

	int _model_width;
	int _model_height;
	int _face_width;
	int _face_height;
	int _face_size;
	//int _feature_size;

	int ec_mc_y;             //在截取后的照片中,两眼中点与嘴中点的距离，用于求缩放比例
	int ec_y;                //在截取后的照片中,两眼中点与上边界的距离
	int crop_face_size;     //最终截取的人脸大小

	int _gender_estimation;

	std::string _datapath;	
	void* _net;
	void* lder;
	vyalgoLog *log_ge;
	std::string _deploy_model_file;
	std::string _model_file;
	std::string _mean_file;
	float*  _input_data;
	////int _height;
	////int _width;
	////int _size;
	////=======================提取整脸特征所用变量===================
	//float*  _input_data;
	int _height;
	int _width;
	int _size;
private:
	char*                   m_pFrameBuff;
	VYFeature               m_stVYFeature;
};

class _declspec(dllexport)  Extract
{
public:
	Extract();
	~Extract();

public:
	int				       Init(const std::string& datafile, InitVYAlgo *initalgo);//初始化
	int                    Init(InitVYAlgo *initalgo);
	//bool					save5points(std::string strFilePath, VYFeature* pFeature);
	int 				   save5point(std::string strFilePath, VYFeature* pFeature);
	int                    DoExtractEx(std::string strFilePath, VYFeature* pFeature, int flag = FLAG_EXTRACT_WITH_DETECTION);
	int                    DoExtractEx(const std::string strFilePath, std::vector<VYCONFRECT> &facesArray, VYFeature* pFeature, int detmethod = FLAG_DETECT_DL);
	int                    DoExtractEx(Mat image, VYFeature* pFeature, int detmethod = FLAG_DETECT_DL);
	int					   DoExtractEx(std::vector<Mat> images, std::vector<VYFeature*> &pFeature, int detmethod = FLAG_DETECT_DL);
	int                    DoExtractEx(VYImage* pImgMemory, std::vector<VYCONFRECT> &facesArray, VYFeature* pFeature, int detmethod = FLAG_DETECT_DL);

	int					DoExtractEx(BYTE* pImgMemory, unsigned long lMemorySize, VYFeature* pFeature);

	int                    DoExtractEx_GTF(std::string strFilePath, VYFeature* pFeature);//灰度拉伸后提取特征

	int                    Initmodle(const std::string& deploy_mode_file, const std::string& model_file);//提取patch1初始化模型
	int                    Initmodle_patch1(const std::string& deploy_mode_file, const std::string& model_file);//提取整脸特征初始化模型
	int                    Initmodle_ms(const std::string& deploy_mode_file, const std::string& model_file);//提取整脸ms特征初始化模型

	//==================暂时没用================================================================
	int                    DoExtractByPath(std::string strFilePath, VYFeature* pFeature, BYTE* pImgMemory/*=NULL*/, unsigned long* lMemorySize);
	int                    DoExtractByMemory(BYTE* in_pImgMemory, unsigned long in_lMemorySize, VYFeature* pFeature, BYTE* out_pImgMemory/*=NULL*/, unsigned long* out_lMemorySize);
	//============================================================================================================================

	//================================================此处DoExtract与Detect配套使用，分开使用，输入图片必须是128*128，上方DoExtractEx不需要与Detect分开使用============
	int                    DoExtract(BYTE* pImgMemory, unsigned long lMemorySize, VYFeature* pFeature);
	int                    DoExtract(std::string strFilePath, VYFeature* pFeature);
	//============================================================================================================================
	int                    setDetect(Detector *de);
	int					   setBatchSize(int s);

	int                    uInit();
private:
	//bool					decodeFile(std::string datafile);
	//bool					deleteFile(std::string datafile);
	int					parse_args();
	

private:
	int _batch_size;
	Detector *_detector;
	int _model_width;
	int _model_height;
	int _face_width;
	int _face_height;
	int _face_size;
	int _feature_size;

	int ec_mc_y;             //在截取后的照片中,两眼中点与嘴中点的距离，用于求缩放比例
	int ec_y;                //在截取后的照片中,两眼中点与上边界的距离
	int crop_face_size;     //最终截取的人脸大小
	vyalgoLog *log_ex;
	int _down_model_width;// 68;
	int _down_model_height;// 68;
	int _down_top_left_x;// 11;
	int _down_top_left_y;// 47;
	int _down_size;//
	std::string _deploy_model_file;
	std::string _model_file;
	std::string _datapath;
	void* _net;
	void* lder;
	void* net_patch_down;
	void* _net_ms;
	//=======================提取整脸特征所用变量===================
	float*  _input_data;
	float*  _input_data_ms;
	int _height;
	int _width;
	int _size;
	//===============================================================

	float*  _input_data_patch1;
	//int _height_patch1;
	//int _width_patch1;
	//int _size_patch1;

private:
	char*                   m_pFrameBuff;
	VYFeature               m_stVYFeature;
};


class _declspec(dllexport)   Compare
{
public:
	Compare();
	~Compare();

public:
	float L2_value;
	//临时变更的函数，工程中用的是DoCompare
	double          DoCompare_AC(const VYFeature* stSrc, const VYFeature* stDest, double scale);     //A和C特征对比，供DoCompare_GTF使用
	double          DoCompare_AC_GTF(const VYFeature* stSrc, const VYFeature* stDest, double scale); //A的拉伸图像特征和C特征对比，供DoCompare_GTF使用
	double          DoCompare_GTF(const VYFeature *pSrcFeature, const VYFeature *pSrcFeature_GTF, const VYFeature *pDestFeature, double scale);//A和A的拉伸图像特征与C特征对比，取得分较大值
	double          DoCompare(const VYFeature* stSrc, const VYFeature* stDest, double scale);
	float           get_score(float L2_value, string str_s_pro, string str_s_val, string str_d_pro, string str_d_val);
private:
	//std::vector<float>feature;
};

/***********************************************
ClassName：	Tracker
Author: 	YANG Hongbo HUIYI
Data:		2017/3/2017/03/16
Description:
other
***********************************************/
class  __declspec(dllexport) Tracker
{
	typedef struct{
		cv::Mat image;
		std::vector<MTCNN_RECT> faces;
	}FRAME_;
	
public:
	Tracker(int img_width, int img_height, int max_num_faces = 10, int buff_size = 50);
	~Tracker(){

	}

	void init();
	int                    Init(InitVYAlgo *initalgo, int sdk_init_method = SDK_INIT_SIMPLE_MTCNN);
	int                    Init(InitVYAlgo *initalgo, const std::string& deploy_mode_file, const std::string& model_file, int batch_size, int sdk_init_method = SDK_INIT_SIMPLE_MTCNN);
	int					   uInit();
	cv::Mat track(const cv::Mat& frame_in, std::vector<MTCNN_RECT>& faces);
	void facearray2mtcnn_rect(const VYFACELAND &src, MTCNN_RECT &dst);

	int img_width_;
	int img_height_;
	int max_num_faces_;
	std::vector<FRAME_>  buff_;
	unsigned long fid;

	int buff_size_;
	int buff_p_index_;
	int buff_q_index_;
	int detector_working_index_;
	volatile bool stop_detection_;
	Detector detector_;
	
	Simple_Facetracker tracker_;
	std::thread* detector_thread_;

private:

	bool merge(std::vector<MTCNN_RECT>& src, std::vector<MTCNN_RECT>& dst);
	void nms(std::vector<MTCNN_RECT>& rects);
};
/***********************************************
ClassName：	Demesh
Author: 	YANG Hongbo HUIYI
Data:		2017/3/2017/05/27
Description:
other
***********************************************/
class  __declspec(dllexport) Demesh
{
public:
	Demesh();
	~Demesh();	
	int                    Init(InitVYAlgo *initalgo);
	int                    Init(InitVYAlgo *initalgo, const std::string& deploy_mode_file, const std::string& model_file);
	
	int					   uInit();
	int                    DoDemeshEx(const char *filename, std::vector<VYImage*>&ve_pdmImgMemory);
	int					   DoDemeshEx(std::vector<VYImage*>&ve_ImgMemory, std::vector<VYImage*>&ve_pdmImgMemory);
	int                    DoDemesh(std::vector<Mat>& src_imgs, std::vector<Mat>& dest_imgs);
	int					   GetWidth();
	int					   GetHeight();
	int					   GetChannel();


private:
	int                    Initmodle(const std::string& deploy_mode_file, const std::string& model_file);//提取模型
	int					   parse_args();
	std::string _deploy_model_file;
	std::string _model_file;
	int					   demesh_w;
	int                    demesh_h;
	int					   demesh_size;
	float                  demesh_m;
	float                  demesh_s;
	float*  _input_data;

	void* _net;
	

};

class  __declspec(dllexport) VYAlgoInterface
{

public:
	VYAlgoInterface();
	~VYAlgoInterface();

	int                    _InitVYAlgo(const std::string& datafile, int sdk_init_method = SDK_INIT_SIMPLE_MTCNN);
	int                    _UInitVYAlgo();
	Detector* getDetector();
	Gender* getGender();
	Extract* getExtract();
	InitVYAlgo* getInitVYAlgo();
	Compare* getCompare();
	Tracker* getTracker();
	Demesh* getDemesh();
	char appID[32];// = "DEMO_HUIYI";									// 应用程序标识
	long keyHandles = 0;											// 用于获取Smart X1句柄
	long keyNumber = 0;													// 用于获取查找到的Smart X1个数	
	int                    DoDetectEx(const std::string strFilePath, std::vector<VYFACELAND> &facesArray, int method = FLAG_DETECT_DL);
	int                    DoDetectEx(VYImage* pImgMemory, std::vector<VYFACELAND> &facesArray, int method = FLAG_DETECT_DL);
	//bool                    DoDetectEx_Landmarks(const std::string strFilePath, std::vector<VYFACELAND> &facesArray, int method = FLAG_DETECT_DL);
	//bool                    DoDetectEx_Landmarks(VYImage* pImgMemory, std::vector<VYFACELAND> &facesArray,int method = FLAG_DETECT_DL);

	int                    DoExtractEx(const std::string strFilePath, std::vector<VYFACELAND> &facesArray, std::vector<VYFeature*> &pFeature, int flag = FLAG_EXTRACT_WITHOUT_DETECTION, int detmethod = FLAG_DETECT_DL);
	int                    DoExtractEx(VYImage* pImgMemory, std::vector<VYFACELAND> &facesArray, std::vector<VYFeature*> &pFeatureList, int flag = FLAG_EXTRACT_WITHOUT_DETECTION, int detmethod = FLAG_DETECT_DL);
	
	int                    DoExtractExLet(VYImage* pImgMemory, std::vector<VYFeature*> &pFeatureList,int facesnum);
	
	int                    DoGenderEx(const std::string strFilePath, std::vector<VYFACELAND> &facesArray, std::vector<VYRECTGENDER> &genderArray, int method = FLAG_CALL_FROM_FILE);
	int                    DoGenderEx(VYImage* pImgMemory, std::vector<VYFACELAND> &facesArray, std::vector<VYRECTGENDER> &genderArray, int method = FLAG_CALL_FROM_MEM);

	int                    DoDetectEx_FacePose(const std::string strFilePath, std::vector<VYFACELAND> &facesArray, std::vector<VYFACEPOSE> &faceposeArray, int method = FLAG_DETECT_DL);
	int                    DoDetectEx_FacePose(VYImage* pImgMemory, std::vector<VYFACELAND> &facesArray, std::vector<VYFACEPOSE> &faceposeArray, int method = FLAG_DETECT_DL);
	float 				    DoCompare(const VYFeature *pSrcFeature, const VYFeature *pDestFeature, double scale = 1.0);
	int                    DoTrackEx(const std::string strFilePath, std::vector<MTCNN_RECT> &facesArray, VYImage* pImgMemory);
	int                    DoTrackEx(VYImage* pImgMemory, std::vector<MTCNN_RECT> &facesArray);
	int					   DoCutFaces(const VYImage* pImgMemory, VYImage *pOutMemory, std::vector<VYFACELAND> &facesArray);
	int					   DoDemeshEx(const char *filename, std::vector<VYImage*>&ve_pdmImgMemory);
	int					   DoDemeshEx(std::vector<VYImage*>&ve_ImgMemory, std::vector<VYImage*>&ve_pdmImgMemory);
	

private:
	
	vyalgoLog               log_algo;
	Detector				*_detect = NULL;
	Gender					*_gender = NULL;
	Extract					*_extract = NULL;
	InitVYAlgo              *_initalgo = NULL;
	Compare                 *_compare = NULL;
	Tracker					*_tracker = NULL;
	Demesh                  *_demesh = NULL;
	int Check_dog();
	
	int dog_release();
	
	int dog_init();
	


};


#endif
#endif // __EX_HPP__
