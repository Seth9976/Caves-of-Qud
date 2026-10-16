using System;
using System.Runtime.InteropServices;
using NAudio.Wave;

namespace NAudio.MediaFoundation
{
	// Token: 0x02000050 RID: 80
	public static class MediaFoundationInterop
	{
		// Token: 0x060001C0 RID: 448
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFStartup(int version, int dwFlags = 0);

		// Token: 0x060001C1 RID: 449
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFShutdown();

		// Token: 0x060001C2 RID: 450
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		internal static extern void MFCreateMediaType(out IMFMediaType ppMFType);

		// Token: 0x060001C3 RID: 451
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		internal static extern void MFInitMediaTypeFromWaveFormatEx([In] IMFMediaType pMFType, [In] WaveFormat pWaveFormat, [In] int cbBufSize);

		// Token: 0x060001C4 RID: 452
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		internal static extern void MFCreateWaveFormatExFromMFMediaType(IMFMediaType pMFType, ref IntPtr ppWF, ref int pcbSize, int flags = 0);

		// Token: 0x060001C5 RID: 453
		[DllImport("mfreadwrite.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFCreateSourceReaderFromURL([MarshalAs(21)] [In] string pwszURL, [In] IMFAttributes pAttributes, [MarshalAs(28)] out IMFSourceReader ppSourceReader);

		// Token: 0x060001C6 RID: 454
		[DllImport("mfreadwrite.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFCreateSourceReaderFromByteStream([In] IMFByteStream pByteStream, [In] IMFAttributes pAttributes, [MarshalAs(28)] out IMFSourceReader ppSourceReader);

		// Token: 0x060001C7 RID: 455
		[DllImport("mfreadwrite.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFCreateSinkWriterFromURL([MarshalAs(21)] [In] string pwszOutputURL, [In] IMFByteStream pByteStream, [In] IMFAttributes pAttributes, out IMFSinkWriter ppSinkWriter);

		// Token: 0x060001C8 RID: 456
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFCreateMFByteStreamOnStreamEx([MarshalAs(25)] object punkStream, out IMFByteStream ppByteStream);

		// Token: 0x060001C9 RID: 457
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFTEnumEx([In] Guid guidCategory, [In] _MFT_ENUM_FLAG flags, [In] MFT_REGISTER_TYPE_INFO pInputType, [In] MFT_REGISTER_TYPE_INFO pOutputType, out IntPtr pppMFTActivate, out int pcMFTActivate);

		// Token: 0x060001CA RID: 458
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		internal static extern void MFCreateSample(out IMFSample ppIMFSample);

		// Token: 0x060001CB RID: 459
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		internal static extern void MFCreateMemoryBuffer(int cbMaxLength, out IMFMediaBuffer ppBuffer);

		// Token: 0x060001CC RID: 460
		[DllImport("mfplat.dll", ExactSpelling = true, PreserveSig = false)]
		internal static extern void MFCreateAttributes([MarshalAs(28)] out IMFAttributes ppMFAttributes, [In] int cInitialSize);

		// Token: 0x060001CD RID: 461
		[DllImport("mf.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern void MFTranscodeGetAudioOutputAvailableTypes([MarshalAs(43)] [In] Guid guidSubType, [In] _MFT_ENUM_FLAG dwMFTFlags, [In] IMFAttributes pCodecConfig, [MarshalAs(28)] out IMFCollection ppAvailableTypes);

		// Token: 0x040002D1 RID: 721
		public const int MF_SOURCE_READER_ALL_STREAMS = -2;

		// Token: 0x040002D2 RID: 722
		public const int MF_SOURCE_READER_FIRST_AUDIO_STREAM = -3;

		// Token: 0x040002D3 RID: 723
		public const int MF_SOURCE_READER_FIRST_VIDEO_STREAM = -4;

		// Token: 0x040002D4 RID: 724
		public const int MF_SOURCE_READER_MEDIASOURCE = -1;

		// Token: 0x040002D5 RID: 725
		public const int MF_SDK_VERSION = 2;

		// Token: 0x040002D6 RID: 726
		public const int MF_API_VERSION = 112;

		// Token: 0x040002D7 RID: 727
		public const int MF_VERSION = 131184;
	}
}
