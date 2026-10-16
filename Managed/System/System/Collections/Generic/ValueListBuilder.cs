using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020004C6 RID: 1222
	internal ref struct ValueListBuilder<T>
	{
		// Token: 0x0600278A RID: 10122 RVA: 0x00088EEE File Offset: 0x000870EE
		public ValueListBuilder(Span<T> initialSpan)
		{
			this._span = initialSpan;
			this._arrayFromPool = null;
			this._pos = 0;
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x00088F05 File Offset: 0x00087105
		public int Length
		{
			get
			{
				return this._pos;
			}
		}

		// Token: 0x17000811 RID: 2065
		public ref T this[int index]
		{
			get
			{
				return this._span[index];
			}
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x00088F1C File Offset: 0x0008711C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(T item)
		{
			int pos = this._pos;
			if (pos >= this._span.Length)
			{
				this.Grow();
			}
			*this._span[pos] = item;
			this._pos = pos + 1;
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x00088F5F File Offset: 0x0008715F
		public ReadOnlySpan<T> AsSpan()
		{
			return this._span.Slice(0, this._pos);
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x00088F78 File Offset: 0x00087178
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			if (this._arrayFromPool != null)
			{
				ArrayPool<T>.Shared.Return(this._arrayFromPool, false);
				this._arrayFromPool = null;
			}
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x00088F9C File Offset: 0x0008719C
		private void Grow()
		{
			T[] array = ArrayPool<T>.Shared.Rent(this._span.Length * 2);
			this._span.TryCopyTo(array);
			T[] arrayFromPool = this._arrayFromPool;
			this._span = (this._arrayFromPool = array);
			if (arrayFromPool != null)
			{
				ArrayPool<T>.Shared.Return(arrayFromPool, false);
			}
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x00088FFE File Offset: 0x000871FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe T Pop()
		{
			this._pos--;
			return *this._span[this._pos];
		}

		// Token: 0x0400156F RID: 5487
		private Span<T> _span;

		// Token: 0x04001570 RID: 5488
		private T[] _arrayFromPool;

		// Token: 0x04001571 RID: 5489
		private int _pos;
	}
}
