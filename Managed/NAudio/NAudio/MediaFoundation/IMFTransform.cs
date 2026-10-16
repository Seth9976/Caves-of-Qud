using System;
using System.Runtime.InteropServices;

namespace NAudio.MediaFoundation
{
	// Token: 0x0200004C RID: 76
	[InterfaceType(1)]
	[Guid("bf94c121-5b05-4e6f-8000-ba598961414d")]
	[ComImport]
	public interface IMFTransform
	{
		// Token: 0x0600019E RID: 414
		void GetStreamLimits(out int pdwInputMinimum, out int pdwInputMaximum, out int pdwOutputMinimum, out int pdwOutputMaximum);

		// Token: 0x0600019F RID: 415
		void GetStreamCount(out int pcInputStreams, out int pcOutputStreams);

		// Token: 0x060001A0 RID: 416
		void GetStreamIds([In] int dwInputIDArraySize, [In] [Out] int[] pdwInputIDs, [In] int dwOutputIDArraySize, [In] [Out] int[] pdwOutputIDs);

		// Token: 0x060001A1 RID: 417
		void GetInputStreamInfo([In] int dwInputStreamID, out MFT_INPUT_STREAM_INFO pStreamInfo);

		// Token: 0x060001A2 RID: 418
		void GetOutputStreamInfo([In] int dwOutputStreamID, out MFT_OUTPUT_STREAM_INFO pStreamInfo);

		// Token: 0x060001A3 RID: 419
		void GetAttributes(out IMFAttributes pAttributes);

		// Token: 0x060001A4 RID: 420
		void GetInputStreamAttributes([In] int dwInputStreamID, out IMFAttributes pAttributes);

		// Token: 0x060001A5 RID: 421
		void GetOutputStreamAttributes([In] int dwOutputStreamID, out IMFAttributes pAttributes);

		// Token: 0x060001A6 RID: 422
		void DeleteInputStream([In] int dwOutputStreamID);

		// Token: 0x060001A7 RID: 423
		void AddInputStreams([In] int cStreams, [In] int[] adwStreamIDs);

		// Token: 0x060001A8 RID: 424
		void GetInputAvailableType([In] int dwInputStreamID, [In] int dwTypeIndex, out IMFMediaType ppType);

		// Token: 0x060001A9 RID: 425
		void GetOutputAvailableType([In] int dwOutputStreamID, [In] int dwTypeIndex, out IMFMediaType ppType);

		// Token: 0x060001AA RID: 426
		void SetInputType([In] int dwInputStreamID, [In] IMFMediaType pType, [In] _MFT_SET_TYPE_FLAGS dwFlags);

		// Token: 0x060001AB RID: 427
		void SetOutputType([In] int dwOutputStreamID, [In] IMFMediaType pType, [In] _MFT_SET_TYPE_FLAGS dwFlags);

		// Token: 0x060001AC RID: 428
		void GetInputCurrentType([In] int dwInputStreamID, out IMFMediaType ppType);

		// Token: 0x060001AD RID: 429
		void GetOutputCurrentType([In] int dwOutputStreamID, out IMFMediaType ppType);

		// Token: 0x060001AE RID: 430
		void GetInputStatus([In] int dwInputStreamID, out _MFT_INPUT_STATUS_FLAGS pdwFlags);

		// Token: 0x060001AF RID: 431
		void GetOutputStatus([In] int dwInputStreamID, out _MFT_OUTPUT_STATUS_FLAGS pdwFlags);

		// Token: 0x060001B0 RID: 432
		void SetOutputBounds([In] long hnsLowerBound, [In] long hnsUpperBound);

		// Token: 0x060001B1 RID: 433
		void ProcessEvent([In] int dwInputStreamID, [In] IMFMediaEvent pEvent);

		// Token: 0x060001B2 RID: 434
		void ProcessMessage([In] MFT_MESSAGE_TYPE eMessage, [In] IntPtr ulParam);

		// Token: 0x060001B3 RID: 435
		void ProcessInput([In] int dwInputStreamID, [In] IMFSample pSample, int dwFlags);

		// Token: 0x060001B4 RID: 436
		[PreserveSig]
		int ProcessOutput([In] _MFT_PROCESS_OUTPUT_FLAGS dwFlags, [In] int cOutputBufferCount, [MarshalAs(42, SizeParamIndex = 1)] [In] [Out] MFT_OUTPUT_DATA_BUFFER[] pOutputSamples, out _MFT_PROCESS_OUTPUT_STATUS pdwStatus);
	}
}
