using System;

namespace NAudio.Wave.Asio
{
	// Token: 0x0200014D RID: 333
	internal enum ASIOMessageSelector
	{
		// Token: 0x0400075C RID: 1884
		kAsioSelectorSupported = 1,
		// Token: 0x0400075D RID: 1885
		kAsioEngineVersion,
		// Token: 0x0400075E RID: 1886
		kAsioResetRequest,
		// Token: 0x0400075F RID: 1887
		kAsioBufferSizeChange,
		// Token: 0x04000760 RID: 1888
		kAsioResyncRequest,
		// Token: 0x04000761 RID: 1889
		kAsioLatenciesChanged,
		// Token: 0x04000762 RID: 1890
		kAsioSupportsTimeInfo,
		// Token: 0x04000763 RID: 1891
		kAsioSupportsTimeCode,
		// Token: 0x04000764 RID: 1892
		kAsioMMCCommand,
		// Token: 0x04000765 RID: 1893
		kAsioSupportsInputMonitor,
		// Token: 0x04000766 RID: 1894
		kAsioSupportsInputGain,
		// Token: 0x04000767 RID: 1895
		kAsioSupportsInputMeter,
		// Token: 0x04000768 RID: 1896
		kAsioSupportsOutputGain,
		// Token: 0x04000769 RID: 1897
		kAsioSupportsOutputMeter,
		// Token: 0x0400076A RID: 1898
		kAsioOverload
	}
}
