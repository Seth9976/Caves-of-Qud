using System;

namespace NAudio.Mixer
{
	// Token: 0x020000F9 RID: 249
	[Flags]
	internal enum MixerControlSubclass
	{
		// Token: 0x040005F2 RID: 1522
		SwitchBoolean = 0,
		// Token: 0x040005F3 RID: 1523
		SwitchButton = 16777216,
		// Token: 0x040005F4 RID: 1524
		MeterPolled = 0,
		// Token: 0x040005F5 RID: 1525
		TimeMicrosecs = 0,
		// Token: 0x040005F6 RID: 1526
		TimeMillisecs = 16777216,
		// Token: 0x040005F7 RID: 1527
		ListSingle = 0,
		// Token: 0x040005F8 RID: 1528
		ListMultiple = 16777216,
		// Token: 0x040005F9 RID: 1529
		Mask = 251658240
	}
}
