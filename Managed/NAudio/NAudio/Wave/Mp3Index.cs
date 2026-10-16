using System;

namespace NAudio.Wave
{
	// Token: 0x020001E6 RID: 486
	internal class Mp3Index
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0001FA51 File Offset: 0x0001DC51
		// (set) Token: 0x06000AB8 RID: 2744 RVA: 0x0001FA59 File Offset: 0x0001DC59
		public long FilePosition { get; set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0001FA62 File Offset: 0x0001DC62
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x0001FA6A File Offset: 0x0001DC6A
		public long SamplePosition { get; set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0001FA73 File Offset: 0x0001DC73
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x0001FA7B File Offset: 0x0001DC7B
		public int SampleCount { get; set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0001FA84 File Offset: 0x0001DC84
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x0001FA8C File Offset: 0x0001DC8C
		public int ByteCount { get; set; }
	}
}
