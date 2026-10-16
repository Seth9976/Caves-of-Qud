using System;

namespace NAudio.Wave
{
	// Token: 0x020001AD RID: 429
	public interface IWaveBuffer
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060008FF RID: 2303
		byte[] ByteBuffer { get; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000900 RID: 2304
		float[] FloatBuffer { get; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000901 RID: 2305
		short[] ShortBuffer { get; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000902 RID: 2306
		int[] IntBuffer { get; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000903 RID: 2307
		int MaxSize { get; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000904 RID: 2308
		int ByteBufferCount { get; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000905 RID: 2309
		int FloatBufferCount { get; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000906 RID: 2310
		int ShortBufferCount { get; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000907 RID: 2311
		int IntBufferCount { get; }
	}
}
