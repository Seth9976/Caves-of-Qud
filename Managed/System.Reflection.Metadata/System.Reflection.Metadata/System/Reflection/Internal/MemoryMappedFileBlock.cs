using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Reflection.Internal
{
	// Token: 0x02000178 RID: 376
	internal sealed class MemoryMappedFileBlock : AbstractMemoryBlock
	{
		// Token: 0x06000B92 RID: 2962 RVA: 0x00022E22 File Offset: 0x00021022
		[NullableContext(1)]
		internal MemoryMappedFileBlock(IDisposable accessor, SafeBuffer safeBuffer, long offset, int size)
		{
			this._data = new MemoryMappedFileBlock.DisposableData(accessor, safeBuffer, offset);
			this._size = size;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00022E40 File Offset: 0x00021040
		public override void Dispose()
		{
			this._data.Dispose();
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00022E4D File Offset: 0x0002104D
		public unsafe override byte* Pointer
		{
			get
			{
				return this._data.Pointer;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x00022E5A File Offset: 0x0002105A
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x0400094D RID: 2381
		private readonly MemoryMappedFileBlock.DisposableData _data;

		// Token: 0x0400094E RID: 2382
		private readonly int _size;

		// Token: 0x02000204 RID: 516
		private sealed class DisposableData : CriticalDisposableObject
		{
			// Token: 0x06000D52 RID: 3410 RVA: 0x00026F70 File Offset: 0x00025170
			public unsafe DisposableData(IDisposable accessor, SafeBuffer safeBuffer, long offset)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					byte* ptr = null;
					safeBuffer.AcquirePointer(ref ptr);
					this._accessor = accessor;
					this._safeBuffer = safeBuffer;
					this._pointer = ptr + offset;
				}
			}

			// Token: 0x06000D53 RID: 3411 RVA: 0x00026FC0 File Offset: 0x000251C0
			protected override void Release()
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					SafeBuffer safeBuffer = Interlocked.Exchange<SafeBuffer>(ref this._safeBuffer, null);
					if (safeBuffer != null)
					{
						safeBuffer.ReleasePointer();
					}
					IDisposable disposable = Interlocked.Exchange<IDisposable>(ref this._accessor, null);
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				this._pointer = null;
			}

			// Token: 0x1700035F RID: 863
			// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0002701C File Offset: 0x0002521C
			public unsafe byte* Pointer
			{
				get
				{
					return this._pointer;
				}
			}

			// Token: 0x04000BE0 RID: 3040
			private IDisposable _accessor;

			// Token: 0x04000BE1 RID: 3041
			private SafeBuffer _safeBuffer;

			// Token: 0x04000BE2 RID: 3042
			private unsafe byte* _pointer;
		}
	}
}
