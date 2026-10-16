using System;

namespace NAudio.CoreAudioApi
{
	// Token: 0x02000117 RID: 279
	[Flags]
	public enum DeviceState
	{
		// Token: 0x040006D0 RID: 1744
		Active = 1,
		// Token: 0x040006D1 RID: 1745
		Disabled = 2,
		// Token: 0x040006D2 RID: 1746
		NotPresent = 4,
		// Token: 0x040006D3 RID: 1747
		Unplugged = 8,
		// Token: 0x040006D4 RID: 1748
		All = 15
	}
}
