using System;

namespace NAudio.Mixer
{
	// Token: 0x020000F8 RID: 248
	[Flags]
	internal enum MixerControlClass
	{
		// Token: 0x040005E8 RID: 1512
		Custom = 0,
		// Token: 0x040005E9 RID: 1513
		Meter = 268435456,
		// Token: 0x040005EA RID: 1514
		Switch = 536870912,
		// Token: 0x040005EB RID: 1515
		Number = 805306368,
		// Token: 0x040005EC RID: 1516
		Slider = 1073741824,
		// Token: 0x040005ED RID: 1517
		Fader = 1342177280,
		// Token: 0x040005EE RID: 1518
		Time = 1610612736,
		// Token: 0x040005EF RID: 1519
		List = 1879048192,
		// Token: 0x040005F0 RID: 1520
		Mask = 1879048192
	}
}
