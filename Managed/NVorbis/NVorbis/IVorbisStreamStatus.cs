using System;

namespace NVorbis
{
	// Token: 0x0200002B RID: 43
	public interface IVorbisStreamStatus
	{
		// Token: 0x06000153 RID: 339
		void ResetStats();

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000154 RID: 340
		int EffectiveBitRate { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000155 RID: 341
		int InstantBitRate { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000156 RID: 342
		TimeSpan PageLatency { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000157 RID: 343
		TimeSpan PacketLatency { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000158 RID: 344
		TimeSpan SecondLatency { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000159 RID: 345
		long OverheadBits { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600015A RID: 346
		long AudioBits { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015B RID: 347
		int PagesRead { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600015C RID: 348
		int TotalPages { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600015D RID: 349
		bool Clipped { get; }
	}
}
