using System;

namespace NAudio.Wave
{
	// Token: 0x02000079 RID: 121
	public interface IWaveProvider
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600028A RID: 650
		WaveFormat WaveFormat { get; }

		// Token: 0x0600028B RID: 651
		int Read(byte[] buffer, int offset, int count);
	}
}
