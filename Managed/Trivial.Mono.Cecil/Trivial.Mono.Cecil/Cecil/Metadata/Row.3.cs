using System;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000E7 RID: 231
	internal struct Row<T1, T2, T3, T4>
	{
		// Token: 0x06000976 RID: 2422 RVA: 0x0001FB4C File Offset: 0x0001DD4C
		public Row(T1 col1, T2 col2, T3 col3, T4 col4)
		{
			this.Col1 = col1;
			this.Col2 = col2;
			this.Col3 = col3;
			this.Col4 = col4;
		}

		// Token: 0x040003BD RID: 957
		internal T1 Col1;

		// Token: 0x040003BE RID: 958
		internal T2 Col2;

		// Token: 0x040003BF RID: 959
		internal T3 Col3;

		// Token: 0x040003C0 RID: 960
		internal T4 Col4;
	}
}
