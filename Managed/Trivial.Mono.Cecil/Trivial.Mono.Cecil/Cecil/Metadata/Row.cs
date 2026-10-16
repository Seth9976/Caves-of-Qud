using System;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000E5 RID: 229
	internal struct Row<T1, T2>
	{
		// Token: 0x06000974 RID: 2420 RVA: 0x0001FB25 File Offset: 0x0001DD25
		public Row(T1 col1, T2 col2)
		{
			this.Col1 = col1;
			this.Col2 = col2;
		}

		// Token: 0x040003B8 RID: 952
		internal T1 Col1;

		// Token: 0x040003B9 RID: 953
		internal T2 Col2;
	}
}
