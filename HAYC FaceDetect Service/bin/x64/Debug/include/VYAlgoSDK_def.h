#ifndef __VYALGOSDKDEF_H__
#define __VYALGOSDKDEF_H__
#define ALGO_OK (0)		///< 正常运行
#define ALGO_E_INITIAL (-10)	///< 初始化失败
#define ALGO_E_UINITIAL (-11)	///< 初始化失败
#define ALGO_E_IMG (-2)	///< 读取图像失败
#define ALGO_E_MEANFILE (-3)	///< 均值文件错误
#define ALGO_E_FACEDETECTFAIL (-4)///< 人脸检测
#define ALGO_E_EX_5_POINTS (-5)	///< 提取5点失败
#define ALGO_E_EX_FEATURES (-6)	///< 提取验证特征码失败
#define ALGO_E_COMP (-7)
#define ALGO_E_GEND (-8)
#define ALGO_E_POSE (-9)
#define ALGO_W_NOFACE (-41)
#define ALGO_E_VERSION (-12)	///< 初始化失败
#define ALGO_E_LOGFILE (-13)
#define ALGO_E_DOG (-100)//硬狗错误
#define PI 3.1415926535897932384626433832795
//enum detector_method {
//	ssd = 1,
//	normal = 0
//};
enum FLAG_EXTRACT {
	FLAG_EXTRACT_WITH_DETECTION = 1,
	FLAG_EXTRACT_WITHOUT_DETECTION = 2
};
enum FLAG_CALL_METHOD {
	FLAG_CALL_FROM_FILE = 1,
	FLAG_CALL_FROM_MEM = 2,
	FLAG_CALL_FROM_SUPPRSION_MEM = 3
};
enum FLAG_DETECT_METHOD {
	FLAG_DETECT_DL = 1,
	FLAG_DETECT_OV = 2,
	FLAG_DETECT_DL_SIM = 3

};
enum SDK_INIT_METHOD {
	SDK_INIT_SIMPLE_SSD = 1,
	SDK_INIT_SIMPLE_MTCNN = 2,
	SDK_INIT_COMPOSITE_SSD = 3,
	SDK_INIT_COMPOSITE_MTCNN = 4
};
typedef struct _VYFeature
{
	int                 _nFeatureSize;
	char*               _pFeatureData;
}VYFeature;
typedef struct _VYImage
{
	int                 flag;
	int                 _nWidth;
	int                 _nHeight;
	int                 _nDataSize;
	char*               _pData;
}VYImage;
typedef struct _VYRECT
{
	float               _x;
	float               _y;
	float               _width;
	float               _height;

}VYRECT;
typedef struct _VYCONFRECT
{
	VYRECT               _rc;
	float               _confidence;
	float				_rot;
}VYCONFRECT;
typedef struct _VYPOINT
{
	int x;
	int y;
}VYPOINT;
typedef struct _VYPOINTF
{
	float x;
	float y;
}VYPOINTF;
typedef struct _VYLANDMARKSF
{
	VYPOINTF le;
	VYPOINTF re;
	VYPOINTF nose;
	VYPOINTF lm;
	VYPOINTF rm;
}VYLANDMARKSF;
typedef struct _VYFACELAND
{
	VYCONFRECT face;
	VYLANDMARKSF landmarks;

}VYFACELAND;
typedef struct _VYRECTGENDER
{
	VYRECT               _rc;
	float               _confidence;
}VYRECTGENDER;

typedef struct _VYFACEPOSE
{
	VYFACELAND           _rc;
	float                roll;
	float                yaw;
	float                pitch;

}VYFACEPOSE;

typedef struct _VYLANDMARKS
{
	VYPOINT le;
	VYPOINT re;
	VYPOINT nose;
	VYPOINT lm;
	VYPOINT rm;
}VYLANDMARKS;

typedef struct _VYFACES
{
	VYFACEPOSE          _rc;
	float				_gender;	
	
}VYFACES;





typedef struct _BBOX
{
	int x0;
	int y0;
	int x1;
	int y1;
	float score;
	float reg0;
	float reg1;
	float reg2;
	float reg3;
}BBOX;
typedef struct _FBBOX
{
	float x0;
	float y0;
	float x1;
	float y1;
	float score;
}FBBOX;
typedef struct _PAD_PARAMS
{
	int dy;
	int edy;
	int dx;
	int edx;
	int y;
	int ey;
	int x;
	int ex;
	int tmpw;
	int tmph;
}PAD_PARAMS;
typedef struct _cutface_params
{
	int ec_mc_y;             //在截取后的照片中,两眼中点与嘴中点的距离，用于求缩放比例
	int ec_y;                //在截取后的照片中,两眼中点与上边界的距离
	int crop_face_size_x;     //最终截取的人脸大小
	int crop_face_size_y;     //最终截取的人脸大小
}CutFace_params;
/***********************************************
Author: YANG Hongbo HUIYI
Data:	2017/3/2017/03/16
Description:
other
***********************************************/
typedef struct{
	const float area() const{
		return (ex - x + 1)*(ey - y + 1);
	}
	const float width() const{
		return ex - x + 1;
	}
	const float height() const{
		return ey - y + 1;
	}
	void cvt_int(){
		x = int(x);
		y = int(y);
		ex = int(ex);
		ey = int(ey);
	}
	
	VYRECT get_rect() const{
		VYRECT t;
		t._x = x;
		t._y = y;
		t._height = height();
		t._width = width();
		return t;
	}


	float confidence;
	float x;
	float y;
	float ex;
	float ey;
	float x_delta;
	float y_delta;
	float ex_delta;
	float ey_delta;
	float landmarks[10];
	bool valid;
	bool is_new;
	void* tracker_ptr;
	unsigned long frame_id;
}MTCNN_RECT; 
#endif