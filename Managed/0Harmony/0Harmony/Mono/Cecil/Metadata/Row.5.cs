using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x020002A9 RID: 681
	internal struct Row<T1, T2, T3, T4, T5, T6>
	{
		// Token: 0x0600111E RID: 4382 RVA: 0x00037353 File Offset: 0x00035553
		public Row(T1 col1, T2 col2, T3 col3, T4 col4, T5 col5, T6 col6)
		{
			this.Col1 = col1;
			this.Col2 = col2;
			this.Col3 = col3;
			this.Col4 = col4;
			this.Col5 = col5;
			this.Col6 = col6;
		}

		// Token: 0x04000698 RID: 1688
		internal T1 Col1;

		// Token: 0x04000699 RID: 1689
		internal T2 Col2;

		// Token: 0x0400069A RID: 1690
		internal T3 Col3;

		// Token: 0x0400069B RID: 1691
		internal T4 Col4;

		// Token: 0x0400069C RID: 1692
		internal T5 Col5;

		// Token: 0x0400069D RID: 1693
		internal T6 Col6;
	}
}
