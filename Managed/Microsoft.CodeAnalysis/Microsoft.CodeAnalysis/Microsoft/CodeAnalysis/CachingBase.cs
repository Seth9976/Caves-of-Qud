using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000116 RID: 278
	internal abstract class CachingBase<[Nullable(2)] TEntry>
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x0001BE54 File Offset: 0x0001A054
		internal CachingBase(int size)
		{
			int num = CachingBase<TEntry>.AlignSize(size);
			this.mask = num - 1;
			this.entries = new TEntry[num];
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0001BE83 File Offset: 0x0001A083
		private static int AlignSize(int size)
		{
			size--;
			size |= size >> 1;
			size |= size >> 2;
			size |= size >> 4;
			size |= size >> 8;
			size |= size >> 16;
			return size + 1;
		}

		// Token: 0x040002C3 RID: 707
		protected readonly int mask;

		// Token: 0x040002C4 RID: 708
		[Nullable(1)]
		protected readonly TEntry[] entries;
	}
}
