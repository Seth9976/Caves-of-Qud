using System;

namespace NAudio.Wave
{
	// Token: 0x02000070 RID: 112
	public interface ISampleProvider
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000256 RID: 598
		WaveFormat WaveFormat { get; }

		// Token: 0x06000257 RID: 599
		int Read(float[] buffer, int offset, int count);
	}
}
