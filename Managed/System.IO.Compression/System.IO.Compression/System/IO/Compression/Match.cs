using System;

namespace System.IO.Compression
{
	// Token: 0x02000026 RID: 38
	internal sealed class Match
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000600E File Offset: 0x0000420E
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00006016 File Offset: 0x00004216
		internal MatchState State { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000601F File Offset: 0x0000421F
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00006027 File Offset: 0x00004227
		internal int Position { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00006030 File Offset: 0x00004230
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00006038 File Offset: 0x00004238
		internal int Length { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006041 File Offset: 0x00004241
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00006049 File Offset: 0x00004249
		internal byte Symbol { get; set; }
	}
}
