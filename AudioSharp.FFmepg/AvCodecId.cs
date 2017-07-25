namespace AudioSharp.Ffmpeg
{
    /// <summary>
    /// Defines Ffmpeg Codec Ids.
    /// </summary>
    public enum AvCodecId : int
    {
        None = 0,
        Mpeg1Video = 1,
        Mpeg2Video = 2,
        Mpeg2VideoXvmc = 3,
        H261 = 4,
        H263 = 5,
        Rv10 = 6,
        Rv20 = 7,
        Mjpeg = 8,
        Mjpegb = 9,
        Ljpeg = 10,
        Sp5X = 11,
        Jpegls = 12,
        Mpeg4 = 13,
        Rawvideo = 14,
        Msmpeg4V1 = 15,
        Msmpeg4V2 = 16,
        Msmpeg4V3 = 17,
        Wmv1 = 18,
        Wmv2 = 19,
        H263P = 20,
        H263I = 21,
        Flv1 = 22,
        Svq1 = 23,
        Svq3 = 24,
        Dvvideo = 25,
        Huffyuv = 26,
        Cyuv = 27,
        H264 = 28,
        Indeo3 = 29,
        Vp3 = 30,
        Theora = 31,
        Asv1 = 32,
        Asv2 = 33,
        Ffv1 = 34,
        _4Xm = 35,
        Vcr1 = 36,
        Cljr = 37,
        Mdec = 38,
        Roq = 39,
        InterplayVideo = 40,
        XanWc3 = 41,
        XanWc4 = 42,
        Rpza = 43,
        Cinepak = 44,
        WsVqa = 45,
        Msrle = 46,
        Msvideo1 = 47,
        Idcin = 48,
        _8Bps = 49,
        Smc = 50,
        Flic = 51,
        Truemotion1 = 52,
        Vmdvideo = 53,
        Mszh = 54,
        Zlib = 55,
        Qtrle = 56,
        Tscc = 57,
        Ulti = 58,
        Qdraw = 59,
        Vixl = 60,
        Qpeg = 61,
        Png = 62,
        Ppm = 63,
        Pbm = 64,
        Pgm = 65,
        Pgmyuv = 66,
        Pam = 67,
        Ffvhuff = 68,
        Rv30 = 69,
        Rv40 = 70,
        Vc1 = 71,
        Wmv3 = 72,
        Loco = 73,
        Wnv1 = 74,
        Aasc = 75,
        Indeo2 = 76,
        Fraps = 77,
        Truemotion2 = 78,
        Bmp = 79,
        Cscd = 80,
        Mmvideo = 81,
        Zmbv = 82,
        Avs = 83,
        Smackvideo = 84,
        Nuv = 85,
        Kmvc = 86,
        Flashsv = 87,
        Cavs = 88,
        Jpeg2000 = 89,
        Vmnc = 90,
        Vp5 = 91,
        Vp6 = 92,
        Vp6F = 93,
        Targa = 94,
        Dsicinvideo = 95,
        Tiertexseqvideo = 96,
        Tiff = 97,
        Gif = 98,
        Dxa = 99,
        Dnxhd = 100,
        Thp = 101,
        Sgi = 102,
        C93 = 103,
        Bethsoftvid = 104,
        Ptx = 105,
        Txd = 106,
        Vp6A = 107,
        Amv = 108,
        Vb = 109,
        Pcx = 110,
        Sunrast = 111,
        Indeo4 = 112,
        Indeo5 = 113,
        Mimic = 114,
        Rl2 = 115,
        Escape124 = 116,
        Dirac = 117,
        Bfi = 118,
        Cmv = 119,
        Motionpixels = 120,
        Tgv = 121,
        Tgq = 122,
        Tqi = 123,
        Aura = 124,
        Aura2 = 125,
        V210X = 126,
        Tmv = 127,
        V210 = 128,
        Dpx = 129,
        Mad = 130,
        Frwu = 131,
        Flashsv2 = 132,
        Cdgraphics = 133,
        R210 = 134,
        Anm = 135,
        Binkvideo = 136,
        IffIlbm = 137,
        Kgv1 = 138,
        Yop = 139,
        Vp8 = 140,
        Pictor = 141,
        Ansi = 142,
        A64Multi = 143,
        A64Multi5 = 144,
        R10K = 145,
        Mxpeg = 146,
        Lagarith = 147,
        Prores = 148,
        Jv = 149,
        Dfa = 150,
        Wmv3Image = 151,
        Vc1Image = 152,
        Utvideo = 153,
        BmvVideo = 154,
        Vble = 155,
        Dxtory = 156,
        V410 = 157,
        Xwd = 158,
        Cdxl = 159,
        Xbm = 160,
        Zerocodec = 161,
        Mss1 = 162,
        Msa1 = 163,
        Tscc2 = 164,
        Mts2 = 165,
        Cllc = 166,
        Mss2 = 167,
        Vp9 = 168,
        Aic = 169,
        Escape130 = 170,
        G2M = 171,
        Webp = 172,
        Hnm4Video = 173,
        Hevc = 174,
        Fic = 175,
        AliasPix = 176,
        BrenderPix = 177,
        PafVideo = 178,
        Exr = 179,
        Vp7 = 180,
        Sanm = 181,
        Sgirle = 182,
        Mvc1 = 183,
        Mvc2 = 184,
        Hqx = 185,
        Tdsc = 186,
        HqHqa = 187,
        Hap = 188,
        Dds = 189,
        Dxv = 190,
        Screenpresso = 191,
        Rscc = 192,
        Y41P = 32768,
        Avrp = 32769,
        _012V = 32770,
        Avui = 32771,
        Ayuv = 32772,
        TargaY216 = 32773,
        V308 = 32774,
        V408 = 32775,
        Yuv4 = 32776,
        Avrn = 32777,
        Cpia = 32778,
        Xface = 32779,
        Snow = 32780,
        Smvjpeg = 32781,
        Apng = 32782,
        Daala = 32783,
        Cfhd = 32784,
        Truemotion2Rt = 32785,
        M101 = 32786,
        Magicyuv = 32787,
        Sheervideo = 32788,
        Ylc = 32789,
        FirstAudio = 65536,
        PcmS16Le = 65536,
        PcmS16Be = 65537,
        PcmU16Le = 65538,
        PcmU16Be = 65539,
        PcmS8 = 65540,
        PcmU8 = 65541,
        PcmMulaw = 65542,
        PcmAlaw = 65543,
        PcmS32Le = 65544,
        PcmS32Be = 65545,
        PcmU32Le = 65546,
        PcmU32Be = 65547,
        PcmS24Le = 65548,
        PcmS24Be = 65549,
        PcmU24Le = 65550,
        PcmU24Be = 65551,
        PcmS24Daud = 65552,
        PcmZork = 65553,
        PcmS16LePlanar = 65554,
        PcmDvd = 65555,
        PcmF32Be = 65556,
        PcmF32Le = 65557,
        PcmF64Be = 65558,
        PcmF64Le = 65559,
        PcmBluray = 65560,
        PcmLxf = 65561,
        S302M = 65562,
        PcmS8Planar = 65563,
        PcmS24LePlanar = 65564,
        PcmS32LePlanar = 65565,
        PcmS16BePlanar = 65566,
        PcmS64Le = 67584,
        PcmS64Be = 67585,
        AdpcmImaQt = 69632,
        AdpcmImaWav = 69633,
        AdpcmImaDk3 = 69634,
        AdpcmImaDk4 = 69635,
        AdpcmImaWs = 69636,
        AdpcmImaSmjpeg = 69637,
        AdpcmMs = 69638,
        Adpcm_4Xm = 69639,
        AdpcmXa = 69640,
        AdpcmAdx = 69641,
        AdpcmEa = 69642,
        AdpcmG726 = 69643,
        AdpcmCt = 69644,
        AdpcmSwf = 69645,
        AdpcmYamaha = 69646,
        AdpcmSbpro4 = 69647,
        AdpcmSbpro3 = 69648,
        AdpcmSbpro2 = 69649,
        AdpcmThp = 69650,
        AdpcmImaAmv = 69651,
        AdpcmEaR1 = 69652,
        AdpcmEaR3 = 69653,
        AdpcmEaR2 = 69654,
        AdpcmImaEaSead = 69655,
        AdpcmImaEaEacs = 69656,
        AdpcmEaXas = 69657,
        AdpcmEaMaxisXa = 69658,
        AdpcmImaIss = 69659,
        AdpcmG722 = 69660,
        AdpcmImaApc = 69661,
        AdpcmVima = 69662,
        Vima = 69662,
        AdpcmAfc = 71680,
        AdpcmImaOki = 71681,
        AdpcmDtk = 71682,
        AdpcmImaRad = 71683,
        AdpcmG726Le = 71684,
        AdpcmThpLe = 71685,
        AdpcmPsx = 71686,
        AdpcmAica = 71687,
        AdpcmImaDat4 = 71688,
        AdpcmMtaf = 71689,
        AmrNb = 73728,
        AmrWb = 73729,
        Ra144 = 77824,
        Ra288 = 77825,
        RoqDpcm = 81920,
        InterplayDpcm = 81921,
        XanDpcm = 81922,
        SolDpcm = 81923,
        Sdx2Dpcm = 83968,
        Mp2 = 86016,
        Mp3 = 86017,
        Aac = 86018,
        Ac3 = 86019,
        Dts = 86020,
        Vorbis = 86021,
        Dvaudio = 86022,
        Wmav1 = 86023,
        Wmav2 = 86024,
        Mace3 = 86025,
        Mace6 = 86026,
        Vmdaudio = 86027,
        Flac = 86028,
        Mp3Adu = 86029,
        Mp3On4 = 86030,
        Shorten = 86031,
        Alac = 86032,
        WestwoodSnd1 = 86033,
        Gsm = 86034,
        Qdm2 = 86035,
        Cook = 86036,
        Truespeech = 86037,
        Tta = 86038,
        Smackaudio = 86039,
        Qcelp = 86040,
        Wavpack = 86041,
        Dsicinaudio = 86042,
        Imc = 86043,
        Musepack7 = 86044,
        Mlp = 86045,
        GsmMs = 86046,
        Atrac3 = 86047,
        Voxware = 86048,
        Ape = 86049,
        Nellymoser = 86050,
        Musepack8 = 86051,
        Speex = 86052,
        Wmavoice = 86053,
        Wmapro = 86054,
        Wmalossless = 86055,
        Atrac3P = 86056,
        Eac3 = 86057,
        Sipr = 86058,
        Mp1 = 86059,
        Twinvq = 86060,
        Truehd = 86061,
        Mp4Als = 86062,
        Atrac1 = 86063,
        BinkaudioRdft = 86064,
        BinkaudioDct = 86065,
        AacLatm = 86066,
        Qdmc = 86067,
        Celt = 86068,
        G7231 = 86069,
        G729 = 86070,
        _8SvxExp = 86071,
        _8SvxFib = 86072,
        BmvAudio = 86073,
        Ralf = 86074,
        Iac = 86075,
        Ilbc = 86076,
        Opus = 86077,
        ComfortNoise = 86078,
        Tak = 86079,
        Metasound = 86080,
        PafAudio = 86081,
        On2Avc = 86082,
        DssSp = 86083,
        Ffwavesynth = 88064,
        Sonic = 88065,
        SonicLs = 88066,
        Evrc = 88067,
        Smv = 88068,
        DsdLsbf = 88069,
        DsdMsbf = 88070,
        DsdLsbfPlanar = 88071,
        DsdMsbfPlanar = 88072,
        _4Gv = 88073,
        InterplayAcm = 88074,
        Xma1 = 88075,
        Xma2 = 88076,
        Dst = 88077,
        FirstSubtitle = 94208,
        DvdSubtitle = 94208,
        DvbSubtitle = 94209,
        Text = 94210,
        Xsub = 94211,
        Ssa = 94212,
        MovText = 94213,
        HdmvPgsSubtitle = 94214,
        DvbTeletext = 94215,
        Srt = 94216,
        Microdvd = 96256,
        Eia608 = 96257,
        Jacosub = 96258,
        Sami = 96259,
        Realtext = 96260,
        Stl = 96261,
        Subviewer1 = 96262,
        Subviewer = 96263,
        Subrip = 96264,
        Webvtt = 96265,
        Mpl2 = 96266,
        Vplayer = 96267,
        Pjs = 96268,
        Ass = 96269,
        HdmvTextSubtitle = 96270,
        FirstUnknown = 98304,
        Ttf = 98304,
        Scte35 = 98305,
        Bintext = 100352,
        Xbin = 100353,
        Idf = 100354,
        Otf = 100355,
        SmpteKlv = 100356,
        DvdNav = 100357,
        TimedId3 = 100358,
        BinData = 100359,
        Probe = 102400,
        Mpeg2Ts = 131072,
        Mpeg4Systems = 131073,
        Ffmetadata = 135168,
        WrappedAvframe = 135169,
    }
}