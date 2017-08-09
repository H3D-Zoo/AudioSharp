#ifndef DMO_MONO_Common_h
#define DMO_MONO_Common_h 1

#include <dmo.h>

#define DLLEXPORT __declspec(dllexport)

void CheckHRResult(HRESULT hr);

extern "C" {

	DLLEXPORT void MediaObjectProcessOutput(IMediaObject*, DMO_OUTPUT_DATA_BUFFER*);
}

#endif