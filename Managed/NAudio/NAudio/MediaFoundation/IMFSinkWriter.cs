using System;
using System.Runtime.InteropServices;

namespace NAudio.MediaFoundation
{
	// Token: 0x0200004B RID: 75
	[Guid("3137f1cd-fe5e-4805-a5d8-fb477448cb3d")]
	[InterfaceType(1)]
	[ComImport]
	public interface IMFSinkWriter
	{
		// Token: 0x06000193 RID: 403
		void AddStream([MarshalAs(28)] [In] IMFMediaType pTargetMediaType, out int pdwStreamIndex);

		// Token: 0x06000194 RID: 404
		void SetInputMediaType([In] int dwStreamIndex, [MarshalAs(28)] [In] IMFMediaType pInputMediaType, [MarshalAs(28)] [In] IMFAttributes pEncodingParameters);

		// Token: 0x06000195 RID: 405
		void BeginWriting();

		// Token: 0x06000196 RID: 406
		void WriteSample([In] int dwStreamIndex, [MarshalAs(28)] [In] IMFSample pSample);

		// Token: 0x06000197 RID: 407
		void SendStreamTick([In] int dwStreamIndex, [In] long llTimestamp);

		// Token: 0x06000198 RID: 408
		void PlaceMarker([In] int dwStreamIndex, [In] IntPtr pvContext);

		// Token: 0x06000199 RID: 409
		void NotifyEndOfSegment([In] int dwStreamIndex);

		// Token: 0x0600019A RID: 410
		void Flush([In] int dwStreamIndex);

		// Token: 0x0600019B RID: 411
		void DoFinalize();

		// Token: 0x0600019C RID: 412
		void GetServiceForStream([In] int dwStreamIndex, [In] ref Guid guidService, [In] ref Guid riid, out IntPtr ppvObject);

		// Token: 0x0600019D RID: 413
		void GetStatistics([In] int dwStreamIndex, [In] [Out] MF_SINK_WRITER_STATISTICS pStats);
	}
}
