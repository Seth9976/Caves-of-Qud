using System;

namespace NAudio.Mixer
{
	// Token: 0x020000FD RID: 253
	[Flags]
	public enum MixerFlags
	{
		// Token: 0x04000625 RID: 1573
		Handle = -2147483648,
		// Token: 0x04000626 RID: 1574
		Mixer = 0,
		// Token: 0x04000627 RID: 1575
		MixerHandle = -2147483648,
		// Token: 0x04000628 RID: 1576
		WaveOut = 268435456,
		// Token: 0x04000629 RID: 1577
		WaveOutHandle = -1879048192,
		// Token: 0x0400062A RID: 1578
		WaveIn = 536870912,
		// Token: 0x0400062B RID: 1579
		WaveInHandle = -1610612736,
		// Token: 0x0400062C RID: 1580
		MidiOut = 805306368,
		// Token: 0x0400062D RID: 1581
		MidiOutHandle = -1342177280,
		// Token: 0x0400062E RID: 1582
		MidiIn = 1073741824,
		// Token: 0x0400062F RID: 1583
		MidiInHandle = -1073741824,
		// Token: 0x04000630 RID: 1584
		Aux = 1342177280,
		// Token: 0x04000631 RID: 1585
		Value = 0,
		// Token: 0x04000632 RID: 1586
		ListText = 1,
		// Token: 0x04000633 RID: 1587
		QueryMask = 15,
		// Token: 0x04000634 RID: 1588
		All = 0,
		// Token: 0x04000635 RID: 1589
		OneById = 1,
		// Token: 0x04000636 RID: 1590
		OneByType = 2,
		// Token: 0x04000637 RID: 1591
		GetLineInfoOfDestination = 0,
		// Token: 0x04000638 RID: 1592
		GetLineInfoOfSource = 1,
		// Token: 0x04000639 RID: 1593
		GetLineInfoOfLineId = 2,
		// Token: 0x0400063A RID: 1594
		GetLineInfoOfComponentType = 3,
		// Token: 0x0400063B RID: 1595
		GetLineInfoOfTargetType = 4,
		// Token: 0x0400063C RID: 1596
		GetLineInfoOfQueryMask = 15
	}
}
