using System;

namespace NAudio.Mixer
{
	// Token: 0x020000FA RID: 250
	[Flags]
	internal enum MixerControlUnits
	{
		// Token: 0x040005FB RID: 1531
		Custom = 0,
		// Token: 0x040005FC RID: 1532
		Boolean = 65536,
		// Token: 0x040005FD RID: 1533
		Signed = 131072,
		// Token: 0x040005FE RID: 1534
		Unsigned = 196608,
		// Token: 0x040005FF RID: 1535
		Decibels = 262144,
		// Token: 0x04000600 RID: 1536
		Percent = 327680,
		// Token: 0x04000601 RID: 1537
		Mask = 16711680
	}
}
