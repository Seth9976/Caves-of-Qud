using System;

namespace NAudio.Mixer
{
	// Token: 0x020000FB RID: 251
	public enum MixerControlType
	{
		// Token: 0x04000603 RID: 1539
		Custom,
		// Token: 0x04000604 RID: 1540
		BooleanMeter = 268500992,
		// Token: 0x04000605 RID: 1541
		SignedMeter = 268566528,
		// Token: 0x04000606 RID: 1542
		PeakMeter,
		// Token: 0x04000607 RID: 1543
		UnsignedMeter = 268632064,
		// Token: 0x04000608 RID: 1544
		Boolean = 536936448,
		// Token: 0x04000609 RID: 1545
		OnOff,
		// Token: 0x0400060A RID: 1546
		Mute,
		// Token: 0x0400060B RID: 1547
		Mono,
		// Token: 0x0400060C RID: 1548
		Loudness,
		// Token: 0x0400060D RID: 1549
		StereoEnhance,
		// Token: 0x0400060E RID: 1550
		Button = 553713664,
		// Token: 0x0400060F RID: 1551
		Decibels = 805568512,
		// Token: 0x04000610 RID: 1552
		Signed = 805437440,
		// Token: 0x04000611 RID: 1553
		Unsigned = 805502976,
		// Token: 0x04000612 RID: 1554
		Percent = 805634048,
		// Token: 0x04000613 RID: 1555
		Slider = 1073872896,
		// Token: 0x04000614 RID: 1556
		Pan,
		// Token: 0x04000615 RID: 1557
		QSoundPan,
		// Token: 0x04000616 RID: 1558
		Fader = 1342373888,
		// Token: 0x04000617 RID: 1559
		Volume,
		// Token: 0x04000618 RID: 1560
		Bass,
		// Token: 0x04000619 RID: 1561
		Treble,
		// Token: 0x0400061A RID: 1562
		Equalizer,
		// Token: 0x0400061B RID: 1563
		SingleSelect = 1879113728,
		// Token: 0x0400061C RID: 1564
		Mux,
		// Token: 0x0400061D RID: 1565
		MultipleSelect = 1895890944,
		// Token: 0x0400061E RID: 1566
		Mixer,
		// Token: 0x0400061F RID: 1567
		MicroTime = 1610809344,
		// Token: 0x04000620 RID: 1568
		MilliTime = 1627586560
	}
}
