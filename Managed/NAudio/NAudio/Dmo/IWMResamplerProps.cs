using System;
using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	// Token: 0x0200008E RID: 142
	[Guid("E7E9984F-F09F-4da4-903F-6E2E0EFE56B5")]
	[InterfaceType(1)]
	internal interface IWMResamplerProps
	{
		// Token: 0x06000307 RID: 775
		int SetHalfFilterLength(int outputQuality);

		// Token: 0x06000308 RID: 776
		int SetUserChannelMtx([In] float[] channelConversionMatrix);
	}
}
