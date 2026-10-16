using System;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000E6 RID: 230
	internal struct Row<T1, T2, T3>
	{
		// Token: 0x06000975 RID: 2421 RVA: 0x0001FB35 File Offset: 0x0001DD35
		public Row(T1 col1, T2 col2, T3 col3)
		{
			this.Col1 = col1;
			this.Col2 = col2;
			this.Col3 = col3;
		}

		// Token: 0x040003BA RID: 954
		internal T1 Col1;

		// Token: 0x040003BB RID: 955
		internal T2 Col2;

		// Token: 0x040003BC RID: 956
		internal T3 Col3;
	}
}
