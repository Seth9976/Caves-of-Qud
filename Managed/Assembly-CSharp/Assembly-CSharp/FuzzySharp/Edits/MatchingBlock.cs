using System;

namespace FuzzySharp.Edits
{
	// Token: 0x020001D3 RID: 467
	public class MatchingBlock
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x00065FA6 File Offset: 0x000641A6
		// (set) Token: 0x060010A0 RID: 4256 RVA: 0x00065FAE File Offset: 0x000641AE
		public int SourcePos { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x00065FB7 File Offset: 0x000641B7
		// (set) Token: 0x060010A2 RID: 4258 RVA: 0x00065FBF File Offset: 0x000641BF
		public int DestPos { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00065FC8 File Offset: 0x000641C8
		// (set) Token: 0x060010A4 RID: 4260 RVA: 0x00065FD0 File Offset: 0x000641D0
		public int Length { get; set; }

		// Token: 0x060010A5 RID: 4261 RVA: 0x00065FD9 File Offset: 0x000641D9
		public override string ToString()
		{
			return string.Format("({0},{1},{2})", this.SourcePos, this.DestPos, this.Length);
		}
	}
}
