using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x020002A3 RID: 675
	internal abstract class Heap
	{
		// Token: 0x06001117 RID: 4375 RVA: 0x000372C3 File Offset: 0x000354C3
		protected Heap(byte[] data)
		{
			this.data = data;
		}

		// Token: 0x04000684 RID: 1668
		public int IndexSize;

		// Token: 0x04000685 RID: 1669
		internal readonly byte[] data;
	}
}
