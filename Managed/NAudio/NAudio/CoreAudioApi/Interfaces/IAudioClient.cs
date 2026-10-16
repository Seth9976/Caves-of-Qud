using System;
using System.Runtime.InteropServices;
using NAudio.Wave;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x0200011C RID: 284
	[InterfaceType(1)]
	[Guid("1CB9AD4C-DBFA-4c32-B178-C2F568A703B2")]
	internal interface IAudioClient
	{
		// Token: 0x0600060E RID: 1550
		[PreserveSig]
		int Initialize(AudioClientShareMode shareMode, AudioClientStreamFlags StreamFlags, long hnsBufferDuration, long hnsPeriodicity, [In] WaveFormat pFormat, [In] ref Guid AudioSessionGuid);

		// Token: 0x0600060F RID: 1551
		int GetBufferSize(out uint bufferSize);

		// Token: 0x06000610 RID: 1552
		[return: MarshalAs(9)]
		long GetStreamLatency();

		// Token: 0x06000611 RID: 1553
		int GetCurrentPadding(out int currentPadding);

		// Token: 0x06000612 RID: 1554
		[PreserveSig]
		int IsFormatSupported(AudioClientShareMode shareMode, [In] WaveFormat pFormat, [MarshalAs(43)] out WaveFormatExtensible closestMatchFormat);

		// Token: 0x06000613 RID: 1555
		int GetMixFormat(out IntPtr deviceFormatPointer);

		// Token: 0x06000614 RID: 1556
		int GetDevicePeriod(out long defaultDevicePeriod, out long minimumDevicePeriod);

		// Token: 0x06000615 RID: 1557
		int Start();

		// Token: 0x06000616 RID: 1558
		int Stop();

		// Token: 0x06000617 RID: 1559
		int Reset();

		// Token: 0x06000618 RID: 1560
		int SetEventHandle(IntPtr eventHandle);

		// Token: 0x06000619 RID: 1561
		[PreserveSig]
		int GetService([MarshalAs(43)] [In] Guid interfaceId, [MarshalAs(25)] out object interfacePointer);
	}
}
