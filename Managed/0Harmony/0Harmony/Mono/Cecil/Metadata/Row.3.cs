using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x020002A7 RID: 679
	internal struct Row<T1, T2, T3, T4>
	{
		// Token: 0x0600111C RID: 4380 RVA: 0x0003730D File Offset: 0x0003550D
		public Row(T1 col1, T2 col2, T3 col3, T4 col4)
		{
			this.Col1 = col1;
			this.Col2 = col2;
			this.Col3 = col3;
			this.Col4 = col4;
		}

		// Token: 0x0400068F RID: 1679
		internal T1 Col1;

		// Token: 0x04000690 RID: 1680
		internal T2 Col2;

		// Token: 0x04000691 RID: 1681
		internal T3 Col3;

		// Token: 0x04000692 RID: 1682
		internal T4 Col4;
	}
}
