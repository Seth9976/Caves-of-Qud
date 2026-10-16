using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000177 RID: 375
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class MemoryBlockProvider : IDisposable
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x00022DDF File Offset: 0x00020FDF
		public AbstractMemoryBlock GetMemoryBlock()
		{
			return this.GetMemoryBlockImpl(0, this.Size);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00022DEE File Offset: 0x00020FEE
		public AbstractMemoryBlock GetMemoryBlock(int start, int size)
		{
			if ((ulong)start + (ulong)size > (ulong)((long)this.Size))
			{
				Throw.ImageTooSmallOrContainsInvalidOffsetOrCount();
			}
			return this.GetMemoryBlockImpl(start, size);
		}

		// Token: 0x06000B8C RID: 2956
		protected abstract AbstractMemoryBlock GetMemoryBlockImpl(int start, int size);

		// Token: 0x06000B8D RID: 2957
		public abstract Stream GetStream(out StreamConstraints constraints);

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000B8E RID: 2958
		public abstract int Size { get; }

		// Token: 0x06000B8F RID: 2959
		protected abstract void Dispose(bool disposing);

		// Token: 0x06000B90 RID: 2960 RVA: 0x00022E0B File Offset: 0x0002100B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
