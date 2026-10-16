using System;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000E8 RID: 232
	internal struct Row<T1, T2, T3, T4, T5>
	{
		// Token: 0x06000977 RID: 2423 RVA: 0x0001FB6B File Offset: 0x0001DD6B
		public Row(T1 col1, T2 col2, T3 col3, T4 col4, T5 col5)
		{
			this.Col1 = col1;
			this.Col2 = col2;
			this.Col3 = col3;
			this.Col4 = col4;
			this.Col5 = col5;
		}

		// Token: 0x040003C1 RID: 961
		internal T1 Col1;

		// Token: 0x040003C2 RID: 962
		internal T2 Col2;

		// Token: 0x040003C3 RID: 963
		internal T3 Col3;

		// Token: 0x040003C4 RID: 964
		internal T4 Col4;

		// Token: 0x040003C5 RID: 965
		internal T5 Col5;
	}
}
