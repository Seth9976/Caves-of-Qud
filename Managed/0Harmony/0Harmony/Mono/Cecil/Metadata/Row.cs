using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x020002A5 RID: 677
	internal struct Row<T1, T2>
	{
		// Token: 0x0600111A RID: 4378 RVA: 0x000372E6 File Offset: 0x000354E6
		public Row(T1 col1, T2 col2)
		{
			this.Col1 = col1;
			this.Col2 = col2;
		}

		// Token: 0x0400068A RID: 1674
		internal T1 Col1;

		// Token: 0x0400068B RID: 1675
		internal T2 Col2;
	}
}
