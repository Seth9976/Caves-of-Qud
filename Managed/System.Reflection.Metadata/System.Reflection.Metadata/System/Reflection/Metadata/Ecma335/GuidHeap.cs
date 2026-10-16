using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E4 RID: 228
	internal readonly struct GuidHeap
	{
		// Token: 0x0600089C RID: 2204 RVA: 0x00017C91 File Offset: 0x00015E91
		public GuidHeap(MemoryBlock block)
		{
			this.Block = block;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00017C9C File Offset: 0x00015E9C
		internal Guid GetGuid(GuidHandle handle)
		{
			if (handle.IsNil)
			{
				return default(Guid);
			}
			return this.Block.PeekGuid((handle.Index - 1) * 16);
		}

		// Token: 0x0400053F RID: 1343
		internal readonly MemoryBlock Block;
	}
}
