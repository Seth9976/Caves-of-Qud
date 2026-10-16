using System;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000E9 RID: 233
	internal struct Row<T1, T2, T3, T4, T5, T6>
	{
		// Token: 0x06000978 RID: 2424 RVA: 0x0001FB92 File Offset: 0x0001DD92
		public Row(T1 col1, T2 col2, T3 col3, T4 col4, T5 col5, T6 col6)
		{
			this.Col1 = col1;
			this.Col2 = col2;
			this.Col3 = col3;
			this.Col4 = col4;
			this.Col5 = col5;
			this.Col6 = col6;
		}

		// Token: 0x040003C6 RID: 966
		internal T1 Col1;

		// Token: 0x040003C7 RID: 967
		internal T2 Col2;

		// Token: 0x040003C8 RID: 968
		internal T3 Col3;

		// Token: 0x040003C9 RID: 969
		internal T4 Col4;

		// Token: 0x040003CA RID: 970
		internal T5 Col5;

		// Token: 0x040003CB RID: 971
		internal T6 Col6;
	}
}
