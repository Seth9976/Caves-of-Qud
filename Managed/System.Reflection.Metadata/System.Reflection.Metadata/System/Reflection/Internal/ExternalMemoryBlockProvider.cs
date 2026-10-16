using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000176 RID: 374
	internal sealed class ExternalMemoryBlockProvider : MemoryBlockProvider
	{
		// Token: 0x06000B84 RID: 2948 RVA: 0x00022D70 File Offset: 0x00020F70
		public unsafe ExternalMemoryBlockProvider(byte* memory, int size)
		{
			this._memory = memory;
			this._size = size;
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x00022D86 File Offset: 0x00020F86
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00022D8E File Offset: 0x00020F8E
		[NullableContext(1)]
		protected override AbstractMemoryBlock GetMemoryBlockImpl(int start, int size)
		{
			return new ExternalMemoryBlock(this, this._memory + start, size);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00022D9F File Offset: 0x00020F9F
		[NullableContext(1)]
		public override Stream GetStream(out StreamConstraints constraints)
		{
			constraints = new StreamConstraints(null, 0L, this._size);
			return new ReadOnlyUnmanagedMemoryStream(this._memory, this._size);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00022DC6 File Offset: 0x00020FC6
		protected override void Dispose(bool disposing)
		{
			this._memory = null;
			this._size = 0;
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x00022DD7 File Offset: 0x00020FD7
		public unsafe byte* Pointer
		{
			get
			{
				return this._memory;
			}
		}

		// Token: 0x0400094B RID: 2379
		private unsafe byte* _memory;

		// Token: 0x0400094C RID: 2380
		private int _size;
	}
}
