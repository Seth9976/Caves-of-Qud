using System;
using System.Collections.Immutable;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Reflection.Internal
{
	// Token: 0x02000174 RID: 372
	internal sealed class ByteArrayMemoryProvider : MemoryBlockProvider
	{
		// Token: 0x06000B79 RID: 2937 RVA: 0x00022C74 File Offset: 0x00020E74
		public ByteArrayMemoryProvider(ImmutableArray<byte> array)
		{
			this._array = array;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00022C83 File Offset: 0x00020E83
		protected override void Dispose(bool disposing)
		{
			PinnedObject pinnedObject = Interlocked.Exchange<PinnedObject>(ref this._pinned, null);
			if (pinnedObject == null)
			{
				return;
			}
			pinnedObject.Dispose();
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00022C9C File Offset: 0x00020E9C
		public override int Size
		{
			get
			{
				return this._array.Length;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00022CB7 File Offset: 0x00020EB7
		public ImmutableArray<byte> Array
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00022CBF File Offset: 0x00020EBF
		[NullableContext(1)]
		protected override AbstractMemoryBlock GetMemoryBlockImpl(int start, int size)
		{
			return new ByteArrayMemoryBlock(this, start, size);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00022CC9 File Offset: 0x00020EC9
		[NullableContext(1)]
		public override Stream GetStream(out StreamConstraints constraints)
		{
			constraints = new StreamConstraints(null, 0L, this.Size);
			return new ImmutableMemoryStream(this._array);
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x00022CEC File Offset: 0x00020EEC
		internal unsafe byte* Pointer
		{
			get
			{
				if (this._pinned == null)
				{
					PinnedObject pinnedObject = new PinnedObject(ImmutableByteArrayInterop.DangerousGetUnderlyingArray(this._array));
					if (Interlocked.CompareExchange<PinnedObject>(ref this._pinned, pinnedObject, null) != null)
					{
						pinnedObject.Dispose();
					}
				}
				return this._pinned.Pointer;
			}
		}

		// Token: 0x04000946 RID: 2374
		private readonly ImmutableArray<byte> _array;

		// Token: 0x04000947 RID: 2375
		private PinnedObject _pinned;
	}
}
