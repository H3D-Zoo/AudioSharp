#ifndef DMO_MONO_WMResampler_h
#define DMO_MONO_WMResampler_h 1

#include "DMOCommon.h"
#include <dmo.h>
#include <wmcodecdsp.h>

struct WMResampler_Impl;

DLLEXPORT void* DMOWMResamplerCreate();
DLLEXPORT void DMOWMResamlerDestroy(WMResampler_Impl * ptr);
DLLEXPORT IMediaObject* DMOWMResamler_mediaObject(WMResampler_Impl* ptr);
DLLEXPORT IWMResamplerProps* DMOWMResamler_resamplerprops(WMResampler_Impl* ptr);

#endif