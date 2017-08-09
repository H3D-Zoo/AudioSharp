#include "WMResampler.h"

//实际上不需要实现的代码...

struct WMResampler_Impl {
	IMediaObject* _mediaObject;
	IUnknown* _obj;
	IWMResamplerProps* _resamplerprops;

	WMResampler_Impl() {
	  CheckHRResult(CoCreateInstance(CLSID_CResamplerMediaObject,NULL, 0, __uuidof(CResamplerMediaObject),reinterpret_cast<void**>(&_obj)));
	  CheckHRResult(_obj->QueryInterface(&_mediaObject));
	  CheckHRResult(_obj->QueryInterface(&_resamplerprops));
	}

	~WMResampler_Impl() {
		if (_mediaObject != NULL)
			_mediaObject->Release();
		if (_obj != NULL)
			_obj->Release();
		if (_resamplerprops != NULL)
			_resamplerprops->Release();
	}
};

void* DMOWMResamplerCreate() {
	return new WMResampler_Impl();
}

void DMOWMResamlerDestroy(WMResampler_Impl * ptr) {
	delete ptr;
}

IMediaObject* DMOWMResamler_mediaObject(WMResampler_Impl* ptr) {
	return ptr->_mediaObject;
}

IWMResamplerProps* DMOWMResamler_resamplerprops(WMResampler_Impl* ptr) {
	return ptr->_resamplerprops;
}


