using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000173 RID: 371
	internal sealed class ByteArrayMemoryBlock : AbstractMemoryBlock
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x00022C17 File Offset: 0x00020E17
		[NullableContext(1)]
		internal ByteArrayMemoryBlock(ByteArrayMemoryProvider provider, int start, int size)
		{
			this._provider = provider;
			this._size = size;
			this._start = start;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00022C34 File Offset: 0x00020E34
		public override void Dispose()
		{
			this._provider = null;
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00022C3D File Offset: 0x00020E3D
		public unsafe override byte* Pointer
		{
			get
			{
				return this._provider.Pointer + this._start;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x00022C51 File Offset: 0x00020E51
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00022C59 File Offset: 0x00020E59
		public override ImmutableArray<byte> GetContentUnchecked(int start, int length)
		{
			return ImmutableArray.Create<byte>(this._provider.Array, this._start + start, length);
		}

		// Token: 0x04000943 RID: 2371
		private ByteArrayMemoryProvider _provider;

		// Token: 0x04000944 RID: 2372
		private readonly int _start;

		// Token: 0x04000945 RID: 2373
		private readonly int _size;
	}
}
