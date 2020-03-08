#ifndef __VYALGOSDKC_HPP__
#define __VYALGOSDKC_HPP__
#include "VYAlgoSDK_def.h"
//#include <cassert>
//#include <vector>
//#include <string>
//#include <assert.h>
//#include <windows.h>
//#include <tchar.h>

#define _VYALGOSDKC_EXPORT _declspec(dllexport)


typedef int VYAlgo_result_t;
typedef void* VYAlgoHandle;

_VYALGOSDKC_EXPORT VYAlgo_result_t _VYALGOSDKC_UID(char *uid);
_VYALGOSDKC_EXPORT VYAlgoHandle
_VYALGOSDKC_init(const char *datafile, int sdk_init_method = SDK_INIT_SIMPLE_MTCNN);
_VYALGOSDKC_EXPORT VYAlgo_result_t _VYALGOSDKC_uinit(VYAlgoHandle algointerface);
_VYALGOSDKC_EXPORT VYAlgo_result_t
_VYALGOSDKC_detect(VYAlgoHandle algointerface, const char *filename, int *facesnum, VYFACELAND **facesArray, int method);
_VYALGOSDKC_EXPORT VYAlgo_result_t
_VYALGOSDKC_detect(VYAlgoHandle algointerface, VYImage* pImgMemory, int *facesnum, VYFACELAND **facesArray, int method);
_VYALGOSDKC_EXPORT VYAlgo_result_t
_VYALGOSDKC_track(VYAlgoHandle algointerface, const char *filename, int *facesnum, MTCNN_RECT **facesArray, VYImage* pImgMemory);
_VYALGOSDKC_EXPORT VYAlgo_result_t
_VYALGOSDKC_track(VYAlgoHandle algointerface, VYImage* pImgMemory, int *facesnum, MTCNN_RECT **facesArray);
_VYALGOSDKC_EXPORT VYAlgo_result_t
_VYALGOSDKC_extract(VYAlgoHandle algointerface, VYImage* pImgMemory, int *facesnum, VYFACELAND **facesArray, VYFeature **pFeatureList, int flag = FLAG_EXTRACT_WITHOUT_DETECTION, int detmethod = FLAG_DETECT_DL);
_VYALGOSDKC_EXPORT VYAlgo_result_t
_VYALGOSDKC_extract(VYAlgoHandle algointerface, const char *filename, int *facesnum, VYFACELAND **facesArray, VYFeature **pFeatureList, int flag = FLAG_EXTRACT_WITHOUT_DETECTION, int detmethod = FLAG_DETECT_DL);
_VYALGOSDKC_EXPORT VYAlgo_result_t
_VYALGOSDKC_extract(VYAlgoHandle algointerface, VYImage* pImgMemory, int *facesnum, VYFeature **pFeatureList);

_VYALGOSDKC_EXPORT VYAlgo_result_t
_VYALGOSDKC_cutface(VYAlgoHandle algointerface, const VYImage* pImgMemory, VYImage* pOutMemory, MTCNN_RECT *personArrays, int *facesnum);
_VYALGOSDKC_EXPORT float
_VYALGOSDKC_compare(VYAlgoHandle algointerface, VYFeature *pFeature1, VYFeature *pFeature2, double(scale));
_VYALGOSDKC_EXPORT VYAlgo_result_t
_VYALGOSDKC_getVersion(char *version);
_VYALGOSDKC_EXPORT VYAlgo_result_t
_VYALGOSDKC_release_detect_result(int *facesnum, VYFACELAND **facesArray);
_VYALGOSDKC_EXPORT VYAlgo_result_t
_VYALGOSDKC_release_extract_result(int *facesnum, VYFeature **pFeatureList);
_VYALGOSDKC_EXPORT VYAlgo_result_t
_VYALGOSDKC_release_track_result(int *facesnum, MTCNN_RECT **facesArray);


_VYALGOSDKC_EXPORT VYAlgo_result_t
_VYALGOSDKC_demesh(VYAlgoHandle algointerface, VYImage** pImgMemory, VYImage** pdmImgMemory,int *num);
_VYALGOSDKC_EXPORT VYAlgo_result_t
_VYALGOSDKC_demesh(VYAlgoHandle algointerface, const char *filename, VYImage** pdmImgMemory,int *num);
_VYALGOSDKC_EXPORT VYAlgo_result_t
_VYALGOSDKC_release_demesh_result(int *imgsnum, VYImage** pImgMemory);


#endif