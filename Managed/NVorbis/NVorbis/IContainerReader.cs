using System;

namespace NVorbis
{
	// Token: 0x02000005 RID: 5
	public interface IContainerReader : IDisposable
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000022 RID: 34
		int[] StreamSerials { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35
		bool CanSeek { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36
		long WasteBits { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000025 RID: 37
		int PagesRead { get; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000026 RID: 38
		// (remove) Token: 0x06000027 RID: 39
		event EventHandler<NewStreamEventArgs> NewStream;

		// Token: 0x06000028 RID: 40
		bool Init();

		// Token: 0x06000029 RID: 41
		bool FindNextStream();

		// Token: 0x0600002A RID: 42
		int GetTotalPageCount();
	}
}
