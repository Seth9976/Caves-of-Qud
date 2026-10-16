using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x020002AA RID: 682
	internal struct Row<T1, T2, T3, T4, T5, T6, T7, T8, T9>
	{
		// Token: 0x0600111F RID: 4383 RVA: 0x00037384 File Offset: 0x00035584
		public Row(T1 col1, T2 col2, T3 col3, T4 col4, T5 col5, T6 col6, T7 col7, T8 col8, T9 col9)
		{
			this.Col1 = col1;
			this.Col2 = col2;
			this.Col3 = col3;
			this.Col4 = col4;
			this.Col5 = col5;
			this.Col6 = col6;
			this.Col7 = col7;
			this.Col8 = col8;
			this.Col9 = col9;
		}

		// Token: 0x0400069E RID: 1694
		internal T1 Col1;

		// Token: 0x0400069F RID: 1695
		internal T2 Col2;

		// Token: 0x040006A0 RID: 1696
		internal T3 Col3;

		// Token: 0x040006A1 RID: 1697
		internal T4 Col4;

		// Token: 0x040006A2 RID: 1698
		internal T5 Col5;

		// Token: 0x040006A3 RID: 1699
		internal T6 Col6;

		// Token: 0x040006A4 RID: 1700
		internal T7 Col7;

		// Token: 0x040006A5 RID: 1701
		internal T8 Col8;

		// Token: 0x040006A6 RID: 1702
		internal T9 Col9;
	}
}
