using System;

namespace System.Buffers
{
	// Token: 0x02000AEC RID: 2796
	public abstract class ReadOnlySequenceSegment<T>
	{
		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06006369 RID: 25449 RVA: 0x0014CA68 File Offset: 0x0014AC68
		// (set) Token: 0x0600636A RID: 25450 RVA: 0x0014CA70 File Offset: 0x0014AC70
		public ReadOnlyMemory<T> Memory { get; protected set; }

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x0600636B RID: 25451 RVA: 0x0014CA79 File Offset: 0x0014AC79
		// (set) Token: 0x0600636C RID: 25452 RVA: 0x0014CA81 File Offset: 0x0014AC81
		public ReadOnlySequenceSegment<T> Next { get; protected set; }

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x0600636D RID: 25453 RVA: 0x0014CA8A File Offset: 0x0014AC8A
		// (set) Token: 0x0600636E RID: 25454 RVA: 0x0014CA92 File Offset: 0x0014AC92
		public long RunningIndex { get; protected set; }
	}
}
