#include "DMOMediaBuffer.h"

struct IMediaBuffer_Impl :public IMediaBuffer {

	DWORD _maxlength;
	DWORD _length;
	LPVOID _buffer;

	IMediaBuffer_Impl(DWORD maxlength) {
		_maxlength = maxlength;
		_length = 0;
		_buffer = CoTaskMemAlloc(maxlength);
	}
	
	~IMediaBuffer_Impl() {
		CoTaskMemFree(_buffer);
		_buffer = NULL;
	}

	HRESULT STDMETHODCALLTYPE QueryInterface(
		/* [in] */ REFIID riid,
		/* [iid_is][out] */ _COM_Outptr_ void __RPC_FAR *__RPC_FAR *ppvObject) override {
		*ppvObject = this;
		return S_OK;
	}

	ULONG STDMETHODCALLTYPE AddRef(void) override {
		return 1;
	}

	ULONG STDMETHODCALLTYPE Release(void) override {
		return 0;
	}

	HRESULT STDMETHODCALLTYPE SetLength(
		DWORD cbLength) override {
		if (cbLength > _maxlength)
			return E_INVALIDARG;
		_length = cbLength;
		return S_OK;
	 }

	HRESULT STDMETHODCALLTYPE GetMaxLength(
		/* [annotation][out] */
		_Out_  DWORD *pcbMaxLength) override {
		*pcbMaxLength = _maxlength;
		return S_OK;
	 }

	HRESULT STDMETHODCALLTYPE GetBufferAndLength(
		/* [annotation][out] */
		_Outptr_opt_result_bytebuffer_(*pcbLength)  BYTE **ppBuffer,
		/* [annotation][out] */
		_Out_opt_  DWORD *pcbLength) override {
		if (ppBuffer != NULL)
			*ppBuffer = reinterpret_cast<BYTE*>(_buffer);
		if (pcbLength != NULL)
			*pcbLength = _length;
		return S_OK;
	 }
};

extern "C" {

	IMediaBuffer_Impl* DMOMediaBufferCreate(DWORD maxlength) {
		return new IMediaBuffer_Impl(maxlength);
	}

	void DMOMediaBufferDestroy(IMediaBuffer_Impl * ptr) {
		delete ptr;
	}


	DWORD DMOMediaBuffer_getMaxLength(IMediaBuffer_Impl * ptr) {
		return ptr->_maxlength;
	}
	DWORD  DMOMediaBuffer_getLength(IMediaBuffer_Impl * ptr) {
		return ptr->_length;
	}
	void  DMOMediaBuffer_setLength(IMediaBuffer_Impl * ptr, DWORD length) {
		ptr->_length = length;
	}

	void DMOMediaBufferRead(IMediaBuffer_Impl* ptr, BYTE* buffer, DWORD offset, DWORD count, DWORD sourceOffset) {
		memcpy(buffer + offset, (BYTE*)ptr->_buffer + sourceOffset, count);
	}
	void DMOMediaBufferWrite(IMediaBuffer_Impl* ptr, BYTE* buffer, DWORD offset, DWORD count) {
		memcpy(ptr->_buffer, buffer + offset, count);
		ptr->_length = count;
	}

}
