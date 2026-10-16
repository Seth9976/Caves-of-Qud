using System;
using System.Collections.Immutable;
using System.Reflection.Metadata;

namespace System.Reflection.Internal
{
	// Token: 0x02000172 RID: 370
	internal abstract class AbstractMemoryBlock : IDisposable
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000B6E RID: 2926
		public unsafe abstract byte* Pointer { get; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000B6F RID: 2927
		public abstract int Size { get; }

		// Token: 0x06000B70 RID: 2928 RVA: 0x00022BD6 File Offset: 0x00020DD6
		public BlobReader GetReader()
		{
			return new BlobReader(this.Pointer, this.Size);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00022BEC File Offset: 0x00020DEC
		public virtual ImmutableArray<byte> GetContentUnchecked(int start, int length)
		{
			ImmutableArray<byte> immutableArray = BlobUtilities.ReadImmutableBytes(this.Pointer + start, length);
			GC.KeepAlive(this);
			return immutableArray;
		}

		// Token: 0x06000B72 RID: 2930
		public abstract void Dispose();
	}
}
