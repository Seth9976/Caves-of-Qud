using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000175 RID: 373
	internal sealed class ExternalMemoryBlock : AbstractMemoryBlock
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x00022D32 File Offset: 0x00020F32
		public unsafe ExternalMemoryBlock([Nullable(1)] object memoryOwner, byte* buffer, int size)
		{
			this._memoryOwner = memoryOwner;
			this._buffer = buffer;
			this._size = size;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00022D4F File Offset: 0x00020F4F
		public override void Dispose()
		{
			this._buffer = null;
			this._size = 0;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x00022D60 File Offset: 0x00020F60
		public unsafe override byte* Pointer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x00022D68 File Offset: 0x00020F68
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x04000948 RID: 2376
		private readonly object _memoryOwner;

		// Token: 0x04000949 RID: 2377
		private unsafe byte* _buffer;

		// Token: 0x0400094A RID: 2378
		private int _size;
	}
}
