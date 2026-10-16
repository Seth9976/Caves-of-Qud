using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x0200011E RID: 286
	[Guid("C02216F6-8C67-4B5B-9D00-D008E73E0064")]
	[InterfaceType(1)]
	internal interface IAudioMeterInformation
	{
		// Token: 0x0600062C RID: 1580
		int GetPeakValue(out float pfPeak);

		// Token: 0x0600062D RID: 1581
		int GetMeteringChannelCount(out int pnChannelCount);

		// Token: 0x0600062E RID: 1582
		int GetChannelsPeakValues(int u32ChannelCount, [In] IntPtr afPeakValues);

		// Token: 0x0600062F RID: 1583
		int QueryHardwareSupport(out int pdwHardwareSupportMask);
	}
}
