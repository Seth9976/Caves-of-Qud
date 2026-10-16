using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.MediaFoundation
{
	// Token: 0x02000055 RID: 85
	[Guid("70ae66f2-c809-4e4f-8915-bdcb406b7993")]
	[InterfaceType(1)]
	[ComImport]
	public interface IMFSourceReader
	{
		// Token: 0x06000231 RID: 561
		void GetStreamSelection([In] int dwStreamIndex, [MarshalAs(2)] out bool pSelected);

		// Token: 0x06000232 RID: 562
		void SetStreamSelection([In] int dwStreamIndex, [MarshalAs(2)] [In] bool pSelected);

		// Token: 0x06000233 RID: 563
		void GetNativeMediaType([In] int dwStreamIndex, [In] int dwMediaTypeIndex, out IMFMediaType ppMediaType);

		// Token: 0x06000234 RID: 564
		void GetCurrentMediaType([In] int dwStreamIndex, out IMFMediaType ppMediaType);

		// Token: 0x06000235 RID: 565
		void SetCurrentMediaType([In] int dwStreamIndex, IntPtr pdwReserved, [In] IMFMediaType pMediaType);

		// Token: 0x06000236 RID: 566
		void SetCurrentPosition([MarshalAs(43)] [In] Guid guidTimeFormat, [In] ref PropVariant varPosition);

		// Token: 0x06000237 RID: 567
		void ReadSample([In] int dwStreamIndex, [In] int dwControlFlags, out int pdwActualStreamIndex, out MF_SOURCE_READER_FLAG pdwStreamFlags, out ulong pllTimestamp, out IMFSample ppSample);

		// Token: 0x06000238 RID: 568
		void Flush([In] int dwStreamIndex);

		// Token: 0x06000239 RID: 569
		void GetServiceForStream([In] int dwStreamIndex, [MarshalAs(43)] [In] Guid guidService, [MarshalAs(43)] [In] Guid riid, out IntPtr ppvObject);

		// Token: 0x0600023A RID: 570
		[PreserveSig]
		int GetPresentationAttribute([In] int dwStreamIndex, [MarshalAs(43)] [In] Guid guidAttribute, out PropVariant pvarAttribute);
	}
}
