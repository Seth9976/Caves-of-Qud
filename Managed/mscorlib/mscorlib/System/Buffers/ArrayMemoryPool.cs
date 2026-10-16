using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000ADF RID: 2783
	internal sealed class ArrayMemoryPool<T> : MemoryPool<T>
	{
		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x0600630B RID: 25355 RVA: 0x0008408A File Offset: 0x0008228A
		public sealed override int MaxBufferSize
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x0600630C RID: 25356 RVA: 0x0014B41A File Offset: 0x0014961A
		public sealed override IMemoryOwner<T> Rent(int minimumBufferSize = -1)
		{
			if (minimumBufferSize == -1)
			{
				minimumBufferSize = 1 + 4095 / Unsafe.SizeOf<T>();
			}
			else if (minimumBufferSize > 2147483647)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.minimumBufferSize);
			}
			return new ArrayMemoryPool<T>.ArrayMemoryPoolBuffer(minimumBufferSize);
		}

		// Token: 0x0600630D RID: 25357 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected sealed override void Dispose(bool disposing)
		{
		}

		// Token: 0x04003A59 RID: 14937
		private const int s_maxBufferSize = 2147483647;

		// Token: 0x02000AE0 RID: 2784
		private sealed class ArrayMemoryPoolBuffer : IMemoryOwner<T>, IDisposable
		{
			// Token: 0x0600630F RID: 25359 RVA: 0x0014B44E File Offset: 0x0014964E
			public ArrayMemoryPoolBuffer(int size)
			{
				this._array = ArrayPool<T>.Shared.Rent(size);
			}

			// Token: 0x17001185 RID: 4485
			// (get) Token: 0x06006310 RID: 25360 RVA: 0x0014B467 File Offset: 0x00149667
			public Memory<T> Memory
			{
				get
				{
					T[] array = this._array;
					if (array == null)
					{
						ThrowHelper.ThrowObjectDisposedException_ArrayMemoryPoolBuffer();
					}
					return new Memory<T>(array);
				}
			}

			// Token: 0x06006311 RID: 25361 RVA: 0x0014B47C File Offset: 0x0014967C
			public void Dispose()
			{
				T[] array = this._array;
				if (array != null)
				{
					this._array = null;
					ArrayPool<T>.Shared.Return(array, false);
				}
			}

			// Token: 0x04003A5A RID: 14938
			private T[] _array;
		}
	}
}
