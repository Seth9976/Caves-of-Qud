using System;

namespace NAudio.Wave
{
	// Token: 0x020001AE RID: 430
	public interface IWavePosition
	{
		// Token: 0x06000908 RID: 2312
		long GetPosition();

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000909 RID: 2313
		WaveFormat OutputWaveFormat { get; }
	}
}
