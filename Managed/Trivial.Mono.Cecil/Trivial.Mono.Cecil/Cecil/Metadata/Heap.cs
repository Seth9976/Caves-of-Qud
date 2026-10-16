using System;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000E0 RID: 224
	internal abstract class Heap
	{
		// Token: 0x0600096A RID: 2410 RVA: 0x0001FAB9 File Offset: 0x0001DCB9
		protected Heap(byte[] data)
		{
			this.data = data;
		}

		// Token: 0x040003B2 RID: 946
		public int IndexSize;

		// Token: 0x040003B3 RID: 947
		internal readonly byte[] data;
	}
}
