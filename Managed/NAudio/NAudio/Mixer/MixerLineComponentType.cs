using System;

namespace NAudio.Mixer
{
	// Token: 0x0200010B RID: 267
	public enum MixerLineComponentType
	{
		// Token: 0x04000687 RID: 1671
		DestinationUndefined,
		// Token: 0x04000688 RID: 1672
		DestinationDigital,
		// Token: 0x04000689 RID: 1673
		DestinationLine,
		// Token: 0x0400068A RID: 1674
		DestinationMonitor,
		// Token: 0x0400068B RID: 1675
		DestinationSpeakers,
		// Token: 0x0400068C RID: 1676
		DestinationHeadphones,
		// Token: 0x0400068D RID: 1677
		DestinationTelephone,
		// Token: 0x0400068E RID: 1678
		DestinationWaveIn,
		// Token: 0x0400068F RID: 1679
		DestinationVoiceIn,
		// Token: 0x04000690 RID: 1680
		SourceUndefined = 4096,
		// Token: 0x04000691 RID: 1681
		SourceDigital,
		// Token: 0x04000692 RID: 1682
		SourceLine,
		// Token: 0x04000693 RID: 1683
		SourceMicrophone,
		// Token: 0x04000694 RID: 1684
		SourceSynthesizer,
		// Token: 0x04000695 RID: 1685
		SourceCompactDisc,
		// Token: 0x04000696 RID: 1686
		SourceTelephone,
		// Token: 0x04000697 RID: 1687
		SourcePcSpeaker,
		// Token: 0x04000698 RID: 1688
		SourceWaveOut,
		// Token: 0x04000699 RID: 1689
		SourceAuxiliary,
		// Token: 0x0400069A RID: 1690
		SourceAnalog
	}
}
