using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x020002A6 RID: 678
	internal struct Row<T1, T2, T3>
	{
		// Token: 0x0600111B RID: 4379 RVA: 0x000372F6 File Offset: 0x000354F6
		public Row(T1 col1, T2 col2, T3 col3)
		{
			this.Col1 = col1;
			this.Col2 = col2;
			this.Col3 = col3;
		}

		// Token: 0x0400068C RID: 1676
		internal T1 Col1;

		// Token: 0x0400068D RID: 1677
		internal T2 Col2;

		// Token: 0x0400068E RID: 1678
		internal T3 Col3;
	}
}
