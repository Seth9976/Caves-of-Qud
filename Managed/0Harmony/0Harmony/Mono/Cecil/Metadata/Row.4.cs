using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x020002A8 RID: 680
	internal struct Row<T1, T2, T3, T4, T5>
	{
		// Token: 0x0600111D RID: 4381 RVA: 0x0003732C File Offset: 0x0003552C
		public Row(T1 col1, T2 col2, T3 col3, T4 col4, T5 col5)
		{
			this.Col1 = col1;
			this.Col2 = col2;
			this.Col3 = col3;
			this.Col4 = col4;
			this.Col5 = col5;
		}

		// Token: 0x04000693 RID: 1683
		internal T1 Col1;

		// Token: 0x04000694 RID: 1684
		internal T2 Col2;

		// Token: 0x04000695 RID: 1685
		internal T3 Col3;

		// Token: 0x04000696 RID: 1686
		internal T4 Col4;

		// Token: 0x04000697 RID: 1687
		internal T5 Col5;
	}
}
