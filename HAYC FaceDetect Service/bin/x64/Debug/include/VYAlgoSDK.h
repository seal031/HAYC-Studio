#ifndef __VYALGOSDK_HPP__
#define __VYALGOSDK_HPP__
#include "VYAlgoSDK_def.h"

//#include <cassert>
#include <vector>
#include <string>
//#include <assert.h>
//#include <windows.h>
//#include <tchar.h>

#define _VYALGOSDK_EXPORT _declspec(dllexport)


typedef int VYAlgo_result_t;
typedef void* VYAlgoHandle;

_VYALGOSDK_EXPORT 
VYAlgoHandle 
_VYALGOSDK_init(const std::string& datafile, int sdk_init_method = SDK_INIT_SIMPLE_MTCNN);

//_VYALGOSDK_EXPORT
//VYAlgoHandle
//_VYALGOSDK_init(const char *datafile, int sdk_init_method = SDK_INIT_SIMPLE_MTCNN);

_VYALGOSDK_EXPORT 
VYAlgo_result_t 
_VYALGOSDK_uinit(VYAlgoHandle algointerface);

_VYALGOSDK_EXPORT 
VYAlgo_result_t 
_VYALGOSDK_getVersion(std::string &version);

_VYALGOSDK_EXPORT
VYAlgo_result_t
_VYALGOSDK_detect(VYAlgoHandle algointerface, std::string filename, std::vector<VYFACELAND> &facesArray, int method = FLAG_DETECT_DL);

_VYALGOSDK_EXPORT
VYAlgo_result_t
_VYALGOSDK_detect(VYAlgoHandle algointerface, VYImage * pImgMemory, std::vector<VYFACELAND> &facesArray, int method = FLAG_DETECT_DL);



_VYALGOSDK_EXPORT
VYAlgo_result_t
_VYALGOSDK_extract(VYAlgoHandle algointerface, std::string filename, std::vector<VYFACELAND> &facesArray, std::vector<VYFeature*> &pFeatureList, int flag = FLAG_EXTRACT_WITHOUT_DETECTION, int detmethod = FLAG_DETECT_DL);

_VYALGOSDK_EXPORT
VYAlgo_result_t
_VYALGOSDK_extract(VYAlgoHandle algointerface, VYImage * pImgMeory, std::vector<VYFACELAND> &facesArray, std::vector<VYFeature*> &pFeatureList, int flag = FLAG_EXTRACT_WITHOUT_DETECTION, int detmethod = FLAG_DETECT_DL);
_VYALGOSDK_EXPORT
VYAlgo_result_t
_VYALGOSDK_release_extract_result(std::vector<VYFeature*> &pFeatureList);

_VYALGOSDK_EXPORT
float
_VYALGOSDK_compare(VYAlgoHandle algointerface, VYFeature *pFeature1, VYFeature *pFeature2, double(scale));


#endif