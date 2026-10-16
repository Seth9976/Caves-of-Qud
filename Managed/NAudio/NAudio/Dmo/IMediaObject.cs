using System;
using System.Runtime.InteropServices;
using System.Security;

namespace NAudio.Dmo
{
	// Token: 0x02000200 RID: 512
	[SuppressUnmanagedCodeSecurity]
	[Guid("d8ad0f58-5494-4102-97c5-ec798e59bcf4")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IMediaObject
	{
		// Token: 0x06000B8F RID: 2959
		[PreserveSig]
		int GetStreamCount(out int inputStreams, out int outputStreams);

		// Token: 0x06000B90 RID: 2960
		[PreserveSig]
		int GetInputStreamInfo(int inputStreamIndex, out InputStreamInfoFlags flags);

		// Token: 0x06000B91 RID: 2961
		[PreserveSig]
		int GetOutputStreamInfo(int outputStreamIndex, out OutputStreamInfoFlags flags);

		// Token: 0x06000B92 RID: 2962
		[PreserveSig]
		int GetInputType(int inputStreamIndex, int typeIndex, out DmoMediaType mediaType);

		// Token: 0x06000B93 RID: 2963
		[PreserveSig]
		int GetOutputType(int outputStreamIndex, int typeIndex, out DmoMediaType mediaType);

		// Token: 0x06000B94 RID: 2964
		[PreserveSig]
		int SetInputType(int inputStreamIndex, [In] ref DmoMediaType mediaType, DmoSetTypeFlags flags);

		// Token: 0x06000B95 RID: 2965
		[PreserveSig]
		int SetOutputType(int outputStreamIndex, [In] ref DmoMediaType mediaType, DmoSetTypeFlags flags);

		// Token: 0x06000B96 RID: 2966
		[PreserveSig]
		int GetInputCurrentType(int inputStreamIndex, out DmoMediaType mediaType);

		// Token: 0x06000B97 RID: 2967
		[PreserveSig]
		int GetOutputCurrentType(int outputStreamIndex, out DmoMediaType mediaType);

		// Token: 0x06000B98 RID: 2968
		[PreserveSig]
		int GetInputSizeInfo(int inputStreamIndex, out int size, out int maxLookahead, out int alignment);

		// Token: 0x06000B99 RID: 2969
		[PreserveSig]
		int GetOutputSizeInfo(int outputStreamIndex, out int size, out int alignment);

		// Token: 0x06000B9A RID: 2970
		[PreserveSig]
		int GetInputMaxLatency(int inputStreamIndex, out long referenceTimeMaxLatency);

		// Token: 0x06000B9B RID: 2971
		[PreserveSig]
		int SetInputMaxLatency(int inputStreamIndex, long referenceTimeMaxLatency);

		// Token: 0x06000B9C RID: 2972
		[PreserveSig]
		int Flush();

		// Token: 0x06000B9D RID: 2973
		[PreserveSig]
		int Discontinuity(int inputStreamIndex);

		// Token: 0x06000B9E RID: 2974
		[PreserveSig]
		int AllocateStreamingResources();

		// Token: 0x06000B9F RID: 2975
		[PreserveSig]
		int FreeStreamingResources();

		// Token: 0x06000BA0 RID: 2976
		[PreserveSig]
		int GetInputStatus(int inputStreamIndex, out DmoInputStatusFlags flags);

		// Token: 0x06000BA1 RID: 2977
		[PreserveSig]
		int ProcessInput(int inputStreamIndex, [In] IMediaBuffer mediaBuffer, DmoInputDataBufferFlags flags, long referenceTimeTimestamp, long referenceTimeDuration);

		// Token: 0x06000BA2 RID: 2978
		[PreserveSig]
		int ProcessOutput(DmoProcessOutputFlags flags, int outputBufferCount, [MarshalAs(42, SizeParamIndex = 1)] [In] [Out] DmoOutputDataBuffer[] outputBuffers, out int statusReserved);

		// Token: 0x06000BA3 RID: 2979
		[PreserveSig]
		int Lock(bool acquireLock);
	}
}
