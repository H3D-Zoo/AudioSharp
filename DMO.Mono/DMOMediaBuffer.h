#ifndef DMO_MONO_MediaBuffer_h
#define DMO_MONO_MediaBuffer_h 1

#include "DMOCommon.h"
#include <dmo.h>
#include <wmcodecdsp.h>

struct IMediaBuffer_Impl;

extern "C" {
	DLLEXPORT IMediaBuffer_Impl* DMOMediaBufferCreate(DWORD maxlength);
	DLLEXPORT void DMOMediaBufferDestroy(IMediaBuffer_Impl * ptr);

	DLLEXPORT DWORD DMOMediaBuffer_getMaxLength(IMediaBuffer_Impl * ptr);
	DLLEXPORT DWORD  DMOMediaBuffer_getLength(IMediaBuffer_Impl * ptr);
	DLLEXPORT void  DMOMediaBuffer_setLength(IMediaBuffer_Impl * ptr, DWORD length);

	DLLEXPORT void DMOMediaBufferRead(IMediaBuffer_Impl* ptr, BYTE* buffer, DWORD offset, DWORD count, DWORD sourceOffset);
	DLLEXPORT void DMOMediaBufferWrite(IMediaBuffer_Impl* ptr, BYTE* buffer, DWORD offset, DWORD count);
}


#endif