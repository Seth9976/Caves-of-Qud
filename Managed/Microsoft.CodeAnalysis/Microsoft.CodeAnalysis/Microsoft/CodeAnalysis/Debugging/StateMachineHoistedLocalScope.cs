using System;

namespace Microsoft.CodeAnalysis.Debugging
{
	// Token: 0x02000362 RID: 866
	internal struct StateMachineHoistedLocalScope
	{
		// Token: 0x060024F5 RID: 9461 RVA: 0x0007326E File Offset: 0x0007146E
		public StateMachineHoistedLocalScope(int startOffset, int endOffset)
		{
			this.StartOffset = startOffset;
			this.EndOffset = endOffset;
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x0007327E File Offset: 0x0007147E
		public int Length
		{
			get
			{
				return this.EndOffset - this.StartOffset;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x0007328D File Offset: 0x0007148D
		public bool IsDefault
		{
			get
			{
				return this.StartOffset == 0 && this.EndOffset == 0;
			}
		}

		// Token: 0x04000FF0 RID: 4080
		public readonly int StartOffset;

		// Token: 0x04000FF1 RID: 4081
		public readonly int EndOffset;
	}
}
