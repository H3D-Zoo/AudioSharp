#include "DMOCommon.h"

void CheckHRResult(HRESULT hr) {
	if (FAILED(hr)) {
		throw hr;
	}
}
extern "C" {
	void MediaObjectProcessOutput(IMediaObject* pMediaObject, DMO_OUTPUT_DATA_BUFFER* pOutputBuffers) {
		DWORD Status;
		auto hr = pMediaObject->ProcessOutput(0, 1, pOutputBuffers, &Status);
	}
}